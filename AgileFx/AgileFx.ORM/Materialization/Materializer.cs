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
using System.Linq.Expressions;
using IQToolkit;
using System.Runtime.CompilerServices;
using System.Reflection;

using AgileFx;
using AgileFx.ORM.QueryAnalysis;
using AgileFx.ORM.Utils;
using AgileFx.ORM.Mapping;
using AgileFx.ORM.ObjectComposition;
using AgileFx.ORM.QueryAnalysis.TypeTracking;

namespace AgileFx.ORM.Materialization
{
    public class MaterializationData
    {
        public ProjectedType ProjectedType { get; set; }
        public IEnumerable<object> ResultSet { get; set; }
        public List<PostProjectionLoadedResult> InheritanceChains { get; set; }
        public List<PostProjectionLoadedResult> ManyToManyMaps { get; set; }
    }

    //Many methods in the class return "Computations". This is done for performance reasons. 
    //The object is to divide a method into computations which are:
    //  a) Specific to the entire dataset
    //  b) Specific to each row in the dataset.
    //  The returned computations would have already computed Category A (entire dataset), so that they are not repeated which analyzing rows.
    public abstract class Materializer<T>
    {
        protected const string FIRST_FIELD_IN_PROJECTION = "Field0";
        protected EntityContext entityContext;

        public Materializer(EntityContext entityContext)
        {
            this.entityContext = entityContext;
        }

        //Returns a list of materialized entities for a given resultSet representing TableEntities.
        protected List<object> Materialize(IEnumerable<AnonymousType> resultSet, ProjectedType projectedType)
        {
            var getProjection = Get_GetProjection_Computation(projectedType, resultSet.Cast<object>());

            var objects = new List<object>();
            foreach (var result in resultSet)
            {
                var tableSource = new TableEntityRow(result);
                objects.Add(getProjection(tableSource.Values[0], tableSource));
            }
            return objects;
        }

        private Func<object, TableEntityRow, object> Get_GetProjection_Computation(ProjectedType projectedType, IEnumerable<object> resultSet)
        {
            //Field0 holds the originally requested object. The rest are IncludeBindings, InheritanceBindings etc.
            var field0 = projectedType.Fields.First(kvp => kvp.Key.Name == FIRST_FIELD_IN_PROJECTION).Value;

            return Get_GetOrCreateEntityOrCollection_Computation(field0,
                new MaterializationData
                {
                    ProjectedType = projectedType,
                    ResultSet = resultSet,
                    InheritanceChains = new InheritanceChainLoader<T>(projectedType, entityContext).LoadUnprojectedBindings(resultSet.Cast<object>().ToList()),
                    ManyToManyMaps = new ManyToManyMapLoader<T>(projectedType, entityContext).LoadUnprojectedBindings(resultSet.ToList()),
                });
        }

        private Func<object, TableEntityRow, object> Get_GetOrCreateEntityOrCollection_Computation(SimpleType trackedType, MaterializationData data)
        {
            if (trackedType is ProjectedType)
            {
                return Get_HandleProjectedType_Computation(trackedType, data);
            }
            else if (trackedType.Constructor != null)
            {
                return Get_CreateTypeWithConstructor_Computation(trackedType, data);
            }
            //When EnumerableItemType is not null... eg. Collections
            else if (trackedType.NonPrimitiveEnumerableItemType != null)
            {
                return Get_CreateCollection_Computation(trackedType, data);
            }
            //Simple Entities
            else if (typeof(IEntity).IsAssignableFrom(trackedType.Type))
            {
                return Get_GetOrCreateSimpleEntity_Computation(trackedType, data);
            }
            //Primitive types.. eg. int, long, string. Leave as is.
            else
            {
                return Get_CreatePrimitiveType_Computation();
            }
        }

