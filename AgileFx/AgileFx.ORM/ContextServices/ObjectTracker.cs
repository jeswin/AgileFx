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
using AgileFx.ORM.Utils;
using AgileFx.ORM.ObjectComposition;
using AgileFx.ORM.Mapping;
using System.Reflection;
using System.Linq.Expressions;
using AgileFx.ORM.Reflection;

namespace AgileFx.ORM.ContextServices
{
    public class ObjectTracker
    {
        EntityContext entityContext;
        private Dictionary<Type, List<IntermediateEntity>> trackedIntermediateEntities;

        public ObjectTracker(EntityContext entityContext)
        {
            this.entityContext = entityContext;
            this.trackedIntermediateEntities = new Dictionary<Type, List<IntermediateEntity>>();
        }

        public virtual T GetObjectByKey<T>(Dictionary<string, object> keyValues)
        {
            var intermediateType = entityContext._InternalServices.TypeTranslationUtil.GetMapping<IEntityMapping>(typeof(T)).IntermediateEntityType;

            if (!trackedIntermediateEntities.ContainsKey(intermediateType)) return default(T);

            var propValues = keyValues
                .Select(kvp => new KeyValuePair<PropertyInfo, object>(intermediateType.GetProperty(kvp.Key), kvp.Value)).ToList();

            foreach (IntermediateEntity intEntity in trackedIntermediateEntities[intermediateType])
            {
                if (propValues.All(kvp => kvp.Key.GetValue(intEntity, null).Equals(kvp.Value)))
                {
                    return (T)Activator.CreateInstance(typeof(T), intEntity);
                }
            }

            return default(T);
        }

        public virtual void Add(IntermediateEntity intermediateEntity)
        {
            var intermediateType = intermediateEntity.GetType();
            if (!trackedIntermediateEntities.ContainsKey(intermediateType))
                trackedIntermediateEntities.Add(intermediateType, new List<IntermediateEntity>());

            trackedIntermediateEntities[intermediateType].Add(intermediateEntity);
        }

        public virtual IEntity GetCopyInContext(IEntity instance)
        {
            return GetCopyInContext(instance, entityContext._InternalServices.TypeTranslationUtil.GetIdentityProperties(instance.GetType()));
        }

        public IEntity GetCopyInContext(IEntity instance, IEnumerable<PropertyInfo> keyProperties)
        {
            var entityKeyValues = new Dictionary<string, object>();
            
            foreach(var property in keyProperties)
                entityKeyValues.Add(property.Name, property.GetValue(instance, null));

            var getObjectByKeyMethod = MethodFinder.ObjectTrackerMethods.GetObjectByKey(instance.GetType());
            var entity = getObjectByKeyMethod.Invoke(this, new object[] { entityKeyValues }) as IEntity;

            return entity;
        }

        public IEnumerable<object> GetAllItems()
        {
            foreach (var list in trackedIntermediateEntities.Values)
            {
                foreach(var item in list)
                {
                    yield return item;
                }
            }
        }
    }
}
