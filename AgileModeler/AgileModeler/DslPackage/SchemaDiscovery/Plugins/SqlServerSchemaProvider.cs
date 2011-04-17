
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.RepositoryFactory.SchemaDiscovery.Translators;
using Microsoft.Practices.RepositoryFactory.SchemaDiscovery.ObjectModel.Base;
using System.Data.SqlClient;
using Microsoft.Practices.RepositoryFactory.SchemaDiscovery.Translators.SqlServer;
using Microsoft.Practices.RepositoryFactory.SchemaDiscovery.ObjectModel.SqlServer;
using Microsoft.Practices.RepositoryFactory.SchemaDiscovery.Translators.SqlServer.Helpers;
using Microsoft.Practices.RepositoryFactory.SchemaDiscovery.Translators.SqlServer.Data;
using Microsoft.Practices.RepositoryFactory.SchemaDiscovery.CommonHelpers;
using System.Globalization;

namespace Microsoft.Practices.RepositoryFactory.SchemaDiscovery.Plugins
{
    /// <summary>
    /// Represents a Sql Server schema provider plugin
    /// </summary>
    internal class SqlServerSchemaProvider : DbSchemaProvider
    {
        #region DbSchemaProvider Members

        /// <summary>
        /// Gets the tables.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <returns></returns>
        public override List<Table> GetTables(DbConnection connection)
        {
            List<Table> tables = new List<Table>();
            DataTable tablesCollection = GetTablesCollection(connection);
            DataRow[] rows = tablesCollection.Select("", "table_schema, table_name ASC");

            foreach(DataRow table in rows)
            {
                string tableName = SqlServerTranslatorHelper.GetObjectName(table["table_schema"].ToString(), NamingHelper.EscapeObjectName(table["table_name"].ToString()));

                tables.Add(
                    SqlServerTableTranslator.SqlServerTableCollectionToTable(
                        table,
                        GetTableColumnsCollection(connection, tableName, true),
                        GetTableForeignKeysCollection(connection, table["table_schema"].ToString(), table["table_name"].ToString())));
            }

            return tables;
        }

        /// <summary>
        /// Gets the views.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <returns></returns>
        public override List<View> GetViews(DbConnection connection)
        {
            List<View> views = new List<View>();
            DataTable viewsCollection = GetViewsCollection(connection);
            DataRow[] rows = viewsCollection.Select("", "table_schema, table_name ASC");

            foreach(DataRow view in rows)
            {
                string viewName = SqlServerTranslatorHelper.GetObjectName(view["table_schema"].ToString(), NamingHelper.EscapeObjectName(view["table_name"].ToString()));

                views.Add(
                    SqlServerViewTranslator.SqlServerViewCollectionToView(
                        view,
                        GetViewColumnsCollection(connection, viewName)));
            }

            return views;
        }

        /// <summary>
        /// Gets the stored procedures.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <returns></returns>
        public override List<StoredProcedure> GetStoredProcedures(DbConnection connection)
        {
            List<StoredProcedure> storedProcedures = new List<StoredProcedure>();
            DataTable storedProceduresCollection = GetStoredProceduresCollection(connection);
            DataRow[] rows = storedProceduresCollection.Select("", "routine_name ASC");

            foreach(DataRow storedProcedure in rows)
            {
                storedProcedures.Add(
                    SqlServerStoredProcedureTranslator.SqlServerStoredProcedureCollectionToStoreProcedure(
                        storedProcedure,
                        GetStoredProcedureParametersCollection(connection, storedProcedure["routine_name"].ToString())));
            }

            return storedProcedures;
        }

        public override List<Parameter> GetSPResultSet(DbConnection connection, StoredProcedure procedure)
        {
            List<Parameter> parameters = new List<Parameter>();

            using(SqlCommand command = (SqlCommand)connection.CreateCommand())
            {
                command.CommandText = procedure.DBName;
                command.CommandType = CommandType.StoredProcedure;

                if(connection.ConnectionTimeout != -1)
                {
                    command.CommandTimeout = connection.ConnectionTimeout;
                }

                foreach(Parameter parameter in procedure.Parameters)
                {
                    SqlParameter parameterToAdd = command.Parameters.Add(
                        parameter.DBName,
                        SqlServerDataTypeConverter.DatabaseType2SqlDbType(parameter.DbDataType.ProviderType));
                    parameterToAdd.Direction = parameter.Direction;
                    parameterToAdd.Value = SqlServerTranslatorHelper.GetDefaultParameterValue(parameter.DbDataType.Type);
                }

                SqlTransaction transaction = null;

                try
                {
                    transaction = (SqlTransaction)connection.BeginTransaction();
                    command.Transaction = transaction;
                    int index = 0;
                    IDataReader dataReader = command.ExecuteReader(CommandBehavior.SchemaOnly);

                    try
                    {
                        do
                        {
                            DataTable table = dataReader.GetSchemaTable();

                            if(table != null)
                            {
                                parameters = GetProcedureResultSchema(table);
                            }

                            index++;

                        } while(dataReader.NextResult());
                    }
                    finally
                    {
                        if(dataReader != null)
                        {
                            dataReader.Dispose();
                        }
                    }
                }
                finally
                {
                    if(transaction != null)
                    {
                        transaction.Rollback();
                    }
                }
            }

            return parameters;
        }
        #endregion

