using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgileFx.ORM.QueryAnalysis.MethodTranslation
{
    public class Reverse<TSource> : MethodTranslatorList
    {
        public Reverse()
        {
            Func<IQueryable<TSource>, IQueryable<TSource>> f = Queryable.Reverse;
            Func<IEnumerable<TSource>, IEnumerable<TSource>> f2 = Enumerable.Reverse;
            var translator = new SimpleMethodTranslator();
            AddTranslator(f.Method, translator);
            AddTranslator(f2.Method, translator);
        }
    }
}
