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
    partial class Modification<TContainer, TElement>
    {        
        public Modification<TContainer, TElement> Remove<TField>(Expression<Func<TElement, TField>> fieldSelector)
            where TField : class, new()
        {
            var upd = new EntityDeletion<TElement, TField>(fieldSelector);
            this.EntityFieldModifications.Add(upd);
            return this;
        }

        public Modification<TContainer, TElement> Remove<TField>(Expression<Func<TElement, IEnumerable<TField>>> collectionSelector,
            Func<TField, bool> predicate)
            where TField : class, new()
        {
            var upd = new EntityDeletion<TElement, TField>(collectionSelector, predicate);
            this.EntityFieldModifications.Add(upd);
            return this;
        }
    }
}
