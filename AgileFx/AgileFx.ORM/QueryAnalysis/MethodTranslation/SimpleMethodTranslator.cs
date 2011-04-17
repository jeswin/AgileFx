using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace AgileFx.ORM.QueryAnalysis.MethodTranslation
{
    public class SimpleMethodTranslator : MethodTranslator
    {
        public override Expression Translate(MethodCallExpression mce, Expression translatedSource1, IEnumerable<Expression> otherArgs, QueryAnalysisContext context)
        {
            var translatedMethod = mce.Method.GetGenericMethodDefinition().MakeGenericMethod(context.QueryableType.NonPrimitiveEnumerableItemType.TranslatedType);
            return Expression.Call(mce.Object, translatedMethod, translatedSource1);
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

    public class SimpleMethodWithLambdaTranslator : MethodTranslator
    {
        public override Expression Translate(MethodCallExpression mce, Expression translatedSource1, IEnumerable<Expression> otherArgs, QueryAnalysisContext context)
        {
            bool isQueryable = DefinedOnQueryable(mce);

            var translation = QueryTranslationVisitor.TranslateLambda(otherArgs.First(), new[] { context.QueryableType.NonPrimitiveEnumerableItemType }, context, isQueryable);

            var newArgs = new Expression[]
            {
                translatedSource1,
                translation.TranslatedExpression
            };

            var translatedMethod = mce.Method.GetGenericMethodDefinition().MakeGenericMethod(context.QueryableType.NonPrimitiveEnumerableItemType.TranslatedType);
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
