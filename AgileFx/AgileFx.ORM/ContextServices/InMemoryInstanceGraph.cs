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
using System.Collections;

using AgileFx.ORM.Serialization;
using AgileFx.ORM.Utils;
using System.Reflection;

namespace AgileFx.ORM.ContextServices
{
    public class InMemoryInstanceGraph : IObjectGraph
    {
        private object source;
        public InMemoryInstanceGraph(object source)
        {
            this.source = source;
        }

        public bool IsAnonymous
        {
            get { return (source != null) ? TypesUtil.IsCompilerGeneratedAnonymousType(source.GetType()) : false; }
        }

        public bool IsCollection
        {
            get { return TypesUtil.IsNonPrimitiveCollection(this.GetInstanceType()); }
        }

        public List<IObjectGraph> GetSourceCollection()
        {
            if (!IsCollection)
                throw new InvalidOperationException("The given object graph is not a collection");
            var source = new List<IObjectGraph>();
            foreach (var item in (source as IEnumerable))
                source.Add(new InMemoryInstanceGraph(item));
            return source;
        }

        public object GetInstance()
        {
            return source;
        }

        public Type GetInstanceType()
        {
            return (source != null) ? source.GetType() : null;
        }

        //All Entities need to be bound to the context.
        public PropertyInfo[] GetPropertiesNeedingContextBinding()
        {   
            if (source == null) return null;
            if (source is IEntity)
                return source.GetType().GetProperties()
                        .Where(p => typeof(IEntity).IsAssignableFrom(p.PropertyType) || typeof(IEntityCollection).IsAssignableFrom(p.PropertyType)).ToArray();
            else
                return source.GetType().GetProperties().Where(p => !TypesUtil.IsPrimitiveDataType(p.PropertyType)).ToArray();
        }

        public object GetPropertyValue(System.Reflection.PropertyInfo property)
        {
            return (source != null) ? property.GetValue(source, null) : null;
        }

        public IObjectGraph GetObjectGraphForProperty(System.Reflection.PropertyInfo property)
        {
            return new InMemoryInstanceGraph(property.GetValue(source, null));
        }

        public IEnumerable<IObjectGraph> GetObjectGraphForCollection(System.Reflection.PropertyInfo property)
        {
            return (property.GetValue(source, null) as IEnumerable).Cast<object>()
                .Select(o => new InMemoryInstanceGraph(o) as IObjectGraph).ToList();
        }
    }
}
