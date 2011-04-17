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
using IQToolkit;
using System.Linq.Expressions;

using AgileFx.ORM.QueryAnalysis;
using AgileFx.ORM.Utils;
using AgileFx.ORM.QueryAnalysis.TypeTracking;

namespace AgileFx.ORM.QueryCompilation
{
    public class CompilableQueryAnalyzer : IQToolkit.ExpressionVisitor
    {
        LambdaExpression originalExpression;
        Expression materializableExpression;
        ParameterExpression postMaterializationInput;
        Expression postMaterializationFunctionBody;
        TypeTranslationUtil typeTranslationUtil;
        bool continueSearchForMaterializable = true;

        public CompilableQueryAnalyzer(LambdaExpression originalExpression, TypeTranslationUtil typeTranslationUtil)
        {
            this.originalExpression = originalExpression;
            this.typeTranslationUtil = typeTranslationUtil;
            Visit(originalExpression);
        }

        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            //We are going to start from the bottom, and work our way up.
            //  And find the first method (from bottom) which does not take an IQueryable.
            //To get to the bottom of the chain:
            //  1) Visit the first argument if the method is static
            //  2) Visit the Object property if the method is an instance method.

            if (m.Method.IsStatic) //An extension method
            {
                var firstArg = Visit(m.Arguments.First());

                //We are within the IQ chain
                if (continueSearchForMaterializable && MethodIsOnIQueryable(m))
                {
                    materializableExpression = m;
                }

                //This means we have just found our IQueryable, and we can stop searching
                if (continueSearchForMaterializable && materializableExpression != null && !MethodIsOnIQueryable(m))
                {
                    continueSearchForMaterializable = false;
                    this.postMaterializationInput = Expression.Parameter(m.Arguments.First().Type, "replacement");
                    this.postMaterializationFunctionBody = 
                        Expression.Call(null, m.Method, GetArguments(this.postMaterializationInput, m.Arguments.Slice(1)));
                }
            }
            else //method is not static.
            {
                var obj = Visit(m.Object);
            }

            return m;
        }

        private bool MethodIsOnIQueryable(MethodCallExpression mce)
        {
            return typeof(IQueryable).IsAssignableFrom(mce.Method.GetParameters().First().ParameterType);
        }

        protected override Expression VisitMemberAccess(MemberExpression m)
        {
            Visit(m.Expression);
            return m;
        }

        public CompilableQueryAnalysisResult GetResult()
        {
            var translationResults = GetTranslationResults();
            return new CompilableQueryAnalysisResult
            {
                OriginalExpression = originalExpression,
                MaterializableExpression = materializableExpression,
                TranslatedCompilableLambda = translationResults.TranslatedExpression as LambdaExpression,
                QueryableType = translationResults.QueryableType,
                PostMaterializationLambda = GetPostMaterializationLambda()
            };
        }

        private ExpressionTranslationResult GetTranslationResults()
        {
            var translatableLambda = Expression.Lambda(materializableExpression, originalExpression.Parameters.ToArray());

            //Add the other Params
            var parameters = originalExpression.Parameters
                .Select(p => new SimpleType(p.Type, typeTranslationUtil.GetTranslatedType(p.Type))).ToArray();

            var translationResults = QueryTranslationVisitor.TranslateLambda(translatableLambda, parameters.ToArray(),
                new QueryAnalysisContext(typeTranslationUtil), false);

            return translationResults;
        }

        private LambdaExpression GetPostMaterializationLambda()
        {
            //If continueSearchForMaterializable is FALSE, this means that we have a Post-Materialization part
            if (!continueSearchForMaterializable)
                return Expression.Lambda(postMaterializationFunctionBody, this.postMaterializationInput);
            else
                return null;
        }

        private Expression[] GetArguments(Expression first, IEnumerable<Expression> rest)
        {
            var args = new List<Expression>();
            args.Add(first);
            args.AddRange(rest);
            return args.ToArray();
        }
    }

    public class CompilableQueryAnalysisResult
    {
        public Expression OriginalExpression { get; set; }
        public Expression MaterializableExpression { get; set; }
        public LambdaExpression TranslatedCompilableLambda { get; set; }
        public SimpleType QueryableType { get; set; }
        public LambdaExpression PostMaterializationLambda { get; set; }
    }
}
