using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Microsoft.Practices.RepositoryFactory.SchemaDiscovery.CommonHelpers;
using System.Globalization;


namespace Microsoft.Practices.RepositoryFactory.SchemaDiscovery.ObjectModel.Base
{
    /// <summary>
    /// Represents a generic stored procedure object.
    /// </summary>
    public class StoredProcedure
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

        /// <summary>
        /// Gets the name of the DB StoreProcedure.
        /// </summary>
        /// <value>The name of the DB StoreProcedure.</value>
        public string DBName
        {
            get 
            {
                //Escape Name
                string nameTemp = string.Format(CultureInfo.CurrentCulture, Properties.Resources.EscapeObjectName, NamingHelper.EscapeObjectName(name));
                return nameTemp;
            }
        }

        private List<Parameter> parameters = new List<Parameter>();

        /// <summary>
        /// Gets or sets the parameters.
        /// </summary>
        /// <value>The parameters.</value>

        [Browsable(false)]
        public List<Parameter> Parameters
        {
            get { return parameters; }
        } 
        #endregion
    }
}
