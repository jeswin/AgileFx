using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace AgileFx.ORM.QueryAnalysis.MethodTranslation
{
    public class First<TSource> : MethodTranslatorList
    {
        public First()
        {
            Func<IQueryable<TSource>, TSource> f = Queryable.First;
            Func<IQueryable<TSource>, TSource> f2 = Queryable.FirstOrDefault;
            Func<IEnumerable<TSource>, TSource> f3 = Enumerable.First;
            Func<IEnumerable<TSource>, TSource> f4 = Enumerable.FirstOrDefault;

            Func<IQueryable<TSource>, Expression<Func<TSource, bool>>, TSource> f5 = Queryable.First;
            Func<IQueryable<TSource>, Expression<Func<TSource, bool>>, TSource> f6 = Queryable.FirstOrDefault;
            Func<IEnumerable<TSource>, Func<TSource, bool>, TSource> f7 = Enumerable.First;
            Func<IEnumerable<TSource>, Func<TSource, bool>, TSource> f8 = Enumerable.FirstOrDefault;

            var translator = new SingleValueMethodTranslator();
            AddTranslator(f.Method, translator);
            AddTranslator(f2.Method, translator);
            AddTranslator(f3.Method, translator);
            AddTranslator(f4.Method, translator);

            var translatorWithLambda = new SingleValueMethodWithLambdaTranslator();
            AddTranslator(f5.Method, translatorWithLambda);
            AddTranslator(f6.Method, translatorWithLambda);
            AddTranslator(f7.Method, translatorWithLambda);
            AddTranslator(f8.Method, translatorWithLambda);
        }        
    }
}
