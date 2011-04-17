using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder;
using Microsoft.Practices.RepositoryFactory.SchemaDiscovery.ObjectModel.Base; 

namespace Microsoft.Practices.RepositoryFactory.SchemaDiscovery
{
    [CustomFactory(typeof(DbSchemaProviderFactory))]
    internal abstract class DbSchemaProvider
    {
        #region Public Implementation
        /// <summary>
        /// Gets the tables.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <returns></returns>
        public abstract List<Table> GetTables(DbConnection connection);

        /// <summary>
        /// Gets the views.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <returns></returns>
        public abstract List<View> GetViews(DbConnection connection);

        /// <summary>
        /// Gets the stored procedures.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <returns></returns>
        public abstract List<StoredProcedure> GetStoredProcedures(DbConnection connection); 

        public abstract List<Parameter> GetSPResultSet(DbConnection connection, StoredProcedure procedure);
        #endregion
    }
}