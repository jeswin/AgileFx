using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AgileFx.AgileModeler.DslPackage.CustomCode.Forms;
using System.Data.SqlClient;
using Microsoft.VisualStudio.Modeling;

namespace AgileFx.AgileModeler.DslPackage.Utils
{
    public enum ConnectionConfigurationType
    {
        CreateLocal = 0,
        ChooseDatabase,
        UseCustomConnection,
    }

    public static class ConnectionUtil
    {
        static string ChooseDatabase()
        {
            var dlg = new ConnectionWizardDialog();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                return dlg.ConnectionString;
            else
                return string.Empty;
        }

        public static string SetExistingConnection(ModelRoot root)
        {
            string connectionString = string.Empty;
            while (string.IsNullOrEmpty(connectionString))
            {
                connectionString = ChooseDatabase();
            }
            using (Transaction tx = root.Store.TransactionManager.BeginTransaction("SetConnectionString", false))
            {
                root.ConnectionString = connectionString;
                tx.Commit();
            }
            return connectionString;
        }

        public static string GetOrCreateConnectionString(ModelRoot root, string diagramName)
        {
            string connectionString = root.ConnectionString;
            while(string.IsNullOrEmpty(connectionString))
            {
                var dbName = string.Join("", diagramName.Split(' ')) + "Db";
                var getOrCreateConnectionDlg = new GetOrCreateConnectionForm(dbName);
                if (getOrCreateConnectionDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    switch (getOrCreateConnectionDlg.ConfigurationType)
                    {
                        case ConnectionConfigurationType.CreateLocal:
                            try
                            {
                                connectionString = CreateLocalConnection(dbName);
                            }
                            catch (Exception e)
                            {
                                System.Windows.Forms.MessageBox.Show(e.Message + "\nPlease choose a connection.");
                                connectionString = ChooseDatabase();
                            }
                            break;
                        case ConnectionConfigurationType.ChooseDatabase:
                            connectionString = ChooseDatabase();
                            break;
                        case ConnectionConfigurationType.UseCustomConnection:
                            connectionString = getOrCreateConnectionDlg.ConnectionString;
                            break;
                    }
                }
            }
            using (Transaction tx = root.Store.TransactionManager.BeginTransaction("SetConnectionString", false))
            {
                root.ConnectionString = connectionString;
                tx.Commit();
            }
            return connectionString;
        }

        public static string GetDefaultConnectionString(string diagramName)
        {
            var dbName = string.Join("", diagramName.Split(' ')) + "Db";
            var connStrBuilder = new SqlConnectionStringBuilder
            {
                DataSource = ".",
                InitialCatalog = dbName,
                IntegratedSecurity = true,
            };
            return connStrBuilder.ConnectionString;
        }

        private static string CreateLocalConnection(string databaseName)
        {
            var connStrBuilder = new SqlConnectionStringBuilder
            {
                DataSource = ".",
                InitialCatalog = "master",
                IntegratedSecurity = true,
            };
            var conn = new SqlConnection(connStrBuilder.ConnectionString);
            try {   conn.Open(); }
            catch { throw new Exception("Unable to connect to local database server."); }

            var dbName = databaseName;

            try { new SqlCommand(string.Format("CREATE DATABASE {0};", dbName), conn).ExecuteNonQuery(); }
            catch { throw new Exception("Unable to create new database on the local database server."); }

            connStrBuilder.InitialCatalog = dbName;
            return connStrBuilder.ConnectionString;
        }
    }
}
