using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using AgileFx.ORM.QueryAnalysis.TypeTracking;
using AgileFx.ORM.Utils;
using System.Collections;

namespace AgileFx.ORM.QueryAnalysis.MethodTranslation
{
    public abstract class MethodTranslator
    {
        public abstract Expression Translate(MethodCallExpression mce, Expression translatedFirstArg, IEnumerable<Expression> otherArgs, QueryAnalysisContext context);
        public abstract bool ModifyTranslatedSource(MethodCallExpression mce, QueryAnalysisContext context);
        public abstract bool ModifyTranslatedResult(MethodCallExpression mce, QueryAnalysisContext context);

        protected bool DefinedOnQueryable(MethodCallExpression mce)
        {
            return mce.Method.DeclaringType == typeof(Queryable);
        }

        protected bool EnumerableArgumentIsLambda(Expression arg, bool isQueryable)
        {
            if (isQueryable)
                return arg is UnaryExpression && (arg as UnaryExpression).Operand is LambdaExpression;
            else
                return arg is LambdaExpression;
        }

        protected MethodInfo GetGenericMethod(MethodInfo originalMethod, params Type[] typeArgs)
        {
            return originalMethod.GetGenericMethodDefinition().MakeGenericMethod(typeArgs);
        }
    }
}
