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

namespace AgileFx.ORM.QueryAnalysis
{
    public class QueryTranslationResult
    {
        public Expression TranslatedExpression { get; private set; }
        public QueryAnalysisContext AnalysisContext { get; private set; }

        public QueryTranslationResult(Expression translatedExpression, QueryAnalysisContext context)
        {
            this.TranslatedExpression = translatedExpression;
            this.AnalysisContext = context;
        }
    }
}
