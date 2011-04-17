using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace AgileFx.ORM.QueryAnalysis.MethodTranslation
{
    public class Sum<TSource> : MethodTranslatorList
    {
        public Sum()
        {
            Func<IQueryable<TSource>, Expression<Func<TSource, decimal>>, decimal> f = Queryable.Sum;
            Func<IQueryable<TSource>, Expression<Func<TSource, decimal?>>, decimal?> f2 = Queryable.Sum;
            Func<IQueryable<TSource>, Expression<Func<TSource, double>>, double> f3 = Queryable.Sum;
            Func<IQueryable<TSource>, Expression<Func<TSource, double?>>, double?> f4 = Queryable.Sum;
            Func<IQueryable<TSource>, Expression<Func<TSource, float>>, float> f5 = Queryable.Sum;
            Func<IQueryable<TSource>, Expression<Func<TSource, float?>>, float?> f6 = Queryable.Sum;
            Func<IQueryable<TSource>, Expression<Func<TSource, int>>, int> f7 = Queryable.Sum;
            Func<IQueryable<TSource>, Expression<Func<TSource, int?>>, int?> f8 = Queryable.Sum;
            Func<IQueryable<TSource>, Expression<Func<TSource, long>>, long> f9 = Queryable.Sum;
            Func<IQueryable<TSource>, Expression<Func<TSource, long?>>, long?> f10 = Queryable.Sum;

            Func<IEnumerable<TSource>, Func<TSource, decimal>, decimal> f11 = Enumerable.Sum;
            Func<IEnumerable<TSource>, Func<TSource, decimal?>, decimal?> f12 = Enumerable.Sum;
            Func<IEnumerable<TSource>, Func<TSource, double>, double> f13 = Enumerable.Sum;
            Func<IEnumerable<TSource>, Func<TSource, double?>, double?> f14 = Enumerable.Sum;
            Func<IEnumerable<TSource>, Func<TSource, float>, float> f15 = Enumerable.Sum;
            Func<IEnumerable<TSource>, Func<TSource, float?>, float?> f16 = Enumerable.Sum;
            Func<IEnumerable<TSource>, Func<TSource, int>, int> f17 = Enumerable.Sum;
            Func<IEnumerable<TSource>, Func<TSource, int?>, int?> f18 = Enumerable.Sum;
            Func<IEnumerable<TSource>, Func<TSource, long>, long> f19 = Enumerable.Sum;
            Func<IEnumerable<TSource>, Func<TSource, long?>, long?> f20 = Enumerable.Sum;

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
