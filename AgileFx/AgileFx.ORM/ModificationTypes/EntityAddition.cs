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

namespace AgileFx.ORM.ModificationTypes
{
    public class EntityAddition<TContainer, TElement> : EntityUpdation<TContainer, TElement>
        where TContainer : class, new()
        where TElement : class, IEntity, new()
    {
        public EntityAddition(Expression<Func<TContainer, IEnumerable<TElement>>> collectionSelector,
            Action<TElement> entityUpdate, params IModification[] entityFieldModifications) :
            base(collectionSelector, entityUpdate, entityFieldModifications)
        {
        }

        public EntityAddition(Expression<Func<TContainer, TElement>> fieldSelector,
            Action<TElement> entityUpdate, params IModification[] entityFieldModifications) :
            base(fieldSelector, entityUpdate, entityFieldModifications)
        {
        }

        public EntityAddition(Expression<Func<TContainer, IEnumerable<TElement>>> collectionSelector,
            Action<TElement, TContainer> entityUpdateReferencingParent, params IModification[] entityFieldModifications) :
            base(collectionSelector, entityUpdateReferencingParent, entityFieldModifications)
        {
        }

        public EntityAddition(Expression<Func<TContainer, TElement>> fieldSelector,
            Action<TElement, TContainer> entityUpdateReferencingParent, params IModification[] entityFieldModifications) :
            base(fieldSelector, entityUpdateReferencingParent, entityFieldModifications)
        {
        }

        public override ModificationType Type
        {
            get
            {
                return ModificationType.Add;
            }
        }

        public override TElement GetElement(object container, EntityContext context)
        {
            return new TElement();
        }
    }
}
