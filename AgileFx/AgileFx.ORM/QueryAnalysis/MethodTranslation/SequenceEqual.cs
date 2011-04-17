using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using AgileFx.ORM.QueryAnalysis.TypeTracking;

namespace AgileFx.ORM.QueryAnalysis.MethodTranslation
{
    public class SequenceEqual<TSource> : MethodTranslatorList
    {
        public SequenceEqual()
        {
            Func<IQueryable<TSource>, IEnumerable<TSource>, bool> f = Queryable.SequenceEqual;
            Func<IEnumerable<TSource>, IEnumerable<TSource>, bool> f2 = Enumerable.SequenceEqual;
            var translator = new SequenceEqualTranslator();
            AddTranslator(f.Method, translator);
            AddTranslator(f2.Method, translator);
        }

        public class SequenceEqualTranslator : MethodTranslator
        {
            public override Expression Translate(MethodCallExpression mce, Expression translatedSource1, IEnumerable<Expression> otherArgs, QueryAnalysisContext context)
            {
                var translatedSource2 = QueryTranslationVisitor.VisitWithNewVisitor(otherArgs.Single(), context, false).TranslatedExpression;

                if (translatedSource1.Type != translatedSource2.Type)
                    throw new Exception("Queries joined with a SequenceEqual must be similar.");

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
                return false;
            }

            public override bool ModifyTranslatedResult(MethodCallExpression mce, QueryAnalysisContext context)
            {
                return false;
            }
        }
    }
}
