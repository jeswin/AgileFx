using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration; 

namespace Microsoft.Practices.RepositoryFactory.SchemaDiscovery.Configuration
{
    internal class DbSchemaProviderConfigurationView
    {
        private IConfigurationSource configurationSource;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:DbSchemaProviderConfigurationView"/> class.
        /// </summary>
        /// <param name="configurationSource">The configuration source.</param>
        public DbSchemaProviderConfigurationView(IConfigurationSource configurationSource)
		{
			this.configurationSource = configurationSource;
		}

        /// <summary>
        /// Gets the db schema provider settings.
        /// </summary>
        /// <value>The db schema provider settings.</value>
        public DbSchemaProviderSettings DbSchemaProviderSettings
        {
            get { return (DbSchemaProviderSettings)configurationSource.GetSection(DbSchemaProviderSettings.SectionName); }
        }

        /// <summary>
        /// Gets the provider mapping.
        /// </summary>
        /// 
        /// <param name="dbProviderName">Name of the db provider.</param>
        /// <returns></returns>
        public DbSchemaProviderMapping GetProviderMapping(string dbProviderName)
        {
            DbSchemaProviderSettings settings = this.DbSchemaProviderSettings;
            if(settings != null)
            {
                DbSchemaProviderMapping existingMapping = settings.ProviderMappings.Get(dbProviderName);
                if(existingMapping != null)
                {
                    return existingMapping;
                }
            }

            DbSchemaProviderMapping defaultMapping = DbSchemaProviderMapping.GetDefaultMapping(dbProviderName);
            if(defaultMapping != null)
            {
                return defaultMapping;
            }

            return null;
        }
    }
}
