using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace AgileFx.ORM.QueryAnalysis.MethodTranslation
{
    public class LongCount<TSource> : MethodTranslatorList
    {
        public LongCount()
        {
            Func<IQueryable<TSource>, long> f = Queryable.LongCount;
            Func<IQueryable<TSource>, Expression<Func<TSource, bool>>, long> f2 = Queryable.LongCount;
            Func<IEnumerable<TSource>, long> f3 = Enumerable.LongCount;
            Func<IEnumerable<TSource>, Func<TSource, bool>, long> f4 = Enumerable.LongCount;

            var translator = new ScalarMethodTranslator();
            var translatorWithLambda = new ScalarMethodWithLambdaTranslator();
            AddTranslator(f.Method, translator);
            AddTranslator(f2.Method, translatorWithLambda);
            AddTranslator(f3.Method, translator);
            AddTranslator(f4.Method, translatorWithLambda);
        }
    }
}
