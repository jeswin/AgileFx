using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace AgileFx.ORM.QueryAnalysis.MethodTranslation
{
    public class Count<TSource> : MethodTranslatorList
    {
        public Count()
        {
            Func<IQueryable<TSource>, int> f = Queryable.Count;
            Func<IQueryable<TSource>, Expression<Func<TSource, bool>>, int> f2 = Queryable.Count;
            Func<IEnumerable<TSource>, int> f3 = Enumerable.Count;
            Func<IEnumerable<TSource>, Func<TSource, bool>, int> f4 = Enumerable.Count;

            var translator = new ScalarMethodTranslator();
            var translatorWithLambda = new ScalarMethodWithLambdaTranslator();
            AddTranslator(f.Method, translator);
            AddTranslator(f2.Method, translatorWithLambda);
            AddTranslator(f3.Method, translator);
            AddTranslator(f4.Method, translatorWithLambda);
        }
    }
}
