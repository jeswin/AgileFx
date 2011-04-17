using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace AgileFx.ORM.QueryAnalysis.MethodTranslation
{
    public class Single<TSource> : MethodTranslatorList
    {
        public Single()
        {
            Func<IQueryable<TSource>, TSource> f = Queryable.Single;
            Func<IQueryable<TSource>, TSource> f2 = Queryable.SingleOrDefault;
            Func<IEnumerable<TSource>, TSource> f3 = Enumerable.Single;
            Func<IEnumerable<TSource>, TSource> f4 = Enumerable.SingleOrDefault;

            Func<IQueryable<TSource>, Expression<Func<TSource, bool>>, TSource> f5 = Queryable.Single;
            Func<IQueryable<TSource>, Expression<Func<TSource, bool>>, TSource> f6 = Queryable.SingleOrDefault;
            Func<IEnumerable<TSource>, Func<TSource, bool>, TSource> f7 = Enumerable.Single;
            Func<IEnumerable<TSource>, Func<TSource, bool>, TSource> f8 = Enumerable.SingleOrDefault;

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
