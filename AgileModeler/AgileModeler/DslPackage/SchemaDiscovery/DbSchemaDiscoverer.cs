using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder;
using Microsoft.Practices.RepositoryFactory.SchemaDiscovery.ObjectModel.Base;
using System.Configuration;
using Microsoft.Practices.RepositoryFactory.SchemaDiscovery.Plugins;

namespace Microsoft.Practices.RepositoryFactory.SchemaDiscovery
{
    /// <summary/>
    public class DbSchemaDiscoverer
    {
        #region Fields
        DbSchemaProvider dbSchemaProvider;
        DbProviderFactory factory;
        string cnstring;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="T:DbSchemaDiscovery"/> class.
        /// </summary>
        /// <param name="databaseName">Name of the database.</param>
        public DbSchemaDiscoverer(string databaseName)
        {
            Database database = DatabaseFactory.CreateDatabase(databaseName);
            this.cnstring = database.ConnectionStringWithoutCredentials;
            this.factory = database.DbProviderFactory;

            //TODO: Get mapping from wide configuration settings and call object builder
            dbSchemaProvider = new SqlServerSchemaProvider(); //EnterpriseLibraryFactory.BuildUp<DbSchemaProvider>(databaseName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:DbSchemaDiscoverer"/> class.
        /// </summary>
        /// <param name="connectionSettings">The connection settings.</param>
        public DbSchemaDiscoverer(ConnectionStringSettings connectionSettings)
        {
            if (connectionSettings == null)
                throw new ArgumentNullException("connectionSettings");
            
            this.cnstring = connectionSettings.ConnectionString;
            this.factory = DbProviderFactories.GetFactory(connectionSettings.ProviderName);

            //TODO: Get mapping from wide configuration settings
            if(connectionSettings.ProviderName == "System.Data.SqlClient")
            {
                dbSchemaProvider = new SqlServerSchemaProvider();
            }
        }
        #endregion

        #region Public Implementation
        /// <summary>
        /// Discovers the tables.
        /// </summary>
        /// <returns></returns>
        public List<Table> DiscoverTables()
        {
            using(DbConnection connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.cnstring;
                connection.Open();
                return dbSchemaProvider.GetTables(connection);
            }
        }

        /// <summary>
        /// Discovers the views.
        /// </summary>
        /// <returns></returns>
        public List<View> DiscoverViews()
        {
            using(DbConnection connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.cnstring;
                connection.Open();
                return dbSchemaProvider.GetViews(connection);
            }
        }

        /// <summary>
        /// Discovers the stored procedures.
        /// </summary>
        /// <returns></returns>
        public List<StoredProcedure> DiscoverStoredProcedures()
        {
            using(DbConnection connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.cnstring;
                connection.Open();
                return dbSchemaProvider.GetStoredProcedures(connection);
            }
        }

        /// <summary>
        /// Discovers the SP result set.
        /// </summary>
        /// <param name="procedure">The procedure.</param>
        /// <returns></returns>
        public List<Parameter> DiscoverSPResultSet(StoredProcedure procedure)
        {
            using(DbConnection connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.cnstring;
                connection.Open();
                return dbSchemaProvider.GetSPResultSet(connection, procedure);
            }
        }

        #endregion
    }
}