using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using AgileFx.ORM.QueryAnalysis.TypeTracking;

namespace AgileFx.ORM.QueryAnalysis.MethodTranslation
{
    public class ScalarMethodWithLambdaTranslator : SimpleMethodWithLambdaTranslator
    {
        public override Expression Translate(MethodCallExpression mce, Expression translatedSource1, IEnumerable<Expression> otherArgs, QueryAnalysisContext context)
        {
            var result = base.Translate(mce, translatedSource1, otherArgs, context);
            context.QueryableType = new SimpleType(mce.Type, result.Type);
            return result;
        }

        public override bool ModifyTranslatedResult(MethodCallExpression mce, QueryAnalysisContext context)
        {
            return false;
        }
    }

    public class ScalarMethodTranslator : SimpleMethodTranslator
    {
        public override Expression Translate(MethodCallExpression mce, Expression translatedSource1, IEnumerable<Expression> otherArgs, QueryAnalysisContext context)
        {
            var result = base.Translate(mce, translatedSource1, otherArgs, context);
            context.QueryableType = new SimpleType(mce.Type, result.Type);
            return result;
        }

        public override bool ModifyTranslatedResult(MethodCallExpression mce, QueryAnalysisContext context)
        {
            return false;
        }
    }
}
