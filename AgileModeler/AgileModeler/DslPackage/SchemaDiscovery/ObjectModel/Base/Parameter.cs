using System;
using System.Collections.Generic;
using System.Text;
using System.Data;


namespace Microsoft.Practices.RepositoryFactory.SchemaDiscovery.ObjectModel.Base
{
    /// <summary>
    /// Represents a generic stored procedure parameter.
    /// </summary>
    public class Parameter
    {
        #region Properties
        private string dbName;

        /// <summary>
        /// Gets or sets the dbname.
        /// </summary>
        /// <value>The dbname.</value>
        public string DBName
        {
            get { return dbName; }
            set { dbName = value; }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get 
            {
                string nameTemp = dbName.Replace("@", "");
                nameTemp = nameTemp.Replace(" ", "");
                nameTemp = nameTemp.Replace("[", "");
                nameTemp = nameTemp.Replace("]", "");

                return nameTemp;
            }
        }

        private ParameterDirection direction;

        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        /// <value>The direction.</value>
        public ParameterDirection Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        private DbDataType dbDataType;

        /// <summary>
        /// Gets or sets the type of the db data.
        /// </summary>
        /// <value>The type of the db data.</value>
        public DbDataType DbDataType
        {
            get { return dbDataType; }
            set { dbDataType = value; }
        }
        #endregion
    }
}