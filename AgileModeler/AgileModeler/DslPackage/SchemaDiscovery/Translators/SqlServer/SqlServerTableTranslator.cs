using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.RepositoryFactory.SchemaDiscovery.ObjectModel.Base;
using Microsoft.Practices.RepositoryFactory.SchemaDiscovery.ObjectModel.SqlServer;
using Microsoft.Practices.RepositoryFactory.SchemaDiscovery.Translators.SqlServer.Data;
using Microsoft.Practices.RepositoryFactory.SchemaDiscovery.Translators.SqlServer.Helpers;

namespace Microsoft.Practices.RepositoryFactory.SchemaDiscovery.Translators.SqlServer
{
    internal static class SqlServerTableTranslator
    {
        public static SqlServerTable SqlServerTableCollectionToTable(DataRow sqlServerTable, DataTable sqlServerColumnsColletions, DataTable sqlServerForeignKeysCollection)
        {
            SqlServerTable table = new SqlServerTable();

            table.Name = sqlServerTable["table_name"].ToString();
            table.Schema = sqlServerTable["table_schema"].ToString();

            DataRow[] rows = sqlServerColumnsColletions.Select("", "ColumnName ASC");

            foreach(DataRow row in rows)
            {
                SqlServerColumn column = new SqlServerColumn();
                SqlServerDbDataType dbDataType = new SqlServerDbDataType();

                column.Name = row["ColumnName"].ToString();
                column.IsPrimaryKey = (bool)row["IsKey"];
                column.IsNullable = (bool)row["AllowDBNull"];
                column.IsUnique = (bool)row["IsUnique"];
                column.IsIdentity = (bool)row["IsIdentity"];
                column.IsReadOnly = (bool)row["IsReadOnly"];

                dbDataType.ProviderType = SqlServerDataTypeConverter.SqlDbType2DatabaseType(row["ProviderType"]);
                dbDataType.Type = (Type)row["DataType"];
                dbDataType.Size = (int)row["ColumnSize"];
                dbDataType.Precision = (Int16)row["NumericPrecision"];
                dbDataType.Scale = (Int16)row["NumericScale"];

                column.DbDataType = dbDataType;
                table.Columns.Add(column);
            }

            foreach(DataRow row in sqlServerForeignKeysCollection.Rows)
            {
                string columnName = row["ColumnName"].ToString();

                table.Columns.Find(
                    delegate(Column column)
                    { return (column.Name.Equals(columnName)); }).IsForeignKey = true;
            }

            return table;
        } 
    }
}