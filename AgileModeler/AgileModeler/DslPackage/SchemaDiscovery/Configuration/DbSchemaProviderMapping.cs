using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Configuration;
using System.ComponentModel;
using Microsoft.Practices.RepositoryFactory.SchemaDiscovery.Plugins; 

namespace Microsoft.Practices.RepositoryFactory.SchemaDiscovery.Configuration
{
    internal class DbSchemaProviderMapping : NamedConfigurationElement
    {
        public const string DefaultSqlProviderName = "System.Data.SqlClient";
        public const string DefaultOracleProviderName = "System.Data.OracleClient";
        private const string dbSchemaProviderTypeProperty = "dbSchemaProviderType";
        private static readonly DbSchemaProviderMapping defaultSqlMapping = new DbSchemaProviderMapping(DbSchemaProviderMapping.DefaultSqlProviderName, typeof(SqlServerSchemaProvider));
        private static readonly DbSchemaProviderMapping defaultOracleMapping = new DbSchemaProviderMapping(DbSchemaProviderMapping.DefaultOracleProviderName, typeof(OracleSchemaProvider));

        /// <summary>
        /// Initializes a new instance of the <see cref="T:DbSchemaProviderMapping"/> class.
        /// </summary>
        public DbSchemaProviderMapping()
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="T:DbSchemaProviderMapping"/> class.
        /// </summary>
        /// <param name="dbProviderName">Name of the db provider.</param>
        /// <param name="dbSchemaProviderType">Type of the db schema provider.</param>
        public DbSchemaProviderMapping(string dbProviderName, Type dbSchemaProviderType)
			: base(dbProviderName)
		{
            this.DbSchemaProviderType = dbSchemaProviderType;
		}

        /// <summary>
        /// Gets or sets the type of the db schema provider.
        /// </summary>
        /// <value>The type of the db schema provider.</value>
        [ConfigurationProperty(dbSchemaProviderTypeProperty)]
        [TypeConverter(typeof(AssemblyQualifiedTypeNameConverter))]
        [SubclassTypeValidator(typeof(DbSchemaProvider))]
        public Type DbSchemaProviderType
        {
            get { 
                return (Type)base[dbSchemaProviderTypeProperty]; }
            set { base[dbSchemaProviderTypeProperty] = value; }
        }

        public static DbSchemaProviderMapping GetDefaultMapping(string dbProviderName)
        {
            if (DefaultSqlProviderName.Equals(dbProviderName))
                return defaultSqlMapping;

            if (DefaultOracleProviderName.Equals(dbProviderName))
                return defaultOracleMapping;

            return null;
        }
    }
}