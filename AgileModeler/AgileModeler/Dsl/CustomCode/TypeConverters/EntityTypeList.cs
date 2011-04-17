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

namespace AgileFx.AgileModeler.CustomCode.TypeConverters
{
    public class EntityTypeList : TypeConverterBase
    {
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

            List<string> values = new List<string>();

            if (store != null)
                values.AddRange(store.ElementDirectory.FindElements<ModelClass>().Select(e => e.Name));

            return new StandardValuesCollection(values);
        }
    }
}
