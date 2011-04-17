using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using AgileFx.ORM.Utils;
using AgileFx.ORM.QueryAnalysis.TypeTracking;

namespace AgileFx.ORM.QueryAnalysis.MethodTranslation
{
    public class GroupBy1<TSource, TKey> : MethodTranslatorList
    {
        public GroupBy1()
        {
            Func<IQueryable<TSource>, Expression<Func<TSource, TKey>>, IQueryable<IGrouping<TKey, TSource>>> f = Queryable.GroupBy;
            Func<IEnumerable<TSource>, Func<TSource, TKey>, IEnumerable<IGrouping<TKey, TSource>>> f2 = Enumerable.GroupBy;
            var translator = new GroupBy1Translator();
            AddTranslator(f.Method, translator);
            AddTranslator(f2.Method, translator);
        }

        public class GroupBy1Translator : MethodTranslator
        {
            public override Expression Translate(MethodCallExpression mce, Expression translatedSource, IEnumerable<Expression> otherArgs, QueryAnalysisContext context)
            {
                bool isQueryable = DefinedOnQueryable(mce);

                //Func<TSource, TKey>
                var keySelectorTranslation = QueryTranslationVisitor.TranslateLambda(otherArgs.First(), 
                    new[] { context.QueryableType.NonPrimitiveEnumerableItemType }, context, isQueryable);

                var newArgs = new Expression[]
                {
                    translatedSource,
                    keySelectorTranslation.TranslatedExpression
                };

                //GroupBy<TSource, TKey>
                var translatedMethod = mce.Method.GetGenericMethodDefinition().MakeGenericMethod(
                    context.QueryableType.NonPrimitiveEnumerableItemType.TranslatedType,
                    keySelectorTranslation.QueryableType.TranslatedType);

                //IQueryable<IGrouping<TKey, TSource>>
                var groupingType = RuntimeTypes.CreateGrouping(keySelectorTranslation.QueryableType, context.QueryableType.NonPrimitiveEnumerableItemType);
                var queryableType = RuntimeTypes.CreateEnumerable(groupingType);

                context.QueryableType = queryableType;

                return Expression.Call(mce.Object, translatedMethod, newArgs);
            }

            public override bool ModifyTranslatedSource(MethodCallExpression mce, QueryAnalysisContext context)
            {
                return true;
            }

            public override bool ModifyTranslatedResult(MethodCallExpression mce, QueryAnalysisContext context)
            {
                return context.IsOuterExpression;
            }
        }
    }

    public class GroupBy2<TSource, TKey, TResult> : MethodTranslatorList
    {
        public GroupBy2()
        {
            Func<IQueryable<TSource>, Expression<Func<TSource, TKey>>, Expression<Func<TKey, IEnumerable<TSource>, TResult>>, IQueryable<TResult>> f = Queryable.GroupBy;
            Func<IEnumerable<TSource>, Func<TSource, TKey>, Func<TKey, IEnumerable<TSource>, TResult>, IEnumerable<TResult>> f2 = Enumerable.GroupBy;
            var translator = new GroupBy2Translator();
            AddTranslator(f.Method, translator);
            AddTranslator(f2.Method, translator);
        }
        public class GroupBy2Translator : MethodTranslator
        {
            public override Expression Translate(MethodCallExpression mce, Expression translatedSource, IEnumerable<Expression> otherArgs, QueryAnalysisContext context)
            {
                var _otherArgs = otherArgs.ToArray();
                bool isQueryable = DefinedOnQueryable(mce);

                //Func<TSource, TKey>
                var keySelectorTranslation = QueryTranslationVisitor.TranslateLambda(_otherArgs[0], new[] { context.QueryableType.NonPrimitiveEnumerableItemType }, context, isQueryable);

                //Func<TKey, IEnumerable<TSource>, TResult>
                var resultSelectorTranslation = QueryTranslationVisitor.TranslateLambda(_otherArgs[1], new[] { keySelectorTranslation.QueryableType, context.QueryableType }, context, isQueryable);

                var newArgs = new Expression[]
                {
                    translatedSource,
                    keySelectorTranslation.TranslatedExpression,
                    resultSelectorTranslation.TranslatedExpression
                };

                //GroupBy2<TSource, TKey, TResult>
                var translatedMethod = mce.Method.GetGenericMethodDefinition().MakeGenericMethod(
                    context.QueryableType.NonPrimitiveEnumerableItemType.TranslatedType,
                    keySelectorTranslation.QueryableType.TranslatedType,
                    resultSelectorTranslation.QueryableType.TranslatedType);

                //IQueryable<TResult>
                context.QueryableType = RuntimeTypes.CreateEnumerable(resultSelectorTranslation.QueryableType);

                return Expression.Call(mce.Object, translatedMethod, newArgs);
            }

            public override bool ModifyTranslatedSource(MethodCallExpression mce, QueryAnalysisContext context)
            {
                return true;
            }

            public override bool ModifyTranslatedResult(MethodCallExpression mce, QueryAnalysisContext context)
            {
                return context.IsOuterExpression;
            }
        }
    }

    public class GroupBy3<TSource, TKey, TElement> : MethodTranslatorList
    {
        public GroupBy3()
        {
            Func<IQueryable<TSource>, Expression<Func<TSource, TKey>>, Expression<Func<TSource, TElement>>, IQueryable<IGrouping<TKey, TElement>>> f = Queryable.GroupBy;
            Func<IEnumerable<TSource>, Func<TSource, TKey>, Func<TSource, TElement>, IEnumerable<IGrouping<TKey, TElement>>> f2 = Enumerable.GroupBy;
            var translator = new GroupBy3Translator();
            AddTranslator(f.Method, translator);
            AddTranslator(f2.Method, translator);
        }

