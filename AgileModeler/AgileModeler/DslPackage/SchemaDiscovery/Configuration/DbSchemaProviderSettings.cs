using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Configuration; 

namespace Microsoft.Practices.RepositoryFactory.SchemaDiscovery.Configuration
{
    internal class DbSchemaProviderSettings : SerializableConfigurationSection
    {
        private const string dbSchemaProviderMappingsProperty = "dbSchemaProviderMappings";
        public const string SectionName = "dbSchemaProviderConfiguration";

        /// <summary>
        /// Initializes a new instance of the <see cref="T:DbSchemaProviderSettings"/> class.
        /// </summary>
        public DbSchemaProviderSettings()
            : base()
        {
        }

        /// <summary>
        /// Gets the schema provider settings.
        /// </summary>
        /// <param name="configurationSource">The configuration source.</param>
        /// <returns></returns>
        public static DbSchemaProviderSettings GetSchemaProviderSettings(IConfigurationSource configurationSource)
        {
            return (DbSchemaProviderSettings)configurationSource.GetSection(SectionName);
        }

        /// <summary>
        /// Gets the provider mappings.
        /// </summary>
        /// <value>The provider mappings.</value>
        [ConfigurationProperty(dbSchemaProviderMappingsProperty, IsRequired = false)]
        public NamedElementCollection<DbSchemaProviderMapping> ProviderMappings
        {
            get
            {
                return (NamedElementCollection<DbSchemaProviderMapping>)base[dbSchemaProviderMappingsProperty];
            }
        }
    }
}
