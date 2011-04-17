using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using AgileFx.ORM.QueryAnalysis.TypeTracking;

namespace AgileFx.ORM.QueryAnalysis.MethodTranslation
{
    public class OrderBy<TSource, TKey> : MethodTranslatorList
    {
        public OrderBy()
        {
            Func<IQueryable<TSource>, Expression<Func<TSource, TKey>>, IQueryable<TSource>> f = Queryable.OrderBy;
            Func<IEnumerable<TSource>, Func<TSource, TKey>, IEnumerable<TSource>> f2 = Enumerable.OrderBy;
            Func<IQueryable<TSource>, Expression<Func<TSource, TKey>>, IQueryable<TSource>> f3 = Queryable.OrderByDescending;
            Func<IEnumerable<TSource>, Func<TSource, TKey>, IEnumerable<TSource>> f4 = Enumerable.OrderByDescending;
            var translator = new OrderByTranslator();
            AddTranslator(f.Method, translator);
            AddTranslator(f2.Method, translator);
            AddTranslator(f3.Method, translator);
            AddTranslator(f4.Method, translator);
        }

        public class OrderByTranslator : MethodTranslator
        {
            public override Expression Translate(MethodCallExpression mce, Expression translatedFirstArg, IEnumerable<Expression> otherArgs, QueryAnalysisContext context)
            {
                bool isQueryable = DefinedOnQueryable(mce);

                //Func<TSource, TKey>
                var keySelectorTranslation = QueryTranslationVisitor.TranslateLambda(otherArgs.First(), 
                    new[] { context.QueryableType.NonPrimitiveEnumerableItemType }, context, isQueryable);

                var newArgs = new Expression[]
                {
                    translatedFirstArg,
                    keySelectorTranslation.TranslatedExpression
                };

                //OrderBy<TSource, TKey>
                var translatedMethod = mce.Method.GetGenericMethodDefinition()
                    .MakeGenericMethod(context.QueryableType.NonPrimitiveEnumerableItemType.TranslatedType, 
                        keySelectorTranslation.QueryableType.TranslatedType);

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