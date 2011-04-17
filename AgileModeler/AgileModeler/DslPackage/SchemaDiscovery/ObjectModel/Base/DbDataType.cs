using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel; 

namespace Microsoft.Practices.RepositoryFactory.SchemaDiscovery.ObjectModel.Base
{
    /// <summary>
    /// Represents a generic database type object.
    /// </summary>
    public class DbDataType
    {
        #region Properties
        private Type type;

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [Browsable(false)]
        public Type Type
        {
            get { return type; }
            set { type = value; }
        }

        private int size;

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>The size.</value>
        public int Size
        {
            get { return size; }
            set { size = value; }
        }

        private Int16 precision;

        /// <summary>
        /// Gets or sets the precision.
        /// </summary>
        /// <value>The precision.</value>
        public Int16 Precision
        {
            get { return precision; }
            set { precision = value; }
        }

        private Int16 scale;

        /// <summary>
        /// Gets or sets the scale.
        /// </summary>
        /// <value>The scale.</value>
        public Int16 Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        private DatabaseType providerType;

        /// <summary>
        /// Gets or sets the type of the provider.
        /// </summary>
        /// <value>The type of the provider.</value>
        public DatabaseType ProviderType
        {
            get { return providerType; }
            set { providerType = value; }
        }
        #endregion
    }
}
