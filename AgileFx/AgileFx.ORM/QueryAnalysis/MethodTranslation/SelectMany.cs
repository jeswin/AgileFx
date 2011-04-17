using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using AgileFx.ORM.Utils;
using AgileFx.ORM.QueryAnalysis.TypeTracking;
using System.Collections;

namespace AgileFx.ORM.QueryAnalysis.MethodTranslation
{
    public abstract class SelectManyTranslator : MethodTranslator
    {
        protected virtual LambdaExpression GetTranslatedLambda(Type originalLambdaType, Expression translatedBody,
            IEnumerable<ParameterExpression> translatedParameters)
        {
            /*
             * Lambda.Call when called without the "DelegateType" paramter automatically infers the 
             * LambdaExpression Type from the body. Some IQueryable Extension Methods take  
             * Expression<Func<..,IEnumerable<T>,..> but Lambda.Call would have inferred Expression<Func<..,SomeCollection<T>..>>                 * 
             * We need to change this, or this will fail. 
             * This is a consequence of Expression<Func<Derived>> not being derived from Expression<Func<Base>>                 * 
             * I know this is confusing, editing this is a TODO.
             */
            var genericArgs = new List<Type>();
            foreach (var param in translatedParameters) genericArgs.Add(param.Type);
            genericArgs.Add(translatedBody.Type);

            var newType = originalLambdaType.GetGenericTypeDefinition().MakeGenericType(genericArgs
                .Select(t => (t.IsGenericType && t.GetGenericArguments().Length == 1
                    && typeof(IEnumerable).IsAssignableFrom(t) 
                    && typeof(IEnumerable<>).MakeGenericType(t.GetGenericArguments()).IsAssignableFrom(t))
                    ? typeof(IEnumerable<>).MakeGenericType(t.GetGenericArguments()) : t).ToArray());

            return Expression.Lambda(newType, translatedBody, translatedParameters);
        }

