using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace AgileFx.ORM.QueryAnalysis.MethodTranslation
{
    public class Take<TSource> : MethodTranslatorList
    {
        public Take()
        {
            Func<IQueryable<TSource>, int, IQueryable<TSource>> f = Queryable.Take;
            Func<IEnumerable<TSource>, int, IEnumerable<TSource>> f2 = Enumerable.Take;

            var translator = new TakeTranslator();
            AddTranslator(f.Method, translator);
            AddTranslator(f2.Method, translator);

            Func<IQueryable<TSource>, Expression<Func<TSource, bool>>, IQueryable<TSource>> f3 = Queryable.TakeWhile;
            Func<IEnumerable<TSource>, Func<TSource, bool>, IEnumerable<TSource>> f4 = Enumerable.TakeWhile;

            var translatorWithLambda = new SimpleMethodWithLambdaTranslator();
            AddTranslator(f3.Method, translatorWithLambda);
            AddTranslator(f4.Method, translatorWithLambda);
        }

        public class TakeTranslator : MethodTranslator
        {
            public override Expression Translate(MethodCallExpression mce, Expression translatedSource1, IEnumerable<Expression> otherArgs, QueryAnalysisContext context)
            {
                var newArgs = new Expression[] { translatedSource1, otherArgs.First() };

                var translatedMethod = mce.Method.GetGenericMethodDefinition().MakeGenericMethod(context.QueryableType.NonPrimitiveEnumerableItemType.TranslatedType);
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
