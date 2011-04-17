using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.RepositoryFactory.SchemaDiscovery.Plugins;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.RepositoryFactory.SchemaDiscovery.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Configuration; 

namespace Microsoft.Practices.RepositoryFactory.SchemaDiscovery
{
    internal class DbSchemaProviderFactory : ICustomFactory
    {
        #region ICustomFactory Members

        /// <summary>
        /// Returns an new instance of the type the receiver knows how to build.
        /// </summary>
        /// <param name="context">The <see cref="IBuilderContext"/> that represents the current building process.</param>
        /// <param name="name">The name of the instance to build, or null.</param>
        /// <param name="configurationSource">The source for configuration objects.</param>
        /// <param name="reflectionCache">The cache to use retrieving reflection information.</param>
        /// <returns>The new instance.</returns>
        public object CreateObject(IBuilderContext context, string name, IConfigurationSource configurationSource, ConfigurationReflectionCache reflectionCache)
        {
            DatabaseConfigurationView databaseConfigurationView = new DatabaseConfigurationView(configurationSource);            
            ConnectionStringSettings connectionStringSettings = databaseConfigurationView.GetConnectionStringSettings(name);
            DbSchemaProviderConfigurationView dbSchemaProviderConfigurationView = new DbSchemaProviderConfigurationView(configurationSource);
            DbSchemaProviderMapping mapping = dbSchemaProviderConfigurationView.GetProviderMapping(connectionStringSettings.ProviderName);

            return Activator.CreateInstance(mapping.DbSchemaProviderType);
        }

        #endregion
    }
}