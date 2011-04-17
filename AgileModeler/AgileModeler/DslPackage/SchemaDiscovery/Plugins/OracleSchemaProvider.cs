using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.RepositoryFactory.SchemaDiscovery.Translators;
using Microsoft.Practices.RepositoryFactory.SchemaDiscovery.ObjectModel.Base;

namespace Microsoft.Practices.RepositoryFactory.SchemaDiscovery.Plugins
{
    /// <summary>
    /// Represents a Oracle schema provider plugin
    /// </summary>
    internal class OracleSchemaProvider : DbSchemaProvider
    {
        #region DbSchemaProvider Members
        /// <summary>
        /// Gets the tables.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <returns></returns>
        public override List<Table> GetTables(DbConnection connection)
        {
            throw new NotImplementedException(Properties.Resources.NotImplementedException);
        }

        /// <summary>
        /// Gets the views.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <returns></returns>
        public override List<View> GetViews(DbConnection connection)
        {
            throw new NotImplementedException(Properties.Resources.NotImplementedException);
        }

        /// <summary>
        /// Gets the stored procedures.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <returns></returns>
        public override List<StoredProcedure> GetStoredProcedures(DbConnection connection)
        {
            throw new NotImplementedException(Properties.Resources.NotImplementedException);
        } 

        public override List<Parameter> GetSPResultSet(DbConnection connection, StoredProcedure procedure)
        {
            throw new NotImplementedException(Properties.Resources.NotImplementedException);
        }
        #endregion
    }
}