        protected ExpressionTranslationResult TranslateLambdaToIEnumerableFormat(Expression lambdaArg, SimpleType[] parameterTypes, QueryAnalysisContext context, bool isQueryable)
        {
            var lambda = QueryTranslationVisitor.GetLambda(lambdaArg, isQueryable);
            var translation = QueryTranslationVisitor.TranslateLambda(lambda, parameterTypes, context);

            //select many requires enumerable return type in collection selector lambda delegate.
            //hence we need convert to IEnumerable<>
            var translatedLambda = translation.TranslatedExpression as LambdaExpression;
            var translatedLambdaArg = QueryTranslationVisitor.FormatLambda(GetTranslatedLambda
                (translatedLambda.Type, translatedLambda.Body, translatedLambda.Parameters), isQueryable);

            return new ExpressionTranslationResult(translatedLambdaArg, translation.QueryableType);
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

    public class SelectMany1<TSource, TResult> : MethodTranslatorList
    {
        public SelectMany1()
        {
            Func<IQueryable<TSource>, Expression<Func<TSource, IEnumerable<TResult>>>, IQueryable<TResult>> f1 = Queryable.SelectMany;
            Func<IEnumerable<TSource>, Func<TSource, IEnumerable<TResult>>, IEnumerable<TResult>> f2 = Enumerable.SelectMany;
            var translator = new SelectMany1Translator();
            AddTranslator(f1.Method, translator);
            AddTranslator(f2.Method, translator);
        }

        public class SelectMany1Translator : SelectManyTranslator
        {
            public override Expression Translate(MethodCallExpression mce, Expression translatedSource, IEnumerable<Expression> otherArgs, QueryAnalysisContext context)
            {
                bool isQueryable = DefinedOnQueryable(mce);

                //Func<TSource, IEnumerable<TResult>>
                var selectorTranslation = TranslateLambdaToIEnumerableFormat(otherArgs.First(), 
                    new[] { context.QueryableType.NonPrimitiveEnumerableItemType }, context, isQueryable);

                //Handling Many-to-Many Case.. Converting to SelectMany3 case
                if (selectorTranslation.QueryableType.NonPrimitiveEnumerableItemType is ManyToManyMapType)
                {
                    var mapType = selectorTranslation.QueryableType.NonPrimitiveEnumerableItemType as ManyToManyMapType;

                    var sourceTypeParam = context.QueryableType.NonPrimitiveEnumerableItemType.GetParameter("p");
                    var mapParam = mapType.GetParameter("map");
                    var mappedItemSelector = Expression.Lambda(mapType.GetTranslatedParameterOrMember(mapParam), sourceTypeParam, mapParam);

                    var newArgs2 = new Expression[] 
                    {
                        translatedSource, 
                        selectorTranslation.TranslatedExpression, 
                        isQueryable ? Expression.Quote(mappedItemSelector) as Expression : mappedItemSelector
                    };

                    Func<IQueryable<object>, Expression<Func<object, IEnumerable<object>>>, Expression<Func<object, object, object>>, IQueryable<object>> f1 = Queryable.SelectMany;
                    Func<IEnumerable<object>, Func<object, IEnumerable<object>>, Func<object, object, object>, IEnumerable<object>> f2 = Enumerable.SelectMany;
                    var newSelectManyMethod = (isQueryable ? f1.Method : f2.Method).GetGenericMethodDefinition()
                        .MakeGenericMethod(context.QueryableType.NonPrimitiveEnumerableItemType.TranslatedType, 
                            selectorTranslation.QueryableType.NonPrimitiveEnumerableItemType.TranslatedType, mappedItemSelector.Body.Type);

                    context.QueryableType = RuntimeTypes.CreateEnumerable(new SimpleType(mapType.MappedItemType, mappedItemSelector.Body.Type));
                    return Expression.Call(mce.Object, newSelectManyMethod, newArgs2);
                }

                var newArgs = new Expression[]
                {
                    translatedSource,
                    selectorTranslation.TranslatedExpression
                };

                //SelectMany<TSource, TResult>
                var translatedMethod = mce.Method.GetGenericMethodDefinition().MakeGenericMethod(
                    context.QueryableType.NonPrimitiveEnumerableItemType.TranslatedType,
                    selectorTranslation.QueryableType.NonPrimitiveEnumerableItemType.TranslatedType);

                //IQueryable<TResult>
                context.QueryableType = RuntimeTypes.CreateEnumerable(selectorTranslation.QueryableType.NonPrimitiveEnumerableItemType);

                return Expression.Call(mce.Object, translatedMethod, newArgs) as Expression;
            }
        }
    }

    public class SelectMany2<TSource, TResult> : MethodTranslatorList
    {
        public SelectMany2()
        {
            Func<IQueryable<TSource>, Expression<Func<TSource, int, IEnumerable<TResult>>>, IQueryable<TResult>> f1 = Queryable.SelectMany;
            Func<IEnumerable<TSource>, Func<TSource, int, IEnumerable<TResult>>, IEnumerable<TResult>> f2 = Enumerable.SelectMany;
            var translator = new SelectMany2Translator();
            AddTranslator(f1.Method, translator);
            AddTranslator(f2.Method, translator);
        }

        public class SelectMany2Translator : SelectManyTranslator
        {
            public override Expression Translate(MethodCallExpression mce, Expression translatedSource, IEnumerable<Expression> otherArgs, QueryAnalysisContext context)
            {
                bool isQueryable = DefinedOnQueryable(mce);

                //Expression<Func<TSource, int, IEnumerable<TResult>>
                var selectorTranslation = TranslateLambdaToIEnumerableFormat(otherArgs.First(),
                    new[] { context.QueryableType.NonPrimitiveEnumerableItemType,
                            new SimpleType(typeof(int)) },
                    context, isQueryable);

                var newArgs = new Expression[]
                {
                    translatedSource,
                    selectorTranslation.TranslatedExpression
                };

                //SelectMany<TSource, TResult>
                var translatedMethod = mce.Method.GetGenericMethodDefinition().MakeGenericMethod(
                    context.QueryableType.NonPrimitiveEnumerableItemType.TranslatedType,
                    selectorTranslation.QueryableType.NonPrimitiveEnumerableItemType.TranslatedType);

                context.QueryableType = RuntimeTypes.CreateEnumerable(selectorTranslation.QueryableType.NonPrimitiveEnumerableItemType);

                return Expression.Call(mce.Object, translatedMethod, newArgs) as Expression;
            }
        }
    }

    public class SelectMany3<TSource, TCollection, TResult> : MethodTranslatorList
    {
        public SelectMany3()
        {
            Func<IQueryable<TSource>, Expression<Func<TSource, IEnumerable<TCollection>>>, Expression<Func<TSource, TCollection, TResult>>, IQueryable<TResult>> f1 = Queryable.SelectMany;
            Func<IEnumerable<TSource>, Func<TSource, IEnumerable<TCollection>>, Func<TSource, TCollection, TResult>, IEnumerable<TResult>> f2 = Enumerable.SelectMany;
            var translator = new SelectMany3Translator();
            AddTranslator(f1.Method, translator);
            AddTranslator(f2.Method, translator);
        }

        public class SelectMany3Translator : SelectManyTranslator
        {
            public override Expression Translate(MethodCallExpression mce, Expression translatedSource, IEnumerable<Expression> otherArgs, QueryAnalysisContext context)
            {
                var _otherArgs = otherArgs.ToArray();
                bool isQueryable = DefinedOnQueryable(mce);

                //Func<TSource, IEnumerable<TResult>>
                var collectionSelectorTranslation = TranslateLambdaToIEnumerableFormat(_otherArgs[0], 
                    new[] { context.QueryableType.NonPrimitiveEnumerableItemType }, context, isQueryable);

                //Func<TSource, TCollection, TResult>
                var resultSelectorTranslation = QueryTranslationVisitor.TranslateLambda(_otherArgs[1],
                    new[] { context.QueryableType.NonPrimitiveEnumerableItemType,                           //TSource
                            collectionSelectorTranslation.QueryableType.NonPrimitiveEnumerableItemType },   //TCollection
                    context, isQueryable);

                var newArgs = new Expression[]
                {
                    translatedSource,
                    collectionSelectorTranslation.TranslatedExpression,
                    resultSelectorTranslation.TranslatedExpression
                };

                //SelectMany<TSource, TCollection, TResult>
                var translatedMethod = mce.Method.GetGenericMethodDefinition().MakeGenericMethod(
                    context.QueryableType.NonPrimitiveEnumerableItemType.TranslatedType,
                    collectionSelectorTranslation.QueryableType.NonPrimitiveEnumerableItemType.TranslatedType,
                    resultSelectorTranslation.QueryableType.TranslatedType);

                //IEnumerable<TResult>
                context.QueryableType = RuntimeTypes.CreateEnumerable(resultSelectorTranslation.QueryableType);

                return Expression.Call(mce.Object, translatedMethod, newArgs) as Expression;
            }
        }
    }

    public class SelectMany4<TSource, TCollection, TResult> : MethodTranslatorList
    {
        public SelectMany4()
        {
            Func<IQueryable<TSource>, Expression<Func<TSource, int, IEnumerable<TCollection>>>, Expression<Func<TSource, TCollection, TResult>>, IQueryable<TResult>> f1 = Queryable.SelectMany;
            Func<IEnumerable<TSource>, Func<TSource, int, IEnumerable<TCollection>>, Func<TSource, TCollection, TResult>, IEnumerable<TResult>> f2 = Enumerable.SelectMany;
            var translator = new SelectMany4Translator();
            AddTranslator(f1.Method, translator);
            AddTranslator(f2.Method, translator);
        }

        public class SelectMany4Translator : SelectManyTranslator
        {
            public override Expression Translate(MethodCallExpression mce, Expression translatedSource, IEnumerable<Expression> otherArgs, QueryAnalysisContext context)
            {
                var _otherArgs = otherArgs.ToArray();
                bool isQueryable = DefinedOnQueryable(mce);

                //Func<TSource, int, IEnumerable<TCollection>>
                var collectionSelectorTranslation = TranslateLambdaToIEnumerableFormat(_otherArgs[0], 
                    new[]   { context.QueryableType.NonPrimitiveEnumerableItemType,     //TSource
                              new SimpleType(typeof(int)) },                //int
                    context, isQueryable);

                //Func<TSource, TCollection, TResult>
                var resultSelectorTranslation = QueryTranslationVisitor.TranslateLambda(_otherArgs[1],
                    new[] { context.QueryableType.NonPrimitiveEnumerableItemType,                           //TSource
                            collectionSelectorTranslation.QueryableType.NonPrimitiveEnumerableItemType },   //TCollection
                    context, isQueryable);

                var newArgs = new Expression[]
                {
                    translatedSource,
                    collectionSelectorTranslation.TranslatedExpression
                };

                //SelectMany<TSource, TCollection, TResult>
                var translatedMethod = mce.Method.GetGenericMethodDefinition().MakeGenericMethod(
                    context.QueryableType.NonPrimitiveEnumerableItemType.TranslatedType,
                    collectionSelectorTranslation.QueryableType.NonPrimitiveEnumerableItemType.TranslatedType,
                    resultSelectorTranslation.QueryableType.TranslatedType);

                //IEnumerable<TResult>
                context.QueryableType = RuntimeTypes.CreateEnumerable(resultSelectorTranslation.QueryableType);

                return Expression.Call(mce.Object, translatedMethod, newArgs) as Expression;
            }
        }
    }
}
