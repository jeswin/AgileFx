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
        public Modification<TContainer, TElement> Update<TField>(Expression<Func<TElement, TField>> fieldSelector,
            Action<TField> entityUpdate) where TField : class, new()
        {
            var upd = new EntityUpdation<TElement, TField>(fieldSelector, entityUpdate, null);
            this.EntityFieldModifications.Add(upd);
            return this;
        }

        public Modification<TContainer, TElement> Update<TField>(Expression<Func<TElement, TField>> fieldSelector,
            Action<TField> entityUpdate, Action<EntityUpdation<TElement, TField>> entityFieldUpdater) 
            where TField : class, new()
        {
            var upd = new EntityUpdation<TElement, TField>(fieldSelector, entityUpdate, null);
            entityFieldUpdater(upd);
            this.EntityFieldModifications.Add(upd);
            return this;
        }

        public Modification<TContainer, TElement> Update<TField>(Expression<Func<TElement, TField>> fieldSelector,
            Action<TField, TElement> entityUpdateReferencingParent) where TField : class, new()
        {
            var upd = new EntityUpdation<TElement, TField>(fieldSelector, entityUpdateReferencingParent, null);
            this.EntityFieldModifications.Add(upd);
            return this;
        }

        public Modification<TContainer, TElement> Update<TField>(Expression<Func<TElement, TField>> fieldSelector,
            Action<TField, TElement> entityUpdateReferencingParent, Action<EntityUpdation<TElement, TField>> entityFieldUpdater)
            where TField : class, new()
        {
            var upd = new EntityUpdation<TElement, TField>(fieldSelector, entityUpdateReferencingParent, null);
            entityFieldUpdater(upd);
            this.EntityFieldModifications.Add(upd);
            return this;
        }
    }
}
