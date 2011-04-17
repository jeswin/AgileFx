//Credits: Originally included in Microsoft Code Samples.
//****************************************************************************
//
//    Copyright (c) Microsoft Corporation. All rights reserved.
//    This code is licensed under the Microsoft Public License.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//****************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Modeling;

using AgileFx.AgileModeler.CustomCode;

namespace AgileFx.AgileModeler.DslPackage.CustomCode
{
    public class FieldTypesProvider : System.ComponentModel.TypeConverter
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
        /// Return a list of the values to display in the grid
        /// </summary>
        /// <param name="context"></param>
        /// <returns>A list of values the user can choose from</returns>
        public override StandardValuesCollection GetStandardValues(System.ComponentModel.ITypeDescriptorContext context)
        {
            // Try to get a store from the current context
            // "context.Instance"  returns the element(s) that are currently selected i.e. whose values are being
            // shown in the property grid. Note that the user could have selected multiple objects, in which case 
            //context.Instance will be an array.
            Store store = GetStore(context.Instance);

            List<string> values = (store != null) ? DSLUtil.GetClasses(store).ToList() : new List<string>();
            return new StandardValuesCollection(values);
        }


        /// <summary>
        /// Attempts to get to a store from the currently selected object / objects
        /// in the property grid.
        /// </summary>
        private Store GetStore(object gridSelection)
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
