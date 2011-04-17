using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using AgileFx.ORM.Utils;
using AgileFx.ORM.QueryAnalysis.TypeTracking;

namespace AgileFx.ORM.QueryAnalysis.MethodTranslation
{
    public class Select1<TSource, TResult> : MethodTranslatorList
    {
        public Select1()
        {
            Func<IQueryable<TSource>, Expression<Func<TSource, TResult>>, IQueryable<TResult>> f1 = Queryable.Select;
            Func<IEnumerable<TSource>, Func<TSource, TResult>, IEnumerable<TResult>> f2 = Enumerable.Select;
            var translator = new Select1Translator();
            AddTranslator(f1.Method, translator);
            AddTranslator(f2.Method, translator);
        }

        public class Select1Translator : MethodTranslator
        {
            public override Expression Translate(MethodCallExpression mce, Expression translatedSource, IEnumerable<Expression> otherArgs, QueryAnalysisContext context)
            {
                bool isQueryable = DefinedOnQueryable(mce);

                //Func<TSource, TResult>
                var selectorTranslation = QueryTranslationVisitor.TranslateLambda(otherArgs.First(),
                    new[] { context.QueryableType.NonPrimitiveEnumerableItemType }, context, isQueryable);

                var newArgs = new Expression[]
                {
                    translatedSource,
                    selectorTranslation.TranslatedExpression
                };

                //Select<TSource, TResult>
                var translatedMethod = mce.Method.GetGenericMethodDefinition().MakeGenericMethod(
                    context.QueryableType.NonPrimitiveEnumerableItemType.TranslatedType,
                    selectorTranslation.QueryableType.TranslatedType);

                //IQueryable<TResult>
                context.QueryableType = RuntimeTypes.CreateEnumerable(selectorTranslation.QueryableType);

                return Expression.Call(mce.Object, translatedMethod, newArgs);
            }

            public override bool ModifyTranslatedSource(MethodCallExpression mce, QueryAnalysisContext context)
            {
                return false;
            }

            public override bool ModifyTranslatedResult(MethodCallExpression mce, QueryAnalysisContext context)
            {
                return context.IsOuterExpression;
            }
        }
    }

    public class Select2<TSource, TResult> : MethodTranslatorList
    {
        public Select2()
        {
            Func<IQueryable<TSource>, Expression<Func<TSource, int, TResult>>, IQueryable<TResult>> f1 = Queryable.Select;
            Func<IEnumerable<TSource>, Func<TSource, int, TResult>, IEnumerable<TResult>> f2 = Enumerable.Select;
            var translator = new Select2Translator();
            AddTranslator(f1.Method, translator);
            AddTranslator(f2.Method, translator);
        }

        public class Select2Translator : MethodTranslator
        {
            public override Expression Translate(MethodCallExpression mce, Expression translatedSource, IEnumerable<Expression> otherArgs, QueryAnalysisContext context)
            {
                bool isQueryable = DefinedOnQueryable(mce);

                //Func<TSource, int, TResult>
                var selectorTranslation = QueryTranslationVisitor.TranslateLambda(otherArgs.First(), 
                    new[] { context.QueryableType.NonPrimitiveEnumerableItemType,   //TSource
                            new SimpleType(typeof(int)) },              //int
                    context, isQueryable);

                var newArgs = new Expression[]
                {
                    translatedSource,
                    selectorTranslation.TranslatedExpression
                };

                //Select<TSource, TResult>
                var translatedMethod = mce.Method.GetGenericMethodDefinition().MakeGenericMethod(
                    context.QueryableType.NonPrimitiveEnumerableItemType.TranslatedType,
                    selectorTranslation.QueryableType.TranslatedType);

                context.QueryableType = RuntimeTypes.CreateEnumerable(selectorTranslation.QueryableType);

                return Expression.Call(mce.Object, translatedMethod, newArgs);
            }

            public override bool ModifyTranslatedSource(MethodCallExpression mce, QueryAnalysisContext context)
            {
                return false;
            }

            public override bool ModifyTranslatedResult(MethodCallExpression mce, QueryAnalysisContext context)
            {
                return context.IsOuterExpression;
            }
        }
    }
}
