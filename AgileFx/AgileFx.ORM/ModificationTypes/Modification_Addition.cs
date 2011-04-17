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
        //Add an entity as a field. Does not update any value inside it.
        public Modification<TContainer, TElement> Add<TField>(Expression<Func<TElement, TField>> fieldSelector)
            where TField : class, IEntity, new()
        {
            var upd = new EntityAddition<TElement, TField>(fieldSelector, x => { });
            this.EntityFieldModifications.Add(upd);
            return this;
        }

        //With a field update
        public Modification<TContainer, TElement> Add<TField>(Expression<Func<TElement, TField>> fieldSelector,
            Action<TField> entityUpdate) where TField : class, IEntity, new()
        {
            var upd = new EntityAddition<TElement, TField>(fieldSelector, entityUpdate);
            this.EntityFieldModifications.Add(upd);
            return this;
        }

        //Can update fields inside fields....
        public Modification<TContainer, TElement> Add<TField>(Expression<Func<TElement, TField>> fieldSelector,
            Action<TField> entityUpdate, Action<EntityAddition<TElement, TField>> entityFieldUpdater)
            where TField : class, IEntity, new()
        {
            var upd = new EntityAddition<TElement, TField>(fieldSelector, entityUpdate);
            entityFieldUpdater(upd);
            this.EntityFieldModifications.Add(upd);
            return this;
        }

        //The update gets a reference to the parent object as well....
        public Modification<TContainer, TElement> Add<TField>(Expression<Func<TElement, TField>> fieldSelector,
                    Action<TField, TElement> entityUpdateReferencingParent) where TField : class, IEntity, new()
        {
            var upd = new EntityAddition<TElement, TField>(fieldSelector, entityUpdateReferencingParent);
            this.EntityFieldModifications.Add(upd);
            return this;
        }

        //The update gets a reference to the parent object as well....
        public Modification<TContainer, TElement> Add<TField>(Expression<Func<TElement, TField>> fieldSelector,
            Action<TField, TElement> entityUpdateReferencingParent, Action<EntityAddition<TElement, TField>> entityFieldUpdater)
            where TField : class, IEntity, new()
        {
            var upd = new EntityAddition<TElement, TField>(fieldSelector, entityUpdateReferencingParent);
            entityFieldUpdater(upd);
            this.EntityFieldModifications.Add(upd);
            return this;
        }


        public Modification<TContainer, TElement> Add<TField>(Expression<Func<TElement, IEnumerable<TField>>> collectionSelector,
            params Action<TField>[] entityUpdates) where TField : class, IEntity, new()
        {
            foreach (var func in entityUpdates)
            {
                this.EntityFieldModifications.Add(new EntityAddition<TElement, TField>(collectionSelector, func));
            }
            return this;
        }

        public Modification<TContainer, TElement> Add<TField>(Expression<Func<TElement, IEnumerable<TField>>> collectionSelector,
            params Action<TField, TElement>[] entityUpdatesReferencingParent) where TField : class, IEntity, new()
        {
            foreach (var func in entityUpdatesReferencingParent)
            {
                this.EntityFieldModifications.Add(new EntityAddition<TElement, TField>(collectionSelector, func));
            }
            return this;
        }

    }
}
