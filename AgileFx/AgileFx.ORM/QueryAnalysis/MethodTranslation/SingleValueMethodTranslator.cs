using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using AgileFx.ORM.QueryAnalysis.TypeTracking;

namespace AgileFx.ORM.QueryAnalysis.MethodTranslation
{
    public class SingleValueMethodTranslator : SimpleMethodTranslator
    {
        public override Expression Translate(MethodCallExpression mce, Expression translatedSource1, IEnumerable<Expression> otherArgs, QueryAnalysisContext context)
        {
            var result = base.Translate(mce, translatedSource1, otherArgs, context);
            context.QueryableType = context.QueryableType.NonPrimitiveEnumerableItemType;
            return result;
        }

        public override bool ModifyTranslatedSource(MethodCallExpression mce, QueryAnalysisContext context)
        {
            return true;
        }

        public override bool ModifyTranslatedResult(MethodCallExpression mce, QueryAnalysisContext context)
        {
            return false;
        }
    }

    public class SingleValueMethodWithLambdaTranslator : SimpleMethodWithLambdaTranslator
    {
        public override Expression Translate(MethodCallExpression mce, Expression translatedSource1, IEnumerable<Expression> otherArgs, QueryAnalysisContext context)
        {
            var result = base.Translate(mce, translatedSource1, otherArgs, context);
            context.QueryableType = context.QueryableType.NonPrimitiveEnumerableItemType;
            return result;
        }

        public override bool ModifyTranslatedSource(MethodCallExpression mce, QueryAnalysisContext context)
        {
            return true;
        }

        public override bool ModifyTranslatedResult(MethodCallExpression mce, QueryAnalysisContext context)
        {
            return false;
        }
    }
}
