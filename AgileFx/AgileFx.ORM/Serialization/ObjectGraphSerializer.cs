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
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

using AgileFx.ORM;
using AgileFx.ORM.Utils;
using AgileFx.ORM.Mapping;
using AgileFx.ORM.Materialization;
using AgileFx.ORM.Reflection;
using AgileFx.ORM.ObjectComposition;
using System.Runtime.Serialization;
using AgileFx.ORM.ContextServices;

namespace AgileFx.ORM.Serialization
{
    public class ObjectGraphSerializer
    {
        private IFormatter formatter;

        public ObjectGraphSerializer(IFormatter formatter)
        {
            this.formatter = formatter;
        }

        public SerializedForm<T> Serialize<T>(T t)
        {
            var sourceWrapper = CreateObjectGraph(t);
            using (var ms = new System.IO.MemoryStream())
            {
                formatter.Serialize(ms, sourceWrapper);
                return new SerializedForm<T>(ms.ToArray());
            }
        }

        //Serialization of entities should consider the included fields.
        // -    If A.B is defined as an include on A at the time of materialization, B should also be serialized
        //       and re-attached when A is deserialized.
        private SerializableObjectGraph CreateObjectGraph(object entity)
        {
            var sourceType = entity.GetType();

            //If this is a collection, create a graph for each item in the collection.
            if (TypesUtil.IsNonPrimitiveCollection(sourceType))
            {
                var list = new List<IObjectGraph>();
                foreach (object obj in entity as IEnumerable)
                    list.Add(CreateObjectGraph(obj));
                return new SerializableCollection
                {
                    Collection = list,
                    InstanceType = sourceType
                };
            }
            else
            {
                var serializableInstance = new SerializableInstance { Object = entity, InstanceType = entity.GetType() };
                if (entity is IEntity)
                {
                    var materializationIncludes = GetIncludeExpressions((entity as IEntity)._getIntermediateEntity().IncludeDirectives);
                    foreach (var includeExps in materializationIncludes)
                        AddIncludes(serializableInstance, includeExps.FieldChain, includeExps.IncludesInCollection);
                }
                else
                {
                    if (TypesUtil.IsCompilerGeneratedAnonymousType(sourceType))
                    {
                        serializableInstance.Object = null;
                        serializableInstance.InstanceType = sourceType;
                    }

                    var properties = (entity is IEntity) ? sourceType.GetProperties()
                        .Where(p => typeof(IEntity).IsAssignableFrom(p.PropertyType) 
                            || typeof(IEntityCollection).IsAssignableFrom(p.PropertyType)).ToArray()
                            : (TypesUtil.IsPrimitiveDataType(sourceType) || sourceType.IsArray || TypesUtil.IsNonPrimitiveCollection(sourceType) 
                                ? new PropertyInfo[] {} : sourceType.GetProperties());

                    foreach (var prop in properties)
                    {
                        var propVal = prop.GetValue(entity, null);

                        //Primitive types can be stored as is.
                        if (TypesUtil.IsPrimitiveDataType(prop.PropertyType))
                        {
                            serializableInstance.IncludedMembers.Add
                                (
                                    prop,
                                    new SerializableInstance { Object = propVal, InstanceType = prop.PropertyType }
                                );
                            continue;
                        }

                        //This is a single object (possibly an entity); but not a primitive type or a collection
                        if (!TypesUtil.IsNonPrimitiveCollection(prop.PropertyType))
                        {
                            serializableInstance.IncludedMembers.Add
                                (
                                    prop,
                                    CreateObjectGraph(propVal)
                                );
                        }

                        //Collection of non-primitive types
                        else if (TypesUtil.IsNonPrimitiveCollection(prop.PropertyType))
                        {
                            var itemsInCollection = new List<IObjectGraph>();

                            foreach (var item in (propVal as IEnumerable)) 
                                itemsInCollection.Add(CreateObjectGraph(item));

                            serializableInstance.IncludedMembers.Add
                                (
                                    prop, 
                                    new SerializableCollection 
                                    { 
                                        Collection = itemsInCollection, 
                                        InstanceType = prop.PropertyType 
                                    } 
                                );
                        }
                    }
                }
                return serializableInstance;
            }
        }


