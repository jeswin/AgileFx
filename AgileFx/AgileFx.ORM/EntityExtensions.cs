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
using AgileFx.ORM.ModificationTypes;

namespace AgileFx.ORM
{
    public static class EntityExtensions
    {
        public static void Save<TContainer, TElement>(this TElement element, Modification<TContainer, TElement> mod, EntityContext ctx)
            where TContainer : class, new()
            where TElement : class, new()
        {
            mod.ApplyChanges(element, ctx);
        }

        public static void Delete(this IEntity obj, EntityContext ctx)
        {
            ctx.DeleteObject(obj);
        }
    }
}
