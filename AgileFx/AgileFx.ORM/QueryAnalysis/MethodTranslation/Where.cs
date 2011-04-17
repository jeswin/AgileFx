using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace AgileFx.ORM.QueryAnalysis.MethodTranslation
{
    public class Where<TSource, TKey> : MethodTranslatorList
    {
        public Where()
        {
            Func<IQueryable<TSource>, Expression<Func<TSource, bool>>, IQueryable<TSource>> f = Queryable.Where;
            Func<IEnumerable<TSource>, Func<TSource, bool>, IEnumerable<TSource>> f2 = Enumerable.Where;

            var translator = new SimpleMethodWithLambdaTranslator();
            AddTranslator(f.Method, translator);
            AddTranslator(f2.Method, translator);
        }
    }
}