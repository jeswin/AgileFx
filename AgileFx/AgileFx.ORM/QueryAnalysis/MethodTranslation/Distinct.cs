using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using AgileFx.ORM.QueryAnalysis.TypeTracking;

namespace AgileFx.ORM.QueryAnalysis.MethodTranslation
{
    public class Distinct<TSource> : MethodTranslatorList
    {
        public Distinct()
        {
            Func<IQueryable<TSource>, IQueryable<TSource>> f = Queryable.Distinct;
            Func<IEnumerable<TSource>, IEnumerable<TSource>> f2 = Enumerable.Distinct;
            var translator = new SimpleMethodTranslator();
            AddTranslator(f.Method, translator);
            AddTranslator(f2.Method, translator);
        }       
    }
}
