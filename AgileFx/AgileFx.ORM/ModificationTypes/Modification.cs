/* AgileFx Version 2.0
 * An open-source framework for rapid development of applications and services using Microsoft.net
 * Developed by: AgileHead
 * Website: www.agilefx.org
 * This component is licensed under the terms of the Apache 2.0 License.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace AgileFx.ORM.ModificationTypes
{
    public abstract partial class Modification<TContainer, TElement> : IModification<TElement>
        where TContainer : class, new()
        where TElement : class, new()
    {
        public FieldReference<TContainer, TElement> FieldReference { get; set; }

        #region IModification

        public override IFieldReference GetFieldReference()
        {
            return FieldReference;
        }

        public FieldReference<TContainer, TElement> GetTypedFieldReference()
        {
            return FieldReference;
        }

        #endregion        
    }
}
