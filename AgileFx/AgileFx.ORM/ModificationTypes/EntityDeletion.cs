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
    public class EntityDeletion<TContainer, TElement> : 
        Modification<TContainer, TElement>
        where TContainer : class, new()
        where TElement : class, new()
    {
        Func<TElement, bool> collectionRemoveCondition { get; set; }
        
        public EntityDeletion(Expression<Func<TContainer, TElement>> fieldSelector)
        {
            this.FieldReference = new FieldReference<TContainer,TElement>(fieldSelector);
        }

        public EntityDeletion(Expression<Func<TContainer, IEnumerable<TElement>>> collectionSelector, 
            Func<TElement, bool> predicate)
        {
            this.FieldReference = new FieldReference<TContainer,TElement>(collectionSelector);
            collectionRemoveCondition = predicate;
        }

        public override object _applyChanges(object container, EntityContext context)
        {
            object removee = null;
            if (FieldReference.SelectorType == FieldSelectorType.FieldSelector)
                removee = FieldReference.RemoveValue(container);
            else if (FieldReference.SelectorType == FieldSelectorType.CollectionSelector)
                removee = FieldReference.RemoveValueFromCollection(container, collectionRemoveCondition);
            return removee;
        }

        public override ModificationType Type
        {
            get
            {
                return ModificationType.Delete;
            }
        }
    }
}