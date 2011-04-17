using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace AgileFx.ORM.QueryAnalysis.MethodTranslation
{
    public class Any<TSource> : MethodTranslatorList
    {
        public Any()
        {
            Func<IQueryable<TSource>, bool> f = Queryable.Any;
            Func<IQueryable<TSource>, Expression<Func<TSource, bool>>, bool> f2 = Queryable.Any;
            Func<IEnumerable<TSource>, bool> f3 = Enumerable.Any;
            Func<IEnumerable<TSource>, Func<TSource, bool>, bool> f4 = Enumerable.Any;

            var translator = new ScalarMethodTranslator();
            var translatorWithLambda = new ScalarMethodWithLambdaTranslator();
            AddTranslator(f.Method, translator);
            AddTranslator(f2.Method, translatorWithLambda);
            AddTranslator(f3.Method, translator);
            AddTranslator(f4.Method, translatorWithLambda);
        }
    }
}
