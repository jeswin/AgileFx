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

using AgileFx.ORM.ModificationTypes;

namespace AgileFx.ORM
{
    public static class Modification
    {
        public static EntityAddition<TElement, TElement> AddEntity<TElement>
            (Action<TElement> entityUpdate, params IModification[] entityFieldUpdates)
            where TElement : class, IEntity, new()
        {
            return new EntityAddition<TElement, TElement>(p => p, entityUpdate, entityFieldUpdates);
        }

        public static EntityUpdation<TElement, TElement> UpdateEntity<TElement>
            (Action<TElement> entityUpdate, params IModification[] entityFieldUpdates) where TElement : class, new()
        {
            return new EntityUpdation<TElement, TElement>(p => p, entityUpdate, entityFieldUpdates);
        }
    }
}