        #region Private Implementation

        private DataTable GetTablesCollection(DbConnection connection)
        {
            string[] restrictions = new String[4] { null, null, null, "BASE TABLE" };
            DataTable tables = connection.GetSchema("Tables", restrictions);

            return tables;
        }

        private DataTable GetTableColumnsCollection(DbConnection connection, string tableName, bool isTable)
        {
            //string[] restrictions = new string[4] { null, null, tableName, null };
            //DataTable columns = connection.GetSchema("Columns", restrictions);
            //columns.TableName = tableName;
            //return columns;

            DbCommand command = connection.CreateCommand();
            DataTable columns = new DataTable();
            columns.Locale = CultureInfo.CurrentCulture;
            command.CommandText = string.Format(CultureInfo.CurrentCulture, "SELECT TOP 1 * FROM {0}", tableName);

            if(isTable)
            {
                using(IDataReader reader = command.ExecuteReader(CommandBehavior.KeyInfo))
                {
                    columns = reader.GetSchemaTable();
                    reader.Close();
                }
            }
            else
            {
                using(IDataReader reader = command.ExecuteReader(CommandBehavior.SchemaOnly))
                {
                    columns = reader.GetSchemaTable();
                    reader.Close();
                }
            }

            return columns;
        }

        private DataTable GetTableForeignKeysCollection(DbConnection connection, string schema, string tableName)
        {
            string[] restrictions = new string[4] { null, null, tableName, null };
            DataTable foreignKeys = connection.GetSchema("ForeignKeys", restrictions);

            DataTable constraints = new DataTable();
            constraints.Locale = CultureInfo.CurrentCulture;
            constraints.TableName = tableName;
            constraints.Columns.Add(new DataColumn("ColumnName"));

            string text1 = string.Format(CultureInfo.CurrentCulture, "SELECT * FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE where table_schema = '{0}' and table_name = '{1}'", schema, tableName);
            SqlCommand command1 = new SqlCommand(text1, (SqlConnection)connection);
            using(IDataReader reader1 = command1.ExecuteReader())
            {
                while(reader1.Read())
                {
                    string contraintName = (string)reader1["CONSTRAINT_NAME"];

                    if(IsForeignKeyPresent(foreignKeys, contraintName, schema, tableName))
                    {
                        DataRow row = constraints.NewRow();
                        row["ColumnName"] = (string)reader1["COLUMN_NAME"];
                        constraints.Rows.Add(row);
                    }
                }
            }
            
            return constraints;
        }

        private DataTable GetViewsCollection(DbConnection connection)
        {
            string[] restrictions = new String[3] { null, null, null };
            DataTable views = connection.GetSchema("Views", restrictions);
            return views;
        }

        private DataTable GetViewColumnsCollection(DbConnection connection, string viewName)
        {
            //string[] restrictions = new string[4] { null, null, viewName, null };
            //DataTable columns = connection.GetSchema("ViewColumns", restrictions);
            //columns.TableName = viewName;
            //return columns;

            return GetTableColumnsCollection(connection, viewName, false);
        }

        private DataTable GetStoredProceduresCollection(DbConnection connection)
        {
            string[] restrictions = new String[4] { null, null, null, "PROCEDURE" };
            DataTable procedures = connection.GetSchema("Procedures", restrictions);
            return procedures;
        }

        private DataTable GetStoredProcedureParametersCollection(DbConnection connection, string storedProcedureName)
        {
            string[] restrictions = new String[4] { null, null, storedProcedureName, null };
            DataTable procedureParameters = connection.GetSchema("ProcedureParameters", restrictions);
            procedureParameters.TableName = storedProcedureName;

            return procedureParameters;
        }

        private List<Parameter> GetProcedureResultSchema(DataTable schema)
        {
            List<Parameter> parameters = new List<Parameter>();

            foreach(DataRow row1 in schema.Rows)
            {
                Type type1 = (Type)row1["DataType"];
                int num1 = -1;
                if(type1 == typeof(string))
                {
                    num1 = (int)row1["ColumnSize"];
                }

                SqlServerParameter parameter = new SqlServerParameter();
                parameter.DBName = SqlServerTranslatorHelper.GetQualifiedName((string)row1["ColumnName"]);
                parameter.Direction = ParameterDirection.ReturnValue;

                SqlServerDbDataType dbDataType = new SqlServerDbDataType();
                dbDataType.Size = num1;
                dbDataType.ProviderType = SqlServerDataTypeConverter.SqlDbType2DatabaseType(row1["ProviderType"]);
                dbDataType.Type = type1;
                parameter.DbDataType = dbDataType;

                parameters.Add(parameter);
            }
            return parameters;
        }

        private bool IsForeignKeyPresent(DataTable foreignKeys, string contraintName, string schema, string tableName)
        {
            foreach(DataRow row in foreignKeys.Rows)
            {
                if(row["table_schema"].ToString() == schema && row["table_name"].ToString() == tableName)
                {
                    if(row["constraint_name"].ToString() == contraintName)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        #endregion
    }
}