using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Modeling;

namespace AgileFx.AgileModeler.CustomCode.TypeConverters
{
    public abstract class TypeConverterBase : System.ComponentModel.TypeConverter
    {
        /// <summary>
        /// Return true to indicate that we return a list of values to choose from
        /// </summary>
        /// <param name="context"></param>
        public override bool GetStandardValuesSupported(System.ComponentModel.ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        /// Returns true to indicate that the user has to select a value from the list
        /// </summary>
        /// <param name="context"></param>
        /// <returns>If we returned false, the user would be able to either select a value from 
        /// the list or type in a value that is not in the list.</returns>
        public override bool GetStandardValuesExclusive(System.ComponentModel.ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        /// Attempts to get to a store from the currently selected object / objects
        /// in the property grid.
        /// </summary>
        protected Store GetStore(object gridSelection)
        {
            // We assume that "instance" will either be a single model element, or 
            // an array of model elements (if multiple items are selected).

            ModelElement currentElement = null;

            object[] objects = gridSelection as object[];
            if (objects != null && objects.Length > 0)
            {
                currentElement = objects[0] as ModelElement;
            }
            else
            {
                currentElement = gridSelection as ModelElement;
            }

            return (currentElement == null) ? null : currentElement.Store;
        }
    }
}
