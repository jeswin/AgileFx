using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Microsoft.Practices.RepositoryFactory.SchemaDiscovery.CommonHelpers;
using System.Globalization;

namespace Microsoft.Practices.RepositoryFactory.SchemaDiscovery.ObjectModel.Base
{
    /// <summary>
    /// Represents a generic database table object.
    /// </summary>
    public class Table
    {
        #region Properties
        private string name;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string schema;
        /// <summary>
        /// Gets or sets the schema.
        /// </summary>
        /// <value>The schema.</value>
        public string Schema
        {
            get { return schema; }
            set { schema = value; }
        }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        /// <value>The full name.</value>
        public string FullName
        {
            get { return string.Format(CultureInfo.CurrentCulture, 
                Properties.Resources.FullNameWithSchema, schema, name); }
        }

        /// <summary>
        /// Gets the name of the DB table.
        /// </summary>
        /// <value>The name of the DB table.</value>
        public string DBName
        {
            get 
            {
                //Escape Name
                string nameTemp = string.Format(CultureInfo.CurrentCulture, 
                    Properties.Resources.EscapeObjectNameWithSchema, 
                    schema, 
                    NamingHelper.EscapeObjectName(name));

                return nameTemp;
            }
        }

        private List<Column> columns = new List<Column>();

        /// <summary>
        /// Gets or sets the columns.
        /// </summary>
        /// <value>The columns.</value>

        [Browsable(false)]
        public List<Column> Columns
        {
            get { return columns; }
        } 

        #endregion
    }
}
