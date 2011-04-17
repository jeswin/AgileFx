using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using AgileFx.ORM.QueryAnalysis.TypeTracking;

namespace AgileFx.ORM.QueryAnalysis.MethodTranslation
{
    public class Union<TSource> : MethodTranslatorList
    {
        public Union()
        {
            Func<IQueryable<TSource>, IEnumerable<TSource>, IQueryable<TSource>> f = Queryable.Union;
            Func<IEnumerable<TSource>, IEnumerable<TSource>, IEnumerable<TSource>> f2 = Enumerable.Union;
            var translator = new UnionTranslator();
            AddTranslator(f.Method, translator);
            AddTranslator(f2.Method, translator);
        }

        public class UnionTranslator : MethodTranslator
        {
            public override Expression Translate(MethodCallExpression mce, Expression translatedSource1, IEnumerable<Expression> otherArgs, QueryAnalysisContext context)
            {
                var translatedSource2 = QueryTranslationVisitor.VisitWithNewVisitor(otherArgs.Single(), context, context.ModifyProjection).TranslatedExpression;

                if (translatedSource1.Type != translatedSource2.Type)
                    throw new Exception("Queries joined with a Union must be similar.");

                var newArgs = new Expression[]
                {
                    translatedSource1,
                    translatedSource2
                };

                var translatedMethod = mce.Method.GetGenericMethodDefinition().MakeGenericMethod(context.QueryableType.NonPrimitiveEnumerableItemType.TranslatedType);
                return Expression.Call(mce.Object, translatedMethod, newArgs);
            }


            public override bool ModifyTranslatedSource(MethodCallExpression mce, QueryAnalysisContext context)
            {
                return true;
            }

            public override bool ModifyTranslatedResult(MethodCallExpression mce, QueryAnalysisContext context)
            {
                return false;
            }
        }
    }
}
