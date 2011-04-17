/* AgileFx Version 2.0
 * An open-source framework for rapid development of applications and services using Microsoft.net
 * Developed by: AgileHead
 * Website: www.agilefx.org
 * This component is licensed under the terms of the Apache 2.0 License.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Reflection;

namespace AgileFx.ORM.ModificationTypes
{
    public class EntityUpdation<TContainer, TElement> : Modification<TContainer, TElement>
        where TContainer : class, new()
        where TElement : class, new()
    {
        public EntityUpdateFunc<TElement, TContainer> EntityUpdates { get; set; }

        public EntityUpdation(Expression<Func<TContainer, TElement>> fieldSelector, Action<TElement> entityUpdate,
            IModification[] innerUpdates)
        {
            this.FieldReference = new FieldReference<TContainer, TElement>(fieldSelector);
            this.EntityUpdates = new EntityUpdateFunc<TElement, TContainer>(entityUpdate);
            if (null != innerUpdates) EntityFieldModifications.AddRange(innerUpdates);
        }

        public EntityUpdation(Expression<Func<TContainer, IEnumerable<TElement>>> collectionSelector, Action<TElement> entityUpdate,
            IModification[] innerUpdates)
        {
            this.FieldReference = new FieldReference<TContainer, TElement>(collectionSelector);
            this.EntityUpdates = new EntityUpdateFunc<TElement, TContainer>(entityUpdate);
            if (null != innerUpdates) EntityFieldModifications.AddRange(innerUpdates);
        }

        public EntityUpdation(Expression<Func<TContainer, TElement>> fieldSelector, Action<TElement, TContainer> entityUpdateReferencingParent,
            IModification[] innerUpdates)
        {
            this.FieldReference = new FieldReference<TContainer, TElement>(fieldSelector);
            this.EntityUpdates = new EntityUpdateFunc<TElement, TContainer>(entityUpdateReferencingParent);
            if (null != innerUpdates) EntityFieldModifications.AddRange(innerUpdates);
        }

        public EntityUpdation(Expression<Func<TContainer, IEnumerable<TElement>>> collectionSelector, Action<TElement, TContainer> entityUpdateReferencingParent,
            IModification[] innerUpdates)
        {
            this.FieldReference = new FieldReference<TContainer, TElement>(collectionSelector);
            this.EntityUpdates = new EntityUpdateFunc<TElement, TContainer>(entityUpdateReferencingParent);
            if (null != innerUpdates) EntityFieldModifications.AddRange(innerUpdates);
        }

        public void ApplyUpdates(object container, TElement obj)
        {
            if (null != this.EntityUpdates)
            {
                this.EntityUpdates.Run(container, obj);
                this.EntityUpdates = null;
            }
        }

        public override ModificationType Type
        {
            get
            {
                return ModificationType.Modification;
            }
        }

        public override object _applyChanges(object container, EntityContext context)
        {
            if (ApplyChangesOverride != null)
            {
                //Most entities call Context.ApplyChanges after their custom code is run.
                //So, this gets called again leading to a stack overflow.
                var func = ApplyChangesOverride;
                ApplyChangesOverride = null;

                return func(this, container);
            }
            else
            {
                //Apply Updates on the element.
                var element = GetElement(container, context);

                if (null != EntityUpdates) ApplyUpdates(container, element);
                FieldReference.SetValue(container, element);
                ApplyInnerModifications(element, context);
                return element;
            }
        }
    }
}
