using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Design;

using Microsoft.SqlServer.Management.Common;

using AgileFx.AgileModeler.DslPackage.CustomCode.Forms;
using AgileFx.AgileModeler.DslPackage.CustomCode.ImportExport.GenerateSQL;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.VisualStudio.Modeling;
using AgileFx.AgileModeler.DslPackage.Utils;

namespace AgileFx.AgileModeler.DslPackage.CustomCode.MenuCommands
{
    class GenerateDbSchemaCommand : DSLMenuCommandImplBase
    {
        Guid commandGuid = new Guid("3c377eba-e82a-45af-9112-a339092d3d4a");
        int commandID = 0x821;

        public override void StatusHandler(CommandSetState state)
        {
            foreach (object selectedObject in state.CurrentSelection)
            {
                if (selectedObject is ClassDiagram)
                {
                    MenuCommand.Visible = MenuCommand.Enabled = true;
                    MenuCommand.Enabled = true;
                    return;
                }
                else
                {
                    MenuCommand.Visible = false;
                    MenuCommand.Enabled = false;
                }
            }
        }

        public override void InvokeHandler(CommandSetState state)
        {
            var diagram = state.CurrentDocView.CurrentDiagram;
            var modelRoot = diagram.Store.ElementDirectory.FindElements<ModelRoot>().Single();
            var connectionString = modelRoot.ConnectionString;

            var dlg = new GenerateSQLForm();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var sqlGenerator = new DbSchemaGenerator(diagram)
                {
                    CleanUpDbSchema = dlg.CleanupDbSchema,
                    UseNavigationPropertyNameForFKeys = dlg.UseNavigationPropertyNameForFKeys,
                };
                var sb = sqlGenerator.GenerateScripts();
                System.IO.File.WriteAllText(dlg.Filename, sb.ToString());

                if (dlg.OverwriteDatabase)
                {
                    //Creating a connection to the given database
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        var originalDatabase = sqlConnection.Database;
                        sqlConnection.Open();

                        //Switching to master database
                        sqlConnection.ChangeDatabase("master");
                        ServerConnection svrConnection = new ServerConnection(sqlConnection);

                        //Recreating database and executing the query file
                        DropAndRecreateDatabase(originalDatabase, svrConnection);
                        svrConnection.ExecuteNonQuery(System.IO.File.ReadAllText(dlg.Filename));
                    }

                    ModelerTransaction.Enter(() =>
                        {

                            //Importing the new schema from database
                            var sync = new Utilities.DbSchemaImporter(diagram);
                            sync.FullDatabaseReload = true;
                            sync.ImportModels();
                        });
                }
                System.Windows.Forms.MessageBox.Show("Sql script generation completed.");
            }
        }

        private void DropAndRecreateDatabase(string databaseName, ServerConnection connection)
        {
            var commandText =
            @"USE [master]
            IF EXISTS(SELECT * FROM sys.databases WHERE NAME = '{0}')
            BEGIN
                ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE
                DROP DATABASE [{0}]
            END
            CREATE DATABASE [{0}]
            GO";
            connection.ExecuteNonQuery(string.Format(commandText, databaseName));
        }

        public override System.ComponentModel.Design.CommandID GetCommandID()
        {
            return new CommandID(commandGuid, commandID);
        }
    }
}
