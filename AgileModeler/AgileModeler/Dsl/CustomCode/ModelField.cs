using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgileFx.AgileModeler
{
    partial class ModelField
    {
        public string ColumnName
        {
            get
            {
                return string.IsNullOrEmpty(this.DatabaseColumnName) ? this.Name : this.DatabaseColumnName;
            }
            set
            {
                this.DatabaseColumnName = value;
            }
        }
    }
}
