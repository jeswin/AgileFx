using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Microsoft.Practices.RepositoryFactory.SchemaDiscovery.CommonHelpers;
using System.Globalization;

namespace Microsoft.Practices.RepositoryFactory.SchemaDiscovery.ObjectModel.Base
{
    /// <summary>
    /// Represents a generic database table column object.
    /// </summary>
    public class Column
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
        /// Gets the name of the DB column.
        /// </summary>
        /// <value>The name of the DB column.</value>
        public string DBName
        {
            get
            {
                //Escape Name
                string nameTemp = string.Format(CultureInfo.CurrentCulture, Properties.Resources.EscapeObjectName, NamingHelper.EscapeObjectName(name));
                return nameTemp;
            } 
        }

        private bool isNullable;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is nullable.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is nullable; otherwise, <c>false</c>.
        /// </value>
        [Browsable(false)]
        public bool IsNullable
        {
            get { return isNullable; }
            set { isNullable = value; }
        }

        private bool isPrimaryKey;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is primary key.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is primary key; otherwise, <c>false</c>.
        /// </value>
        [Browsable(false)]
        public bool IsPrimaryKey
        {
            get { return isPrimaryKey; }
            set { isPrimaryKey = value; }
        }

        private bool isForeignKey;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is foreign key.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is foreign key; otherwise, <c>false</c>.
        /// </value>
        [Browsable(false)]
        public bool IsForeignKey
        {
            get { return isForeignKey; }
            set { isForeignKey = value; }
        }

        private bool isIdentity;
        /// <summary>
        /// Gets or sets a value indicating whether this instance is identity.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is identity; otherwise, <c>false</c>.
        /// </value>
        [Browsable(false)]
        public bool IsIdentity
        {
            get { return isIdentity; }
            set { isIdentity = value; }
        }

        private bool isUnique;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is unique.
        /// </summary>
        /// <value><c>true</c> if this instance is unique; otherwise, <c>false</c>.</value>
        [Browsable(false)]
        public bool IsUnique
        {
            get { return isUnique; }
            set { isUnique = value; }
        }

        private bool isReadOnly;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is read only.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is read only; otherwise, <c>false</c>.
        /// </value>
        [Browsable(false)]
        public bool IsReadOnly
        {
            get { return isReadOnly; }
            set { isReadOnly = value; }
        }

        private DbDataType dbDataType;

        /// <summary>
        /// Gets or sets the type of the db data.
        /// </summary>
        /// <value>The type of the db data.</value>
        [Browsable(false)]
        public DbDataType DbDataType
        {
            get { return dbDataType; }
            set { dbDataType = value; }
        }

        #endregion
    }
}