        //If the ProjectedType appears again in the Type-Tracking Tree, handle it.
        private Func<object, TableEntityRow, object> Get_HandleProjectedType_Computation(SimpleType trackedType, MaterializationData data)
        {
            var valueGetter = Get_FindValueInTreeMatchingTrackedType_Computation(data.ProjectedType, trackedType);
            var newResults = data.ResultSet.Select(result => valueGetter(result)).ToList();

            var getProjectionValue = Get_GetProjection_Computation(trackedType as ProjectedType, newResults);
            return (tableEntity, source) =>
            {
                var newRow = new TableEntityRow(tableEntity as AnonymousType);
                return getProjectionValue(newRow.Values[0], newRow);
            };
        }

        //Where the user has mentioned a specific constructor in the query.
        private Func<object, TableEntityRow, object> Get_CreateTypeWithConstructor_Computation(SimpleType trackedType, MaterializationData data)
        {
            var fields = new Dictionary<MemberInfo, KeyValuePair<Func<object, object>, Func<object, TableEntityRow, object>>>();
            var constructorArgs = new Dictionary<MemberInfo, KeyValuePair<Func<object, object>, Func<object, TableEntityRow, object>>>();
            if (TypesUtil.IsCompilerGeneratedAnonymousType(trackedType.Type))
            {
                constructorArgs = Get_GetMembers_Computation(trackedType.GetAllMembers(), trackedType, data);
            }
            else
            {
                fields = Get_GetMembers_Computation(trackedType.Fields, trackedType, data);
            }

            Action<object, object, TableEntityRow> setCollectionItems = (newObj, projectedEntity, source) => { };
            if (trackedType.NonPrimitiveEnumerableItemType != null)
            {
                var getOrCreateCollectionItem = Get_GetOrCreateEntityOrCollection_Computation(trackedType.NonPrimitiveEnumerableItemType, data);
                var addMethod = trackedType.Constructor.DeclaringType.GetMethod("Add", new Type[] { trackedType.NonPrimitiveEnumerableItemType.Type });
                setCollectionItems = (newObj, projectedEntity, source) =>
                {
                    foreach (var tableCollectionItem in (projectedEntity as IEnumerable))
                    {
                        var collItem = getOrCreateCollectionItem(tableCollectionItem, source);
                        addMethod.Invoke(newObj, new object[] { collItem });
                    }
                };
            }

            Action<object, MemberInfo, object> setMemberValue = (obj, m, val) => trackedType.Constructor.DeclaringType.GetProperty(m.Name).SetValue(obj, val);
            return (projectedEntity, source) =>
            {
                var args = constructorArgs.Select(kvp => kvp.Value.Value(kvp.Value.Key(projectedEntity), source)).ToArray();
                var newObj = trackedType.Constructor.Invoke(args);
                foreach (var fld in fields)
                    setMemberValue(newObj, fld.Key, fld.Value.Value(fld.Value.Key(projectedEntity), source));
                setCollectionItems(newObj, projectedEntity, source);
                return newObj;
            };
        }

        //When EnumerableItemType is not null... eg. Collections
        private Func<object, TableEntityRow, object> Get_CreateCollection_Computation(SimpleType trackedType, MaterializationData data)
        {
            var collectionType = typeof(List<>).MakeGenericType(trackedType.NonPrimitiveEnumerableItemType.Type);
            if (entityContext._InternalServices.TypeTranslationUtil.IsEntityCollection(trackedType.Type))
            {
                var unprojectedBinding = data.ProjectedType.UnprojectedBindings
                    .Find(ub => (ub is UnprojectedCollectionBinding || ub is UnprojectedManyToManyBinding)
                        && ub.TargetBinding is QueryBinding && (ub.TargetBinding as QueryBinding).Value == trackedType);
                if (unprojectedBinding != null)
                {
                    var getUnprojectedCollection = Get_GetUnprojectedCollections_Computation(unprojectedBinding,
                        (unprojectedBinding is UnprojectedManyToManyBinding) ? data.ManyToManyMaps : data.InheritanceChains);

                    if (unprojectedBinding is UnprojectedManyToManyBinding)
                    {
                        var mappingMember = (trackedType.NonPrimitiveEnumerableItemType as ManyToManyMapType).Mapping.Name;
                        return (projectedEntity, source) =>
                        {
                            var maps = getUnprojectedCollection(projectedEntity, source);
                            var list = Activator.CreateInstance(collectionType) as IList;
                            foreach (var map in (maps as IList))
                                list.Add(map.GetType().GetProperty(mappingMember).GetValue(map));
                            return list;
                        };
                    }
                    else
                        return getUnprojectedCollection;
                }
            }

            var getOrCreateEntity = Get_GetOrCreateEntityOrCollection_Computation(trackedType.NonPrimitiveEnumerableItemType, data);
            var attachIncludes = Get_AttachIncludes_Computation(trackedType, data);
            return (projectedEntity, source) =>
            {
                var list = Activator.CreateInstance(collectionType) as IList;
                foreach (var collItem in (projectedEntity as IEnumerable))
                    list.Add(getOrCreateEntity(collItem, source));
                attachIncludes(list, source);
                return list;
            };
        }

