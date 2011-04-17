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
using AgileFx.ORM.Utils;
using AgileFx.ORM.Mapping;
using AgileFx.ORM.QueryAnalysis.TypeTracking;

namespace AgileFx.ORM.Materialization
{
    public class CollectionIncludesLoader<T>
    {
        ProjectedType projectedType;
        EntityContext entityContext;
        Dictionary<LambdaExpression, List<LambdaExpression>> includeGroups = new Dictionary<LambdaExpression, List<LambdaExpression>>();

        public CollectionIncludesLoader(ProjectedType projectedType, EntityContext entityContext)
        {
            this.projectedType = projectedType;
            this.entityContext = entityContext;
            
            //In the case of many-to-many and derived collections, includes can be handled when they are reloaded
            foreach (var inc in projectedType.UnprojectedBindings
                .Where(ub => ub is UnprojectedIncludeInCollectionBinding 
                    && !(ub .TargetBinding is UnprojectedCollectionBinding || ub.TargetBinding is UnprojectedManyToManyBinding)))
            {
                var includeDirective = (inc as UnprojectedIncludeInCollectionBinding).IncludeDirective;
                var collSelector = includeDirective.GetSelector();
                if (!includeGroups.ContainsKey(collSelector))
                {
                    includeGroups.Add(collSelector, new List<LambdaExpression>());
                }
                includeGroups[collSelector].Add(((IIncludeInCollectionDirective)includeDirective).GetFieldSelector());
            }
        }

        private object GetPropertyValue(object result, string propertyPath)
        {
            var props = propertyPath.Split('.');
            var propVal = result.GetType().GetProperty(props[0]).GetValue(result);
            if (props.Length > 1)
                return GetPropertyValue(propVal, string.Join(",", props, 1, props.Length - 1));
            else
                return propVal;
        }

        public void LoadUnprojectedIncludes(IEnumerable<T> results)
        {
            if (results.Count() == 0) return;
            foreach (var kv in includeGroups)
            {
                Type projType = kv.Key.Body.Type.GetGenericArguments().Single();
                var projectedObjects = new List<object>();
                var propPath = ExpressionUtil.GetPropertyName(kv.Key);
                foreach (var projObj in results)
                {
                    foreach (var obj in GetPropertyValue(projObj, propPath) as IEnumerable)
                        projectedObjects.Add(obj);
                }
                if (projectedObjects.Count == 0) continue;

                var createQuery = MethodFinder.EntityContextMethods.CreateQuery(projType);
                IQueryable queryable = createQuery.Invoke(entityContext, null) as IQueryable;

                var selExps = entityContext._InternalServices.TypeTranslationUtil.GetMapping<IModelEntityMapping>(projType).GetIdentityFields().OfType<LambdaExpression>()
                    .Select(x => x.Body as MemberExpression).ToArray();
                var conditionValues = projectedObjects.Select(o => selExps.Select(e =>
                {
                    var l = Expression.Lambda(e, ExpressionUtil.GetParameterExpression(selExps[0]));
                    return new Condition(l, o.GetType().GetMember(e.Member.Name).Single().GetValue(o));
                }).ToList()).ToList();

                queryable = MethodFinder.QueryExtensionsMethods.WhereMatchesConditions(projType)
                    .Invoke(null, new object[] { queryable, conditionValues }) as IQueryable;

                var expressionArray = MethodFinder.CollectionIncludesLoaderMethods<T>.GetIncludeExpression(projType)
                    .Invoke(this, new object[] { kv.Value });

                var loadRelated = MethodFinder.QueryExtensionsMethods.LoadRelated(projType);
                queryable = loadRelated.Invoke(null, new object[] { queryable, expressionArray }) as IQueryable;

                MethodFinder.EnumerableMethods.ToList(projType).Invoke(null, new object[] { queryable });
            }
        }

        private Expression<Func<TField, object>>[] GetIncludeExpression<TField>(IEnumerable<LambdaExpression> includeExpressions)
        {
            return includeExpressions.Select(x => x as Expression<Func<TField, object>>).ToArray();
        }
    }
}