using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace AgileFx.ORM.QueryAnalysis.MethodTranslation
{
    public class All<TSource> : MethodTranslatorList
    {
        public All()
        {
            Func<IQueryable<TSource>, Expression<Func<TSource, bool>>, bool> f = Queryable.All;
            Func<IEnumerable<TSource>, Func<TSource, bool>, bool> f2 = Enumerable.All;

            var translator = new ScalarMethodWithLambdaTranslator();
            AddTranslator(f.Method, translator);
            AddTranslator(f2.Method, translator);
        }
    }
}