        //Simple Entities
        private Func<object, TableEntityRow, object> Get_GetOrCreateSimpleEntity_Computation(SimpleType trackedType, MaterializationData data)
        {
            var getOrCreateEntity = Get_GetOrCreateEntity_Computation(trackedType, data);
            var attachIncludes = Get_AttachIncludes_Computation(trackedType, data);

            return (projectedEntity, source) =>
            {
                var newObj = getOrCreateEntity(projectedEntity, source);
                attachIncludes(newObj, source);
                return newObj;
            };
        }

        //Primitive types.. eg. int, long, string. Leave as is.
        private Func<object, TableEntityRow, object> Get_CreatePrimitiveType_Computation()
        {
            return (projectedEntity, source) => projectedEntity;
        }

        private Func<object, TableEntityRow, object> Get_GetOrCreateEntityOrCollection_Computation(IncludeBinding includeBinding, MaterializationData data)
        {
            if (entityContext._InternalServices.TypeTranslationUtil.IsEntityCollection(includeBinding.Type))
            {
                var unprojectedBinding = data.ProjectedType.UnprojectedBindings
                    .Find(ub => (ub is UnprojectedCollectionBinding || ub is UnprojectedManyToManyBinding) && ub.TargetBinding == includeBinding);
                if (unprojectedBinding != null)
                {
                    if (unprojectedBinding is UnprojectedManyToManyBinding)
                        return Get_GetUnprojectedCollections_Computation(unprojectedBinding, data.ManyToManyMaps);
                    else
                        return Get_GetUnprojectedCollections_Computation(unprojectedBinding, data.InheritanceChains);
                }
                else
                {
                    var collItemType = TypesUtil.GetGenericArgumentForBaseType(includeBinding.Type, typeof(ICollection<>));
                    var collectionEntityConstructor = new EntityConstructor(entityContext, collItemType);

                    return (tableEntity, source) =>
                    {
                        var list = new List<object>();
                        foreach (var item in (tableEntity as IEnumerable))
                            list.Add(collectionEntityConstructor.GetOrCreateEntity(new object[] { item }));
                        return list;
                    };
                }
            }

            return Get_GetOrCreateEntity_Computation(includeBinding, data);
        }

        private Func<object, object> Get_FindValueInTreeMatchingTrackedType_Computation(SimpleType sourceType, SimpleType trackedType)
        {
            if (sourceType == trackedType)
                return tableEntity => tableEntity;

            foreach (var kvp in sourceType.GetAllMembers())
            {
                var memberGetter = Get_FindValueInTreeMatchingTrackedType_Computation(kvp.Value, trackedType);
                if (memberGetter != null)
                {
                    var translatedValueGetter = sourceType.GetTranslatedMemberValue(kvp.Key,
                        entityContext._InternalServices.TypeTranslationUtil);
                    return tableEntity => memberGetter(translatedValueGetter(tableEntity));
                }
            }

            if (sourceType.NonPrimitiveEnumerableItemType != null)
            {
                var collectionItemGetter = Get_FindValueInTreeMatchingTrackedType_Computation(sourceType.NonPrimitiveEnumerableItemType, trackedType);
                if (collectionItemGetter != null)
                {
                    return tableEntity =>
                    {
                        var list = new List<object>();
                        foreach (var item in (tableEntity as IEnumerable)) list.Add(collectionItemGetter(item));
                        return list;
                    };
                }
            }

            return null;
        }

