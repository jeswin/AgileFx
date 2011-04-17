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
using System.Linq.Expressions;
using System.Text;
using System.Reflection;

using IQToolkit;
using AgileFx.ORM.Reflection;
using AgileFx.ORM.QueryAnalysis;
using AgileFx.ORM.Mapping;
using AgileFx.ORM.QueryAnalysis.TypeTracking;

namespace AgileFx.ORM.Materialization
{
    public abstract class PostProjectionLoader<T>
    {
        protected EntityContext entityContext;
        protected ProjectedType projectedType;

        public PostProjectionLoader(ProjectedType projectedType, EntityContext entityContext)
        {
            this.entityContext = entityContext;
            this.projectedType = projectedType;
        }

        private Func<object, object> Get_GetProjectedValue_Computation(SimpleType sourceType, SimpleType queryableType)
        {
            if (sourceType == queryableType) 
                return tableEntity => tableEntity;

            foreach (var kvp in sourceType.GetAllMembers())
            {
                var memberGetter = Get_GetProjectedValue_Computation(kvp.Value, queryableType);
                if (memberGetter != null)
                {
                    var translatedValueGetter = queryableType.GetTranslatedMemberValue(kvp.Key,
                        entityContext._InternalServices.TypeTranslationUtil);
                    return tableEntity => memberGetter(translatedValueGetter(tableEntity));
                }
            }

            if (sourceType.NonPrimitiveEnumerableItemType != null)
            {
                var collectionItemGetter = Get_GetProjectedValue_Computation(sourceType.NonPrimitiveEnumerableItemType, queryableType);
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

        protected Func<TableEntityRow, object> Get_GetProjectedValue_Computation(IRequestedBinding requestedBinding)
        {
            if (requestedBinding is QueryBinding)
            {
                var originalQueryType = projectedType.Fields.First(kvp => kvp.Key.Name == "Field0").Value;
                var valueGetter = Get_GetProjectedValue_Computation(originalQueryType, (requestedBinding as QueryBinding).Value);
                return source => valueGetter(source.Values[0]);
            }
            //Then its an include binding
            else
            {
                var projectionIndex = projectedType.RelatedBindings.IndexOf(requestedBinding as IncludeBinding) + 1;
                return source => source.Values[projectionIndex];
            }
        }

        //Parent property selector defines the entity to be used in where clause.
        protected IQueryable GetQuery(List<object> resultSet, Func<AnonymousType, object> valueGetter, Type targetType)
        {
            var parameter = Expression.Parameter(targetType, "p");

            var whereClauseExpressions = entityContext._InternalServices.TypeTranslationUtil
                .GetMapping<IEntityMapping>(targetType)
                    .GetIdentityFields().OfType<LambdaExpression>()
                        .Select(x => Expression.PropertyOrField(parameter, ((MemberExpression)(x.Body)).Member.Name)).ToArray();

            var createQuery = MethodFinder.EntityContextMethods.CreateQuery(targetType);
            IQueryable query = createQuery.Invoke(entityContext, null) as IQueryable;

            var fieldValues = new List<object>();
            resultSet.ForEach(ao =>
            {
                foreach (var item in (valueGetter(ao as AnonymousType) as IEnumerable)) fieldValues.Add(item);
            });
            var conditionValues = fieldValues.Select(o => whereClauseExpressions.Select(e =>
            {
                var l = Expression.Lambda(e, parameter);
                return new Condition(l, o.GetType().GetMember(e.Member.Name).Single().GetValue(o));
            }).ToList()).ToList();

            return MethodFinder.QueryExtensionsMethods.WhereMatchesConditions(targetType)
                .Invoke(null, new object[] { query, conditionValues }) as IQueryable;
        }

        protected List<LambdaExpression> GetIncludes(IUnprojectedBinding binding)
        {
            return projectedType.UnprojectedBindings
                .Where(b => b is UnprojectedIncludeInCollectionBinding && b.TargetBinding == binding)
                .Select(b => ((b as UnprojectedIncludeInCollectionBinding).IncludeDirective as IIncludeInCollectionDirective)
                    .GetFieldSelector()).ToList();
        }

        protected IQueryable LoadIncludes(IQueryable query, Type queryType, IEnumerable<LambdaExpression> expressions)
        {
            var expressionArray = expressions.ToArray();
            //loading both ends of the map object
            var includeExpression = MethodFinder.PostProjectionLoaderMethods<T>.GetIncludeExpression(queryType)
                    .Invoke(this, new object[] { expressionArray });
            var loadRelated = MethodFinder.QueryExtensionsMethods.LoadRelated(queryType);
            return loadRelated.Invoke(null, new object[] { query, includeExpression }) as IQueryable;
        }

        private Expression<Func<TField, object>>[] GetIncludeExpression<TField>(IEnumerable<LambdaExpression> includeExpressions)
        {
            return includeExpressions.Select(exp => Expression.Lambda(typeof(Func<TField, object>), exp.Body, exp.Parameters)
                    as Expression<Func<TField, object>>).ToArray();
        }

        public List<PostProjectionLoadedResult> LoadUnprojectedBindings(List<object> resultSet)
        {
            var reloadedObjects = new List<PostProjectionLoadedResult>();

            if (resultSet == null && resultSet.Count == 0)
                return reloadedObjects;

            foreach(var binding in GetBindingsToAnalyze(projectedType))
                reloadedObjects.Add(GetResult(binding, resultSet));

            return reloadedObjects;
        }

        public abstract PostProjectionLoadedResult GetResult(IUnprojectedBinding binding, List<object> objects);

        public abstract IEnumerable<IUnprojectedBinding> GetBindingsToAnalyze(ProjectedType projectionType);
    }
}