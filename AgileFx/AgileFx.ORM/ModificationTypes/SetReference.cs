﻿/* AgileFx Version 2.0
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
    public class SetReference<TContainer, TElement> : EntityUpdation<TContainer, TElement>
        where TContainer : class, new()
        where TElement : class, IEntity, new()
    {
        public Expression<Func<TElement, bool>> Predicate { get; set; }

        public SetReference(Expression<Func<TContainer, IEnumerable<TElement>>> collectionSelector,
            Expression<Func<TElement, bool>> predicate, Action<TElement> entityUpdate, params IModification[] entityFieldModifications) :
            base(collectionSelector, entityUpdate, entityFieldModifications)
        {
            this.Predicate = predicate;
        }

        public SetReference(Expression<Func<TContainer, TElement>> fieldSelector,
            Expression<Func<TElement, bool>> predicate, Action<TElement> entityUpdate, params IModification[] entityFieldModifications) :
            base(fieldSelector, entityUpdate, entityFieldModifications)
        {
            this.Predicate = predicate;
        }

        public override ModificationType Type
        {
            get
            {
                return ModificationType.SetReference;
            }
        }

        public override TElement GetElement(object container, EntityContext context)
        {
            return context.CreateQuery<TElement>().Where(this.Predicate).Single();
        }
    }
}
