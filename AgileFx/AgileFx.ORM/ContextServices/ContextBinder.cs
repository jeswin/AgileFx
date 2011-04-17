/* AgileFx Version 2.0
 * An open-source framework for rapid development of applications and services using Microsoft.net
 * Developed by: AgileHead
 * Website: www.agilefx.org
 * This component is licensed under the terms of the Apache 2.0 License.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using AgileFx.ORM.ObjectComposition;
using System.Linq.Expressions;
using AgileFx.ORM.Reflection;
using AgileFx.ORM.Utils;
using IQToolkit;
using System.Runtime.CompilerServices;
using AgileFx.ORM.Mapping;
using AgileFx.ORM.Parallelization;

namespace AgileFx.ORM.ContextServices
{
    //Binds a SerializedObjectGraph to a given context.
    public class ContextBinder
    {
        EntityContext targetContext;

        public ContextBinder(EntityContext context)
        {
            this.targetContext = context;
        }

        public T AttachGraphToContext<T>(IObjectGraph source)
        {
            return (T)LoadObject(source, new Dictionary<object, object>());
        }

        public object SwitchContext(object objects)
        {
            var returnType = objects.GetType();
            var parsedObjects = new Dictionary<object, object>();

            if (!TypesUtil.IsNonPrimitiveCollection(returnType))
            {
                return LoadObject(new InMemoryInstanceGraph(objects), parsedObjects);
            }
            else
            {
                var list = new List<object>();
                foreach (var objSource in objects as IEnumerable)
                    list.Add(LoadObject(new InMemoryInstanceGraph(objSource), parsedObjects));

                return FillEnumerable(objects, list);
            }
        }

        protected object LoadObject(IObjectGraph source, Dictionary<object, object> parsedObjects)
        {
            if (!source.IsAnonymous)
            {
                var instance = source.GetInstance();
                if (instance is IEntity)
                {
                    instance = GetCopyInContextOrAttach(instance as IEntity);
                }

                LoadIncludes(source, instance, parsedObjects);

                return instance;
            }

            else
            {
                //Anonymous Types
                var anonType = source.GetInstanceType();
                var ctor = anonType.GetConstructors().Single();

                var deserializedIncludes = new Dictionary<PropertyInfo, object>();

                foreach (var property in source.GetPropertiesNeedingContextBinding())
                {
                    if (!TypesUtil.IsNonPrimitiveCollection(property.PropertyType))
                    {
                        var propertyValue = LoadObject(source.GetObjectGraphForProperty(property), parsedObjects);
                        deserializedIncludes.Add(property, propertyValue);
                    }
                    else
                    {
                        var collectionItems = source.GetObjectGraphForCollection(property)
                            .Select(itemInfo => LoadObject(itemInfo, parsedObjects)).ToList();

                        var propertyValue = MakeEnumerable(property.PropertyType, collectionItems);

                        deserializedIncludes.Add(property, propertyValue);
                    }
                }

                var ctorArgs = deserializedIncludes.Select(kvp => kvp.Value).ToArray();

                return Activator.CreateInstance(anonType, ctorArgs);
            }
        }

        protected void LoadIncludes(IObjectGraph source, object instance, Dictionary<object, object> parsedObjects)
        {
            if (parsedObjects.ContainsKey(instance))
                return;

            //Add it to the parsed objects list, so that we can avoid going through circular relationships.
            parsedObjects.Add(instance, null);

            if (source.IsCollection)
            {
                var sourceCollection = source.GetSourceCollection();
                var ctr = 0;
                foreach (var item in (instance as IEnumerable))
                {
                    LoadIncludes(sourceCollection[ctr++], item, parsedObjects);
                }
            }
            else
            {

                foreach (var property in source.GetPropertiesNeedingContextBinding())
                {
                    if (!TypesUtil.IsNonPrimitiveCollection(property.PropertyType))
                    {
                        //If the instance is an Entity, we treat property setting differently.
                        //  This is because setters will automatically trigger Reverse Reference setters.
                        if (instance is IEntity)
                        {
                            var propValFromSource = source.GetPropertyValue(property);
                            var loadedValue = property.GetValue(instance);

                            //If the property is already loaded in our current instance, we should not touch it. 
                            //  Yet, if they (current instance and the source) refer to the same value, we should attach includes.
                            //If they are NOT the same, we do nothing. (Since the user changed the property, can't overwrite.)
                            if (IsPropertyLoadedOnEntity(instance, property))
                            {
                                if (loadedValue != null && loadedValue is IEntity
                                    && ((IEntity)loadedValue).IsEquivalent((IEntity)propValFromSource))
                                {
                                    LoadIncludes(source.GetObjectGraphForProperty(property), loadedValue, parsedObjects);
                                }
                            }

                            //Property is not loaded on the current instance. Go ahead and add it via Materialization methods.
                            else
                            {
                                if (source.GetPropertyValue(property) != null)
                                {
                                    var propertyValue = LoadObject(source.GetObjectGraphForProperty(property), parsedObjects);
                                    var intermediateContainer = GetBackingContainerForProperty(instance as IEntity, property);
                                    intermediateContainer.MaterializationAddReference((propertyValue as IEntity)._getIntermediateEntity());
                                }
                            }
                        }

                        //Instance is not an IEntity, we can just set the property value.
                        else
                        {
                            if (source.GetPropertyValue(property) != null)
                            {
                                var propertyValue = LoadObject(source.GetObjectGraphForProperty(property), parsedObjects);
                                property.SetValue(instance, propertyValue);
                            }
                        }
                    }

                    //This property is a collection.
                    else
                    {
                        var entityList = source.GetObjectGraphForCollection(property)
                            .Select(collItemSource => LoadObject(collItemSource, parsedObjects)).ToList();

                        if (IsManyToMany(property))
                        {
                            //Many-to-Many collection can only exist in Entities, hence the cast.
                            AddToManyToManyCollection((IEntity)instance, property, entityList);
                        }
                        else
                        {
                            SetEnumerableProperty(instance, property, MakeEnumerable(property.PropertyType, entityList));
                        }
                    }
                }
            }
        }

        private void AddToManyToManyCollection(IEntity entity, PropertyInfo property, List<object> entityList)
        {
            var entityType = entity.GetType();

            var mapType = targetContext._InternalServices.TypeTranslationUtil.GetMapping<IModelEntityMapping>(entityType)
                .GetManyToManyRelationships()
                .Find(rel => (rel.RelatedEntitySelector.Body as MemberExpression).Member.Name == property.Name)
                .MapType;

            var mapEntityMapping = targetContext._InternalServices.TypeTranslationUtil.GetMapping<IMapEntityMapping>(mapType);
            var keys = mapEntityMapping.GetManyToManyKeyRelationships();
            var entityPropertyOnMap = (mapEntityMapping.GetManyToManyMaps().Find(m => m.EntitySelector.Body.Type == entityType)
                .EntitySelector.Body as MemberExpression).Member;
            var relatedEntityPropertyOnMap = (mapEntityMapping.GetManyToManyMaps().Find(m => m.EntitySelector.Body.Type != entityType)
                .EntitySelector.Body as MemberExpression).Member;

            //Keys = <EntityField, MapField> pair.
            var propertyMaps = keys.Select(keyMap => new KeyValuePair<MemberInfo, MemberInfo>
                (
                    (keyMap.EntityField.Body as MemberExpression).Member,
                    (keyMap.MapField.Body as MemberExpression).Member)
                )
                .ToList();

            Func<IEntity, IEntity> getMapObject = collItem =>
            {
                var map = Activator.CreateInstance(mapType) as IEntity;

                foreach (var propMap in propertyMaps)
                {
                    if (propMap.Key.DeclaringType == entityType)
                        propMap.Value.SetValue(map, propMap.Key.GetValue(entity));
                    else
                        propMap.Value.SetValue(map, propMap.Key.GetValue(collItem));
                }
                entityPropertyOnMap.SetValue(map, entity);
                relatedEntityPropertyOnMap.SetValue(map, collItem);

                return map;
            };

            //Get the container which holds the property value (of type IIntermediateEntityContainer)
            var manyToManyCollection = GetBackingContainerForProperty(entity, property);

            foreach (var collItem in entityList)
            {
                var map = getMapObject(collItem as IEntity);
                manyToManyCollection.MaterializationAddReference(GetCopyInContextOrAttach(map));
            }
        }

        private void SetEnumerableProperty(object instance, PropertyInfo property, object items)
        {
            var propVal = property.GetValue(instance, null);

            if (property.PropertyType.IsArray || propVal == null)
                property.SetValue(instance, items, null);

            //If the instance is IEntity, we should go through Materialization methods to avoid triggering Reverse Reference setting.
            else if (instance is IEntity)
            {
                var intermediateInstance = (instance as IEntity)._getIntermediateEntity();

                var collectionOnIntermediate = intermediateInstance.GetType().GetProperty(property.Name)
                    .GetValue(intermediateInstance) as IIntermediateEntityContainer;

                foreach (var item in (items as IEnumerable))
                    collectionOnIntermediate.MaterializationAddReference((item as IEntity)._getIntermediateEntity());
            }

            //Not an IEntity or an Array.
            //Assume this is a collection and call the "Add" method.
            else
            {
                var typeOfCollection = TypesUtil.GetGenericArgumentForBaseType(property.PropertyType, typeof(ICollection<>));

                var addMethod = propVal.GetType().GetMethod("Add", new Type[] { typeOfCollection });

                foreach (var item in items as IEnumerable)
                    addMethod.Invoke(propVal, new object[] { item });
            }
        }

        private bool IsPropertyLoadedOnEntity(object instance, PropertyInfo propertyInfo)
        {
            var param = Expression.Parameter(instance.GetType(), "x");
            var fieldSelector = Expression.Lambda(Expression.MakeMemberAccess(param, propertyInfo), param);
            var isLoadedMethod = MethodFinder.ModelEntityExtensionsMethods.IsLoaded(instance.GetType(), propertyInfo.PropertyType);
            return (bool)isLoadedMethod.Invoke(instance, new object[] { instance, fieldSelector });
        }

        public static object GetCollectionInstance(Type collectionType, List<object> collection)
        {
            //Arrays
            if (collectionType.IsArray)
            {
                var array = Activator.CreateInstance(collectionType, collection.Count) as Array;
                for (int i = 0; i < collection.Count; i++) array.SetValue(collection[i], i);
                return array;
            }

            //lists with indexers
            else if (collectionType.IsGenericType && collectionType.IsInterface && TypesUtil.IsNonPrimitiveCollection(collectionType))
            {
                var list = Activator.CreateInstance(typeof(List<>)
                    .MakeGenericType(TypesUtil.GetGenericArgumentForBaseType(collectionType, typeof(ICollection<>)))) as IList;
                collection.ForEach(item => list.Add(item));
                return list;
            }

            //Custom generic collections .. assume they have default constructor and Add method
            else if (collectionType.IsGenericType && TypesUtil.IsNonPrimitiveCollection(collectionType)
                && collectionType.GetConstructor(Type.EmptyTypes) != null)
            {
                var customCollection = Activator.CreateInstance(collectionType);
                var addMethod = collectionType.GetMethod("Add");
                collection.ForEach(item => addMethod.Invoke(customCollection, new object[] { item }));
                return customCollection;
            }

            throw new NotSupportedException("Only Arrays, Lists, ICollection<> and Custom collection types with default constructor are supported.");
        }

        private IIntermediateEntityContainer GetBackingContainerForProperty(IEntity entity, PropertyInfo property)
        {
            return targetContext._InternalServices.TypeTranslationUtil.MakeBackingContainerGetter(entity.GetType(), property)
                (entity._getIntermediateEntity()) as IIntermediateEntityContainer;
        }

        private bool IsManyToMany(MemberInfo memberInfo)
        {
            var classMapping = targetContext._InternalServices.TypeTranslationUtil
                .GetMapping<IModelEntityMapping>(memberInfo.DeclaringType);

            //Getting the many-to-many maps for the declaringType
            return (classMapping != null && classMapping.GetManyToManyRelationships()
                .Any(m => ((MemberExpression)m.RelatedEntitySelector.Body).Member.Name == memberInfo.Name));
        }

        private object GetCopyInContextOrAttach(IEntity instance)
        {
            var entity = targetContext._InternalServices.ObjectTracker.GetCopyInContext(instance);
            if (entity != null)
            {
                return entity;
            }
            else
            {
                //Attach object in 'unchanged' state.
                targetContext.AttachObject(instance, false);
                return instance;
            }
        }

        //Make an enumerable of type enumerableType and fill it with items.
        private object MakeEnumerable(Type enumerableType, List<object> items)
        {
            if (enumerableType.IsGenericType && TypesUtil.GenericTypeIsAssignableFrom(enumerableType, typeof(ICollection<>)))
            {
                return MakeListOfType(TypesUtil.GetGenericArgumentForBaseType(enumerableType, typeof(ICollection<>)), items);
            }

            else if (enumerableType.IsArray)
            {
                var list = MakeListOfType(enumerableType.GetElementType(), items);
                return list.GetType().GetMethod("ToArray").Invoke(list, null);
            }

            else
            {
                throw new NotSupportedException("The collection of type " + enumerableType.ToString() + " cannot be bound to the context.");
            }
        }

        //Fills an Enumerable (List, Collection etc) with items
        private object FillEnumerable(object enumerable, List<object> items)
        {
            var enumerableType = enumerable.GetType();
            if (enumerableType.IsArray) //Array
            {
                for (int i = 0; i < items.Count; i++)
                    (enumerable as Array).SetValue(items[i], i);
            }
            else if (typeof(IList).IsAssignableFrom(enumerableType))    //List and derived types .. which have indexers
            {
                for (int i = 0; i < items.Count; i++)
                    (enumerable as IList)[i] = items[i];
            }
            else if (enumerableType.IsGenericType && TypesUtil.IsNonPrimitiveCollection(enumerableType))  //Custom generic collections
            {
                enumerableType.GetMethod("Clear").Invoke(enumerable, null);
                var addMethod = enumerableType.GetMethod("Add");
                foreach (var item in items)
                    addMethod.Invoke(enumerable, new object[] { item });
            }
            else
            {
                throw new NotSupportedException("The collection of type " + enumerableType.ToString() + " cannot be bound to the context.");
            }

            return enumerable;
        }

        private IList MakeListOfType(Type listItemType, List<object> items)
        {
            var coll = Activator.CreateInstance(typeof(List<>).MakeGenericType(listItemType)) as IList;
            items.ForEach(item => coll.Add(item));
            return coll;
        }
    }
}
