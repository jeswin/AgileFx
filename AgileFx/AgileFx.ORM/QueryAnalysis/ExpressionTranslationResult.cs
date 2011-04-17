using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using AgileFx.ORM.QueryAnalysis.TypeTracking;

namespace AgileFx.ORM.QueryAnalysis
{
    public class ExpressionTranslationResult
    {
        public SimpleType QueryableType { get; private set; }
        public Expression TranslatedExpression { get; private set; }

        public ExpressionTranslationResult(Expression translatedExpression, SimpleType queryableType)
        {
            this.TranslatedExpression = translatedExpression;
            this.QueryableType = queryableType;
        }
    }
}
