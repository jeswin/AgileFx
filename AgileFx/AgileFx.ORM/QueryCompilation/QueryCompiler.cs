/* AgileFx Version 2.0
 * An open-source framework for rapid development of applications and services using Microsoft.net
 * Developed by: AgileHead
 * Website: www.agilefx.org
 * This component is licensed under the terms of the Apache 2.0 License.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Collections;
using System.Reflection;

using AgileFx.ORM.QueryAnalysis;
using AgileFx.ORM.Reflection;
using AgileFx.ORM.Materialization;
using AgileFx.ORM.Utils;

namespace AgileFx.ORM.QueryCompilation
{
    public abstract class QueryCompiler
    {
        public abstract TypeTranslationUtil GetTypeTranslationUtil();
        public abstract MethodInfo GetCompileMethod(CompilableQueryAnalysisResult analysisResult);
        public abstract object InvokeCompiledMethod(object compiledBackendMethod, EntityContext entityContext, params object[] args);
        public abstract EntityQueryProvider GetQueryProvider(EntityContext entityContext);

        public virtual Func<EntityContext, TResult> Compile<TResult>(Expression<Func<EntityContext, TResult>> query)
        {
            var func = GetFunc<TResult>(query);
            return (context) =>
            {
                return func(context, new object[] { });
            };
        }

        public virtual Func<EntityContext, TArg1, TResult> Compile<TArg1, TResult>(Expression<Func<EntityContext, TArg1, TResult>> query)
        {
            var func = GetFunc<TResult>(query);
            return (context, arg1) =>
                {
                    return func(context, new object[] { arg1 });
                };
        }

        public virtual Func<EntityContext, TArg1, TArg2, TResult> Compile<TArg1, TArg2, TResult>(Expression<Func<EntityContext, TArg1, TArg2, TResult>> query)
        {
            var func = GetFunc<TResult>(query);
            return (context, arg1, arg2) =>
            {
                return func(context, new object[] { arg1, arg2 });
            };
        }


        public virtual Func<EntityContext, TArg1, TArg2, TArg3, TResult> Compile<TArg1, TArg2, TArg3, TResult>(Expression<Func<EntityContext, TArg1, TArg2, TArg3, TResult>> query)
        {
            var func = GetFunc<TResult>(query);
            return (context, arg1, arg2, arg3) =>
            {
                return func(context, new object[] { arg1, arg2, arg3 });
            };
        }

        #if CLR_AT_LEAST_4_0

        public virtual Func<EntityContext, TArg1, TArg2, TArg3, TArg4, TResult> Compile<TArg1, TArg2, TArg3, TArg4, TResult>(Expression<Func<EntityContext, TArg1, TArg2, TArg3, TArg4, TResult>> query)
        {
            var func = GetFunc<TResult>(query);
            return (context, arg1, arg2, arg3, arg4) =>
            {
                return func(context, new object[] { arg1, arg2, arg3, arg4 });
            };
        }

        public virtual Func<EntityContext, TArg1, TArg2, TArg3, TArg4, TArg5, TResult> Compile<TArg1, TArg2, TArg3, TArg4, TArg5, TResult>(Expression<Func<EntityContext, TArg1, TArg2, TArg3, TArg4, TArg5, TResult>> query)
        {
            var func = GetFunc<TResult>(query);
            return (context, arg1, arg2, arg3, arg4, arg5) =>
            {
                return func(context, new object[] { arg1, arg2, arg3, arg4, arg5 });
            };
        }

        public virtual Func<EntityContext, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult> Compile<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult>(Expression<Func<EntityContext, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult>> query)
        {
            var func = GetFunc<TResult>(query);
            return (context, arg1, arg2, arg3, arg4, arg5, arg6) =>
            {
                return func(context, new object[] { arg1, arg2, arg3, arg4, arg5, arg6 });
            };
        }

        public virtual Func<EntityContext, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult> Compile<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult>(Expression<Func<EntityContext, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult>> query)
        {
            var func = GetFunc<TResult>(query);
            return (context, arg1, arg2, arg3, arg4, arg5, arg6, arg7) =>
            {
                return func(context, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7 });
            };
        }

#endif

        private Func<EntityContext, object[], TResult> GetFunc<TResult>(LambdaExpression query)
        {
            var analyzer = new CompilableQueryAnalyzer(query, GetTypeTranslationUtil());
            var analysisResult = analyzer.GetResult();

            //Find L2S.CompiledQuery.Compile<...>()
            //  and invoke the backend compiler; backendCompiledMethod is a Func<,..>
            var compiledBackendMethod = GetCompileMethod(analysisResult).Invoke(null, new object[] { analysisResult.TranslatedCompilableLambda });

            //Compile the client-side part of the expression and get a Func<>
            //  This function the materialized form and works on it (like call a ToList() for instance).
            object postMaterializationLambda = null;
            if (analysisResult.PostMaterializationLambda != null)
                postMaterializationLambda = analysisResult.PostMaterializationLambda.Compile();

            //l2sCompileResult lambda can return two types
            //  1. IQueryable
            //  2. An Entity itself, as a result of the call to First(), Single() etc.            
            if (!TypesUtil.IsNonPrimitiveCollection(analysisResult.MaterializableExpression.Type))
            {
                return (context, args) =>
                    {
                        var backendResults = InvokeCompiledMethod(compiledBackendMethod, context as EntityContext, args);

                        var singleObjectMaterializer = Activator.CreateInstance(typeof(SingleObjectMaterializer<>)
                            .MakeGenericType(analysisResult.MaterializableExpression.Type), new object[] { context });

                        var materializedResult = singleObjectMaterializer.GetType().GetMethod("MakeResult")
                            .Invoke(singleObjectMaterializer, new object[] { backendResults, analysisResult.QueryableType });

                        return (TResult)InvokePostMaterializationLambda_IfExists(materializedResult, postMaterializationLambda);
                    };
            }
            else
            {
                return (context, args) =>
                    {
                        var queryType = TypesUtil.GetGenericArgumentForBaseType(analysisResult.MaterializableExpression.Type, typeof(IQueryable<>));
                        var compiledQuery = Activator.CreateInstance(typeof(CompiledQuery<>).MakeGenericType(queryType),
                            new object[] { GetQueryProvider(context as EntityContext), this, args, compiledBackendMethod, analysisResult.QueryableType.NonPrimitiveEnumerableItemType });

                        return (TResult)InvokePostMaterializationLambda_IfExists(compiledQuery, postMaterializationLambda);

                    };
            }
        }

        public object InvokePostMaterializationLambda_IfExists(object materializedResult, object postMaterializationLambda)
        {
            if (postMaterializationLambda != null)
            {
                return postMaterializationLambda.GetType().GetMethod("Invoke")
                    .Invoke(postMaterializationLambda, new object[] { materializedResult });
            }
            else
                return materializedResult;
        }
    }
}