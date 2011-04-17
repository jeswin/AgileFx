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
    internal static class SqlServerStoredProcedureTranslator
    {
        public static SqlServerStoredProcedure SqlServerStoredProcedureCollectionToStoreProcedure(DataRow sqlServerStoredProcedure, DataTable sqlServerParametersColletions)
        {
            SqlServerStoredProcedure storedProcedure = new SqlServerStoredProcedure();
            storedProcedure.Name = sqlServerStoredProcedure["routine_name"].ToString();

            foreach(DataRow row in sqlServerParametersColletions.Rows)
            {
                SqlServerParameter parameter = new SqlServerParameter();
                parameter.DBName = row["parameter_name"].ToString();
                parameter.Direction = SqlServerTranslatorHelper.GetDirection(row["parameter_mode"].ToString());

                SqlServerDbDataType dbDataType = new SqlServerDbDataType();
                dbDataType.ProviderType = SqlServerDataTypeConverter.String2DatabaseType(row["data_type"].ToString());
                dbDataType.Type = SqlServerDataTypeConverter.DatabaseType2NetType(dbDataType.ProviderType);
                dbDataType.Precision = SqlServerTranslatorHelper.GetPrecision(row["numeric_precision"]);
                dbDataType.Scale = SqlServerTranslatorHelper.GetScale(row["numeric_scale"]);
                dbDataType.Size = SqlServerTranslatorHelper.GetParameterSize(dbDataType.ProviderType, row);
                parameter.DbDataType = dbDataType;

                storedProcedure.Parameters.Add(parameter);
            }

            return storedProcedure;
        } 
    }
}