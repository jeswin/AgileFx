using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace AgileFx.ORM.QueryAnalysis.MethodTranslation
{
    public class ThenBy<TSource, TKey> : MethodTranslatorList
    {
        public ThenBy()
        {
            Func<IOrderedQueryable<TSource>, Expression<Func<TSource, TKey>>, IOrderedQueryable<TSource>> f = Queryable.ThenBy;
            Func<IOrderedEnumerable<TSource>, Func<TSource, TKey>, IOrderedEnumerable<TSource>> f2 = Enumerable.ThenBy;

            var translator = new SimpleMethodWithLambdaTranslator();
            AddTranslator(f.Method, translator);
            AddTranslator(f2.Method, translator);
        }
    }
}
