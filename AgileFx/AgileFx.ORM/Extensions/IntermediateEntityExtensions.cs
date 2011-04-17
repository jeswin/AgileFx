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
using System.Reflection;
using System.Runtime.Serialization;

using IQToolkit;
using AgileFx.ORM.ObjectComposition;
using AgileFx.ORM.Utils;

namespace AgileFx.ORM
{
    public static class IntermediateEntityExtensions
    {
        public static TEntity GetEntity<TEntity, TTableEntity>(this IntermediateEntity<TEntity, TTableEntity> intermediateEntity)
            where TEntity : class, IEntity
            where TTableEntity : ITableEntity
        {
            return (null == intermediateEntity) ? (TEntity)null : intermediateEntity._entity;
        }

        public static void Deserialize<T, TIntermediate, TPOCO>(this TIntermediate source, SerializationInfo info)
            where T : IEntity<TIntermediate>
            where TIntermediate : IntermediateEntity<T>
            where TPOCO : IPOCO<T>, new()
        {
            foreach (PropertyInfo prop in typeof(TPOCO).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                .Where(p => !typeof(IPOCOContainer).IsAssignableFrom(p.PropertyType)))
            {
                var fieldOnIntermediate = TypesUtil.GetIntermediateContainer(typeof(TIntermediate), prop.Name).GetValue(source) as IEntityField;
                fieldOnIntermediate.MaterializeSet(info.GetValue(prop.Name, prop.PropertyType));
            }
        }
    }
}
