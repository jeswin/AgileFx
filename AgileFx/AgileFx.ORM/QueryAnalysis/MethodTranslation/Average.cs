using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using AgileFx.ORM.QueryAnalysis.TypeTracking;

namespace AgileFx.ORM.QueryAnalysis.MethodTranslation
{
    public class Average<TSource> : MethodTranslatorList
    {
        public Average()
        {
            Func<IQueryable<TSource>, Expression<Func<TSource, decimal>>, decimal> f = Queryable.Average;
            Func<IQueryable<TSource>, Expression<Func<TSource, decimal?>>, decimal?> f2 = Queryable.Average;
            Func<IQueryable<TSource>, Expression<Func<TSource, double>>, double> f3 = Queryable.Average;
            Func<IQueryable<TSource>, Expression<Func<TSource, double?>>, double?> f4 = Queryable.Average;
            Func<IQueryable<TSource>, Expression<Func<TSource, float>>, float> f5 = Queryable.Average;
            Func<IQueryable<TSource>, Expression<Func<TSource, float?>>, float?> f6 = Queryable.Average;
            Func<IQueryable<TSource>, Expression<Func<TSource, int>>, double> f7 = Queryable.Average;
            Func<IQueryable<TSource>, Expression<Func<TSource, int?>>, double?> f8 = Queryable.Average;
            Func<IQueryable<TSource>, Expression<Func<TSource, long>>, double> f9 = Queryable.Average;
            Func<IQueryable<TSource>, Expression<Func<TSource, long?>>, double?> f10 = Queryable.Average;

            Func<IEnumerable<TSource>, Func<TSource, decimal>, decimal> f11 = Enumerable.Average;
            Func<IEnumerable<TSource>, Func<TSource, decimal?>, decimal?> f12 = Enumerable.Average;
            Func<IEnumerable<TSource>, Func<TSource, double>, double> f13 = Enumerable.Average;
            Func<IEnumerable<TSource>, Func<TSource, double?>, double?> f14 = Enumerable.Average;
            Func<IEnumerable<TSource>, Func<TSource, float>, float> f15 = Enumerable.Average;
            Func<IEnumerable<TSource>, Func<TSource, float?>, float?> f16 = Enumerable.Average;
            Func<IEnumerable<TSource>, Func<TSource, int>, double> f17 = Enumerable.Average;
            Func<IEnumerable<TSource>, Func<TSource, int?>, double?> f18 = Enumerable.Average;
            Func<IEnumerable<TSource>, Func<TSource, long>, double> f19 = Enumerable.Average;
            Func<IEnumerable<TSource>, Func<TSource, long?>, double?> f20 = Enumerable.Average;

            var translator = new ScalarMethodWithLambdaTranslator();
            AddTranslator(f.Method, translator);
            AddTranslator(f2.Method, translator);
            AddTranslator(f3.Method, translator);
            AddTranslator(f4.Method, translator);
            AddTranslator(f5.Method, translator);
            AddTranslator(f6.Method, translator);
            AddTranslator(f7.Method, translator);
            AddTranslator(f8.Method, translator);
            AddTranslator(f9.Method, translator);
            AddTranslator(f10.Method, translator);
            AddTranslator(f11.Method, translator);
            AddTranslator(f12.Method, translator);
            AddTranslator(f13.Method, translator);
            AddTranslator(f14.Method, translator);
            AddTranslator(f15.Method, translator);
            AddTranslator(f16.Method, translator);
            AddTranslator(f17.Method, translator);
            AddTranslator(f18.Method, translator);
            AddTranslator(f19.Method, translator);
            AddTranslator(f20.Method, translator);
        }
    }
}
