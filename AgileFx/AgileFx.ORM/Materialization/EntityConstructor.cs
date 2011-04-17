/* AgileFx Version 2.0
 * An open-source framework for rapid development of applications and services using Microsoft.net
 * Developed by: AgileHead
 * Website: www.agilefx.org
 * This component is licensed under the terms of the Apache 2.0 License.
 */
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System;
using System.Collections;
using AgileFx.ORM.Mapping;
using AgileFx.ORM.ObjectComposition;
using AgileFx.ORM.Reflection;

namespace AgileFx.ORM.Materialization
{
    public class EntityConstructor
    {
        EntityContext entityContext;
        Type entityType;
        List<IEntityMapping> classMappings = new List<IEntityMapping>();
        
        public EntityConstructor(EntityContext entityContext, Type entityType)
        {
            this.entityContext = entityContext;
            this.entityType = entityType;

            //Pre-load the ClassMapping data
            var e = entityType;
            
            do
            {
                classMappings.Add(entityContext._InternalServices.TypeTranslationUtil.GetMapping<IEntityMapping>(e));
                e = e.BaseType;
            } while (e.BaseType != null);
        }

        public IEntity GetOrCreateEntity(object[] sources)
        {
            if (sources == null || sources.Count() == 0) return null;

            //sources represent the full inheritance hierarchy required to create the instance.
            //eg: Lawyer will have instanced of the table entities: lawyer, professional, user.

            IntermediateEntity intermediateEntity = null;
            var last = sources.Length - 1;
            for (int i = last; i >= 0; i--)
            {
                ITableEntity src = sources[i] as ITableEntity;

                if (src._getIntermediateEntity() == null)
                {
                    intermediateEntity = i == last ? Activator.CreateInstance(classMappings[i].IntermediateEntityType, new object[] { sources[i] }) as IntermediateEntity
                        : Activator.CreateInstance(classMappings[i].IntermediateEntityType, new object[] { sources[i], intermediateEntity }) as IntermediateEntity;

                    entityContext.AttachContext(intermediateEntity);

                    src._setIntermediateEntity(intermediateEntity);
                }
                else
                    intermediateEntity = src._getIntermediateEntity();
            }

            //See if the type has a TypeFactory. If so, it decides which Type to instantiate.
            IEntity entity = Activator.CreateInstance(classMappings[0].EntityType, new object[] { intermediateEntity }) as IEntity;

            //If there is a type factory, allow the typefactory to suggest the type to create (or recreate).
            var typeFactory = entityContext._InternalServices.TypeTranslationUtil.GetTypeFactory(entityType);
            if (typeFactory != null)
            {
                var typeToConstruct = typeFactory.GetTypeOf(entity);
                if (typeToConstruct != null)
                {
                    entity = Activator.CreateInstance(typeToConstruct) as IEntity;
                    MethodFinder.IEntity._setIntermediateEntity(typeToConstruct).Invoke(entity, new object[] { intermediateEntity });                        
                }
            }

            return entity;
        }
    }
}