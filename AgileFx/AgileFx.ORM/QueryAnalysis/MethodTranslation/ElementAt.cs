using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace AgileFx.ORM.QueryAnalysis.MethodTranslation
{
    public class ElementAt<TSource> : MethodTranslatorList
    {
        public ElementAt()
        {
            Func<IQueryable<TSource>, int, TSource> f = Queryable.ElementAt;
            Func<IEnumerable<TSource>, int, TSource> f2 = Enumerable.ElementAt;
            var translator = new ElementAtTranslator();
            AddTranslator(f.Method, translator);
            AddTranslator(f2.Method, translator);
        }

        public class ElementAtTranslator : MethodTranslator
        {
            public override Expression Translate(MethodCallExpression mce, Expression translatedSource1, IEnumerable<Expression> otherArgs, QueryAnalysisContext context)
            {
                var newArgs = new Expression[]
                {
                    translatedSource1,
                    otherArgs.First()
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
