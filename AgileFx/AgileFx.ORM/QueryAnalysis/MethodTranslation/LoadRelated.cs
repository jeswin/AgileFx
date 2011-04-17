using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using AgileFx.ORM.Utils;
using AgileFx.ORM.QueryAnalysis.TypeTracking;

namespace AgileFx.ORM.QueryAnalysis.MethodTranslation
{
    public class LoadRelated : MethodTranslatorList
    {
        public LoadRelated()
        {
            Func<IQueryable<object>, Expression, IQueryable<object>> f = QueryExtensions.LoadRelated;
            AddTranslator(f.Method, new LoadRelatedTranslator());
        }

        public class LoadRelatedTranslator : MethodTranslator
        {
            //Add all IncludeDirectives to Context, and remove 'our' LoadRelated MCE.
            public override Expression Translate(MethodCallExpression mce, Expression translatedFirstArg, IEnumerable<Expression> otherArgs, QueryAnalysisContext context)
            {
                var includes = ((ConstantExpression)((ConstantExpression)mce.Arguments[1]).Value).Value as IncludeDirective[];
                if (includes != null)
                {
                    foreach (var include in includes)
                        AddInclude(context.QueryableType.NonPrimitiveEnumerableItemType, include.GetSelector().Body as MemberExpression,
                            (include is IIncludeInCollectionDirective) ? (include as IIncludeInCollectionDirective).GetFieldSelector() : null);
                }
                //Remove the MCE by returning the first arg instead.
                return translatedFirstArg;
            }

            private void AddInclude(SimpleType source, MemberExpression memberSelector, LambdaExpression collectionInclude)
            {
                var members = source.GetAllMembers();
                if (members.Count > 0 && memberSelector.GetDepth() > 1)
                {
                    var innermostMember = ExpressionUtil.GetInnermostMemberExpression(memberSelector);
                    foreach (var kvp in members)
                    {
                        if (kvp.Key == innermostMember.Member)
                        {
                            AddInclude(kvp.Value, ExpressionUtil.ReplaceInnermostMemberExpressionWithParameter(memberSelector) as MemberExpression, collectionInclude);
                            return;
                        }
                    }
                }
                else
                {
                    var parameter = ExpressionUtil.GetParameterExpression(memberSelector);
                    if (collectionInclude != null)
                    {
                        source.Includes.Add(IncludeDirectiveUtil.GetIncludeInCollectionDirective(memberSelector, collectionInclude));
                    }
                    else
                    {
                        source.Includes.Add(IncludeDirectiveUtil.GetIncludeDirective(memberSelector));
                    }
                }
            }

            public override bool ModifyTranslatedSource(MethodCallExpression mce, QueryAnalysisContext context)
            {
                return false;
            }

            public override bool ModifyTranslatedResult(MethodCallExpression mce, QueryAnalysisContext context)
            {
                return context.IsOuterExpression;
            }
        }
    }
}