        //Computes the functions to get the member values of a simpletype
        private Dictionary<MemberInfo, KeyValuePair<Func<object, object>, Func<object, TableEntityRow, object>>>
            Get_GetMembers_Computation(Dictionary<MemberInfo, SimpleType> memberQuerableTypes, SimpleType trackedType, MaterializationData data)
        {
            var members = new Dictionary<MemberInfo, KeyValuePair<Func<object, object>, Func<object, TableEntityRow, object>>>();
            foreach (var memberInfo in memberQuerableTypes.Keys)
                members.Add(memberInfo, new KeyValuePair<Func<object, object>, Func<object, TableEntityRow, object>>
                    (trackedType.GetTranslatedMemberValue(memberInfo, entityContext._InternalServices.TypeTranslationUtil),
                        Get_GetOrCreateEntityOrCollection_Computation(memberQuerableTypes[memberInfo], data)));
            return members;
        }

        private Func<object, TableEntityRow, object> Get_GetUnprojectedCollections_Computation(IUnprojectedBinding binding, List<PostProjectionLoadedResult> postProjectionResults)
        {
            var list = postProjectionResults.Find(r => r.ProjectionBinding == binding).Value.Cast<IEntity>().ToList();
            return (projectedEntity, source) =>
            {
                var items = new List<object>();
                foreach (var projectedItem in (projectedEntity as IEnumerable))
                {
                    var item = list.Find(entity => entity._getIntermediateEntity()._getTableEntity().Equals(projectedItem));
                    if (item != null) items.Add(item);
                    else throw new ApplicationException("Data Inconsistency while loading Collection.");
                }
                return items;
            };
        }

        private Func<object, TableEntityRow, object> Get_GetOrCreateEntity_Computation(SimpleType trackedType, MaterializationData data)
        {
            var getbaseObjects = Get_GetBaseObjects_Computation(trackedType, data);
            var entityConstructor = new EntityConstructor(entityContext, trackedType.Type);

            return (projectedEntity, source) =>
            {
                if (projectedEntity == null) return null;
                var tableObjects = new List<object>();
                tableObjects.Add(projectedEntity);
                tableObjects.AddRange(getbaseObjects(source));
                return entityConstructor.GetOrCreateEntity(tableObjects.ToArray());
            };
        }

        private Func<object, TableEntityRow, object> Get_GetOrCreateEntity_Computation(IncludeBinding includeBinding, MaterializationData data)
        {
            var getbaseObjects = Get_GetBaseObjects_Computation(includeBinding, data);
            var entityConstructor = new EntityConstructor(entityContext, includeBinding.Type);

            return (projectedEntity, source) =>
            {
                if (projectedEntity == null) return null;
                var tableObjects = new List<object>();
                tableObjects.Add(projectedEntity);
                tableObjects.AddRange(getbaseObjects(source));
                return entityConstructor.GetOrCreateEntity(tableObjects.ToArray());
            };
        }

        private Action<object, TableEntityRow> Get_AttachIncludes_Computation(SimpleType trackedType, MaterializationData data)
        {
            var includeBindings = data.ProjectedType.RelatedBindings
                .Where(b => b is IncludeBinding && (b.TargetBinding is QueryBinding)
                    && (b.TargetBinding as QueryBinding).Value == trackedType).Cast<IncludeBinding>().ToList();

            var attachIncludes = includeBindings.Select(b => Get_AttachIncludes_Computation(trackedType.Type, b, data)).ToList();

            return (entity, source) =>
            {
                attachIncludes.ForEach(f => f(entity, source));
            };
        }