        public class GroupBy3Translator : MethodTranslator
        {
            public override Expression Translate(MethodCallExpression mce, Expression translatedSource, IEnumerable<Expression> otherArgs, QueryAnalysisContext context)
            {
                var _otherArgs = otherArgs.ToArray();
                bool isQueryable = DefinedOnQueryable(mce);

                //Func<TSource, TKey>
                var keySelectorTranslation = QueryTranslationVisitor.TranslateLambda(_otherArgs[0], new[] { context.QueryableType.NonPrimitiveEnumerableItemType }, context, isQueryable);

                //Expression<Func<TSource, TElement>>
                var elementSelectorTranslation = QueryTranslationVisitor.TranslateLambda(_otherArgs[1], new[] { context.QueryableType.NonPrimitiveEnumerableItemType }, context, isQueryable);

                var newArgs = new Expression[]
                {
                    translatedSource,
                    keySelectorTranslation.TranslatedExpression,
                    elementSelectorTranslation.TranslatedExpression
                };

                //GroupBy3<TSource, TKey, TElement>
                var translatedMethod = mce.Method.GetGenericMethodDefinition().MakeGenericMethod(
                    context.QueryableType.NonPrimitiveEnumerableItemType.TranslatedType,
                    keySelectorTranslation.QueryableType.TranslatedType,
                    elementSelectorTranslation.QueryableType.TranslatedType);

                //IQueryable<IGrouping<TKey, TElement>>
                var groupingType = RuntimeTypes.CreateGrouping(keySelectorTranslation.QueryableType, elementSelectorTranslation.QueryableType);
                var queryableType = RuntimeTypes.CreateEnumerable(groupingType);

                context.QueryableType = queryableType;

                return Expression.Call(mce.Object, translatedMethod, newArgs);
            }

            public override bool ModifyTranslatedSource(MethodCallExpression mce, QueryAnalysisContext context)
            {
                return true;
            }

            public override bool ModifyTranslatedResult(MethodCallExpression mce, QueryAnalysisContext context)
            {
                return context.IsOuterExpression;
            }
        }
    }

    public class GroupBy4<TSource, TKey, TElement, TResult> : MethodTranslatorList
    {
        public GroupBy4()
        {
            Func<IQueryable<TSource>, Expression<Func<TSource, TKey>>, Expression<Func<TSource, TElement>>, Expression<Func<TKey, IEnumerable<TElement>, TResult>>, IQueryable<TResult>> f = Queryable.GroupBy;
            Func<IEnumerable<TSource>, Func<TSource, TKey>, Func<TSource, TElement>, Func<TKey, IEnumerable<TElement>, TResult>, IEnumerable<TResult>> f2 = Enumerable.GroupBy;
            var translator = new GroupBy4Translator();
            AddTranslator(f.Method, translator);
            AddTranslator(f2.Method, translator);
        }

        public class GroupBy4Translator : MethodTranslator
        {
            public override Expression Translate(MethodCallExpression mce, Expression translatedSource, IEnumerable<Expression> otherArgs, QueryAnalysisContext context)
            {
                var _otherArgs = otherArgs.ToArray();
                bool isQueryable = DefinedOnQueryable(mce);

                //Func<TSource, TKey>
                var keySelectorTranslation = QueryTranslationVisitor.TranslateLambda(_otherArgs[0], new[] { context.QueryableType.NonPrimitiveEnumerableItemType }, context, isQueryable);

                //Expression<Func<TSource, TElement>>
                var elementSelectorTranslation = QueryTranslationVisitor.TranslateLambda(_otherArgs[1], new[] { context.QueryableType.NonPrimitiveEnumerableItemType }, context, isQueryable);

                //Func<TKey, IEnumerable<TElement>, TResult>
                var queryableElements = RuntimeTypes.CreateEnumerable(elementSelectorTranslation.QueryableType);
                var resultSelectorTranslation = QueryTranslationVisitor.TranslateLambda(_otherArgs[2], new[] { keySelectorTranslation.QueryableType, queryableElements }, context, isQueryable);

                var newArgs = new Expression[]
                {
                    translatedSource,
                    keySelectorTranslation.TranslatedExpression,
                    elementSelectorTranslation.TranslatedExpression,
                    resultSelectorTranslation.TranslatedExpression
                };

                //GroupBy4<TSource, TKey, TElement, TResult>
                var translatedMethod = mce.Method.GetGenericMethodDefinition().MakeGenericMethod(
                    context.QueryableType.NonPrimitiveEnumerableItemType.TranslatedType,
                    keySelectorTranslation.QueryableType.TranslatedType,
                    elementSelectorTranslation.QueryableType.TranslatedType,
                    resultSelectorTranslation.QueryableType.TranslatedType);

                //IQueryable<TResult>
                context.QueryableType = RuntimeTypes.CreateEnumerable(resultSelectorTranslation.QueryableType);

                return Expression.Call(mce.Object, translatedMethod, newArgs);
            }

            public override bool ModifyTranslatedSource(MethodCallExpression mce, QueryAnalysisContext context)
            {
                return true;
            }

            public override bool ModifyTranslatedResult(MethodCallExpression mce, QueryAnalysisContext context)
            {
                return context.IsOuterExpression;
            }
        }
    }
}