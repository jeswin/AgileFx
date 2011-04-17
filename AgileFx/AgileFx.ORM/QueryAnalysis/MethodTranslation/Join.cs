using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using AgileFx.ORM.QueryAnalysis.TypeTracking;

namespace AgileFx.ORM.QueryAnalysis.MethodTranslation
{
    public class Join<TOuter, TInner, TKey, TResult> : MethodTranslatorList
    {
        public Join()
        {
            Func<IQueryable<TOuter>, IEnumerable<TInner>, Expression<Func<TOuter, TKey>>, Expression<Func<TInner, TKey>>, Expression<Func<TOuter, TInner, TResult>>, IQueryable<TResult>> f = Queryable.Join;
            Func<IEnumerable<TOuter>, IEnumerable<TInner>, Func<TOuter, TKey>, Func<TInner, TKey>, Func<TOuter, TInner, TResult>, IEnumerable<TResult>> f2 = Enumerable.Join;
            var translator = new JoinTranslator();
            AddTranslator(f.Method, translator);
            AddTranslator(f2.Method, translator);
        }

        public class JoinTranslator : MethodTranslator
        {
            public override Expression Translate(MethodCallExpression mce, Expression translatedOuter, IEnumerable<Expression> otherArgs, QueryAnalysisContext context)
            {
                bool isQueryable = DefinedOnQueryable(mce);

                var _otherArgs = otherArgs.ToArray();

                //IEnumerable<TInner>
                var innerTranslation = QueryTranslationVisitor.VisitWithNewVisitor(_otherArgs[0], context, false);

                //Func<TOuter, TKey>
                var outerKeySelectorTranslation = QueryTranslationVisitor.TranslateLambda(_otherArgs[1], new[] { context.QueryableType.NonPrimitiveEnumerableItemType }, context, isQueryable);

                //Func<TInner, TKey>
                var innerKeySelectorTranslation = QueryTranslationVisitor.TranslateLambda(_otherArgs[2], new[] { innerTranslation.QueryableType.NonPrimitiveEnumerableItemType }, context, isQueryable);

                //Func<TOuter, TInner, TResult>
                var resultSelectorTranslation = QueryTranslationVisitor.TranslateLambda(_otherArgs[3], new[] { context.QueryableType.NonPrimitiveEnumerableItemType, innerTranslation.QueryableType.NonPrimitiveEnumerableItemType }, context, isQueryable);

                var newArgs = new Expression[]
                {
                    translatedOuter,
                    innerTranslation.TranslatedExpression,
                    outerKeySelectorTranslation.TranslatedExpression,
                    innerKeySelectorTranslation.TranslatedExpression,
                    resultSelectorTranslation.TranslatedExpression
                };

                //Join<TOuter, TInner, TKey, TResult>
                var translatedMethod = mce.Method.GetGenericMethodDefinition().MakeGenericMethod(
                        context.QueryableType.NonPrimitiveEnumerableItemType.TranslatedType,
                        innerTranslation.QueryableType.NonPrimitiveEnumerableItemType.TranslatedType,
                        outerKeySelectorTranslation.QueryableType.TranslatedType,
                        resultSelectorTranslation.QueryableType.TranslatedType);
                context.QueryableType = RuntimeTypes.CreateEnumerable(resultSelectorTranslation.QueryableType);

                return Expression.Call(mce.Object, translatedMethod, newArgs);
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