        private Action<object, TableEntityRow> Get_AttachIncludes_Computation(Type entityType, IncludeBinding includeBinding
            , MaterializationData data)
        {
            var includeBindings = data.ProjectedType.RelatedBindings
                .Where(b => b is IncludeBinding && b.TargetBinding == includeBinding).Cast<IncludeBinding>().ToList();
            var setIncludes = includeBindings.Select(b => Get_AttachIncludes_Computation(includeBinding.Type, b, data)).ToList();

            //The fieldNo is 1 more than the related-binding index; since the first field [0] is the main entity itself.
            var fieldNo = data.ProjectedType.RelatedBindings.IndexOf(includeBinding) + 1;

            var getOrCreateEntityOrCollection = Get_GetOrCreateEntityOrCollection_Computation(includeBinding, data);
            var memberInfo = (includeBinding.IncludeDirective.GetSelector().Body as MemberExpression).Member;

            //Handling Many-to-Many Map type separately
            if (entityContext._InternalServices.TypeTranslationUtil.GetMapping<IMapEntityMapping>(entityType) != null)
            {
                return (parent, source) =>
                {
                    //Setting the include directive in the intermediate object
                    (parent as IEntity)._getIntermediateEntity().IncludeDirectives.Add(includeBinding.IncludeDirective);

                    var valueInProjection = source.Values[fieldNo];
                    var memberValue = getOrCreateEntityOrCollection(valueInProjection, source);
                    memberInfo.SetValue(parent, memberValue);
                    setIncludes.ForEach(f => f(memberValue, source));
                };
            }

            //Not a Many-to-Many Collection
            else
            {
                var backingContainerGetter = entityContext._InternalServices.TypeTranslationUtil
                    .MakeBackingContainerGetter(entityType, memberInfo);

                return (parent, source) =>
                {
                    //Setting the include directive in the intermediate object
                    (parent as IEntity)._getIntermediateEntity().IncludeDirectives.Add(includeBinding.IncludeDirective);

                    var valueInProjection = source.Values[fieldNo];
                    var intermediateContainerOnParent = backingContainerGetter((parent as IEntity)._getIntermediateEntity());
                    var memberValue = getOrCreateEntityOrCollection(valueInProjection, source);

                    if (null == memberValue) return;

                    if (memberValue is IList)
                    {
                        foreach (var entity in (memberValue as IList))
                            intermediateContainerOnParent.MaterializationAddReference((entity as IEntity)._getIntermediateEntity());
                    }
                    else
                    {
                        intermediateContainerOnParent.MaterializationAddReference((memberValue as IEntity)._getIntermediateEntity());
                    }
                    var member = memberInfo.GetValue(parent);
                    setIncludes.ForEach(f => f(member, source));
                };
            }
        }

        //Get base class objects corresponding to a given TrackedType.
        private Func<TableEntityRow, object[]> Get_GetBaseObjects_Computation(SimpleType trackedType, MaterializationData data)
        {
            return Get_GetBaseObjects_Computation(data, b => b.MatchesTarget(trackedType));
        }

        //Get base class objects corresponding to a given IncludeBinding.
        private Func<TableEntityRow, object[]> Get_GetBaseObjects_Computation(IncludeBinding binding, MaterializationData data)
        {
            return Get_GetBaseObjects_Computation(data, b => b.MatchesTarget(binding));
        }

        //Get base class objects corresponding to a given IncludeBinding.
        private Func<TableEntityRow, object[]> Get_GetBaseObjects_Computation(MaterializationData data,
            Predicate<IRelatedBinding> comparer)
        {
            var fieldIndexes = new List<int>();
            for (int i = 0; i < data.ProjectedType.RelatedBindings.Count; i++)
            {
                var b = data.ProjectedType.RelatedBindings[i];
                if (b is InheritanceBinding && comparer(b))
                    fieldIndexes.Add(i + 1);
            }
            return source => fieldIndexes.Select(idx => source.Values[idx]).ToArray();
        }
    }
}