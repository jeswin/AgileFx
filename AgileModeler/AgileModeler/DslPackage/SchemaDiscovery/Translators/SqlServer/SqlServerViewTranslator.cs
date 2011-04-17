using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.RepositoryFactory.SchemaDiscovery.ObjectModel.Base;
using Microsoft.Practices.RepositoryFactory.SchemaDiscovery.ObjectModel.SqlServer;
using Microsoft.Practices.RepositoryFactory.SchemaDiscovery.Translators.SqlServer.Helpers;
using Microsoft.Practices.RepositoryFactory.SchemaDiscovery.Translators.SqlServer.Data;

namespace Microsoft.Practices.RepositoryFactory.SchemaDiscovery.Translators.SqlServer
{
    internal static class SqlServerViewTranslator
    {
        public static SqlServerView SqlServerViewCollectionToView(DataRow sqlServerView, DataTable sqlServerColumnsColletions)
        {
            SqlServerView view = new SqlServerView();

            view.Name = sqlServerView["table_name"].ToString();
            view.Schema = sqlServerView["table_schema"].ToString();

            foreach(DataRow row in sqlServerColumnsColletions.Rows)
            {
                SqlServerColumn column = new SqlServerColumn();
                SqlServerDbDataType dbDataType = new SqlServerDbDataType();

                column.Name = row["ColumnName"].ToString();
                column.IsNullable = (bool)row["AllowDBNull"];

                dbDataType.ProviderType = SqlServerDataTypeConverter.SqlDbType2DatabaseType(row["ProviderType"]);
                dbDataType.Type = (Type)row["DataType"];
                dbDataType.Size = (int)row["ColumnSize"];
                dbDataType.Precision = (Int16)row["NumericPrecision"];
                dbDataType.Scale = (Int16)row["NumericScale"];

                column.DbDataType = dbDataType;
                view.Columns.Add(column);
            }

            return view;
        } 
    }
}