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

using AgileFx.ORM.Reflection;
using AgileFx.ORM.QueryAnalysis;
using AgileFx.ORM.Utils;
using AgileFx.ORM.Mapping;
using AgileFx.ORM.QueryAnalysis.TypeTracking;

namespace AgileFx.ORM.Materialization
{
    public class ManyToManyMapLoader<T> : PostProjectionLoader<T>
    {
        public ManyToManyMapLoader(ProjectedType projectedType, EntityContext entityContext)
            : base(projectedType, entityContext)
        { }

        public override IEnumerable<IUnprojectedBinding> GetBindingsToAnalyze(ProjectedType projectedType)
        {
            return projectedType.UnprojectedBindings.Where(ub => ub is UnprojectedManyToManyBinding);
        }

        public override PostProjectionLoadedResult GetResult(IUnprojectedBinding binding, List<object> resultSet)
        {
            var result = new PostProjectionLoadedResult();
            result.ProjectionBinding = binding;

            var m2mRelationship = GetManyToManyRelationship(binding as UnprojectedManyToManyBinding);

            Func<AnonymousType, object> valueGetter =
                ao => Get_GetProjectedValue_Computation(binding.TargetBinding)(new TableEntityRow(ao));

            var query = GetQuery(resultSet, valueGetter, m2mRelationship.MapType);

            var includes = ModifyExpressions(GetIncludes(binding)
                , m2mRelationship.RelatedEntitySelectorFromMap.Body as MemberExpression);

            //loading both ends of the map object
            includes.AddRange(new[] { m2mRelationship.RelatedEntitySelectorFromMap, m2mRelationship.SelfSelectorFromMap });


            query = LoadIncludes(query, m2mRelationship.MapType, includes);
            result.Value = MethodFinder.EnumerableMethods.ToList(m2mRelationship.MapType).Invoke(null, new object[] { query }) as IList;

            return result;
        }

        private List<LambdaExpression> ModifyExpressions(List<LambdaExpression> includeSelectors
            , MemberExpression relatedEntitySelector)
        {
            return includeSelectors.Select(lambda => Expression.Lambda(ReplaceParameter(lambda.Body, relatedEntitySelector),
                relatedEntitySelector.Expression as ParameterExpression)).ToList();
        }

        private MemberExpression ReplaceParameter(Expression expression, MemberExpression replacement)
        {
            if (expression is MemberExpression)
            {
                var memberExp = expression as MemberExpression;
                return Expression.MakeMemberAccess(ReplaceParameter(memberExp.Expression, replacement), memberExp.Member);
            }
            else if (expression is ParameterExpression)
            {
                return replacement;
            }

            throw new NotSupportedException("Expected ParameterExpression or MemberExpression. Received " + expression.GetType() + ".");
        }

        private ManyToManyRelationship GetManyToManyRelationship(UnprojectedManyToManyBinding binding)
        {
            if (binding.TargetBinding is IncludeBinding)
            {
                var exp = (binding.TargetBinding as IncludeBinding).IncludeDirective.GetSelector().Body as MemberExpression;
                return entityContext._InternalServices.TypeTranslationUtil.GetMapping<IModelEntityMapping>(exp.Member.DeclaringType)
                    .GetManyToManyRelationships()
                    .Find(map => (map.RelatedEntitySelector.Body as MemberExpression).Member.Name == exp.Member.Name);
            }
            else if (binding.TargetBinding is QueryBinding)
            {
                return ((binding.TargetBinding as QueryBinding).Value.NonPrimitiveEnumerableItemType as ManyToManyMapType).Relationship;
            }

            throw new NotSupportedException("Expected IncludeBinding or QueryBinding. Received " + binding.TargetBinding.GetType() + ".");
        }
    }
}