        private IEnumerable<MaterializationIncludes> GetIncludeExpressions(IEnumerable<IncludeDirective> directives)
        {
            var collIncludeExpressions = new Dictionary<MemberExpression, List<MemberExpression>>();
            var fieldIncludeExpressions = new List<MemberExpression>();
            foreach (var directive in directives)
            {
                if (directive is IIncludeInCollectionDirective)
                {
                    var collSelector = directive.GetSelector().Body as MemberExpression;
                    var existingKey = collIncludeExpressions.Keys.FirstOrDefault(exp => ExpressionUtil.IsEquivalent(exp, collSelector));
                    if (existingKey == null)
                    {
                        collIncludeExpressions.Add(collSelector, new List<MemberExpression>());
                    }
                    collIncludeExpressions[existingKey ?? collSelector].Add((directive as IIncludeInCollectionDirective).GetFieldSelector().Body as MemberExpression);
                }
                else
                {
                    var fieldSelector = directive.GetSelector().Body as MemberExpression;
                    if (!fieldIncludeExpressions.Any(exp => ExpressionUtil.IsEquivalent(fieldSelector, exp) || fieldSelector.IsPartOf(exp)))
                        fieldIncludeExpressions.Add(fieldSelector);
                }
            }

            var includes = new List<MaterializationIncludes>();
            foreach (var exp in fieldIncludeExpressions)
            {
                var matchingCollKey = collIncludeExpressions.Keys.FirstOrDefault(x => ExpressionUtil.IsEquivalent(exp, x));
                includes.Add(new MaterializationIncludes(exp, (matchingCollKey != null) ? collIncludeExpressions[matchingCollKey] : null));
            }
            return includes;
        }

        private bool IsPropertyLoadedOnEntity(object instance, PropertyInfo propertyInfo)
        {
            var param = Expression.Parameter(instance.GetType(), "t");
            var fieldSelector = Expression.Lambda(Expression.MakeMemberAccess(param, propertyInfo), param);
            var isLoadedMethod = MethodFinder.ModelEntityExtensionsMethods.IsLoaded(instance.GetType(), propertyInfo.PropertyType);
            return (bool)isLoadedMethod.Invoke(instance, new object[] { instance, fieldSelector });
        }

        private void AddIncludes(SerializableObjectGraph objectGraph, MemberExpression includedPropertyChain, IEnumerable<MemberExpression> collectionIncludes)
        {
            var memberExp = ExpressionUtil.GetInnermostMemberExpression(includedPropertyChain);
            var propInfo = objectGraph.GetInstance().GetType().GetProperty(memberExp.Member.Name);
            if (IsPropertyLoadedOnEntity(objectGraph.GetInstance(), propInfo))
            {
                var propVal = propInfo.GetValue(objectGraph.GetInstance(), null);
                if (propVal == null) return;

                if (TypesUtil.IsNonPrimitiveCollection(propInfo.PropertyType))
                {
                    var collInclude = objectGraph.IncludedMembers.ContainsKey(propInfo) ? 
                        objectGraph.IncludedMembers[propInfo] : null;

                    if (collInclude == null)
                    {
                        var list = new List<IObjectGraph>();
                        
                        foreach (var item in (propVal as IEnumerable)) 
                            list.Add(new SerializableInstance { Object = item, InstanceType = item.GetType() });

                        collInclude = new SerializableCollection 
                                { 
                                    Collection = list, 
                                    InstanceType = propInfo.PropertyType 
                                };

                        objectGraph.IncludedMembers.Add(propInfo, collInclude);
                    }

                    if (collectionIncludes != null)
                    {
                        foreach (SerializableObjectGraph instanceWrapper in (collInclude as SerializableCollection).Collection)
                            foreach (var include in collectionIncludes)
                                AddIncludes(instanceWrapper, include, null);
                    }
                }
                else
                {
                    var fieldInclude = objectGraph.IncludedMembers.ContainsKey(propInfo) ?
                        objectGraph.IncludedMembers[propInfo] : null;

                    if (fieldInclude == null)
                    {
                        fieldInclude = new SerializableInstance { Object = propVal, InstanceType = propInfo.PropertyType };
                        objectGraph.IncludedMembers.Add(propInfo, fieldInclude);
                    }

                    if (includedPropertyChain.GetDepth() > 1)
                    {
                        AddIncludes(fieldInclude,
                            ExpressionUtil.ReplaceInnermostMemberExpression(includedPropertyChain, Expression.Parameter(memberExp.Type, "x")) as MemberExpression, 
                            collectionIncludes);
                    }
                }
            }
        }
    }
}