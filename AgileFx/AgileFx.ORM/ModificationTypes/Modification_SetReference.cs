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
        public Modification<TContainer, TElement> SetRef<TField>(Expression<Func<TElement, TField>> fieldSelector,
            Expression<Func<TField, bool>> predicate) where TField : class, IEntity, new()
        {
            var upd = new SetReference<TElement, TField>(fieldSelector, predicate, null, null);
            this.EntityFieldModifications.Add(upd);
            return this;
        }

        public Modification<TContainer, TElement> SetRef<TField>(Expression<Func<TElement, IEnumerable<TField>>> collectionSelector,
            params Expression<Func<TField, bool>>[] predicates) where TField : class, IEntity, new()
        {
            foreach (var func in predicates)
            {
                this.EntityFieldModifications.Add(new SetReference<TElement, TField>(collectionSelector, func, null));
            }
            return this;
        }
    }
}
