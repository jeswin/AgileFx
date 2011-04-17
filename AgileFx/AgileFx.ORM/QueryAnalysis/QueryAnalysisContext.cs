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

using AgileFx.ORM.Utils;
using AgileFx.ORM.QueryAnalysis.TypeTracking;

namespace AgileFx.ORM.QueryAnalysis
{
    public class ParameterReplacement
    {
        public Expression ReplacementExpression { get; set; }
        public SimpleType Type { get; set; }
    }

    public class QueryAnalysisContext
    {
        public Dictionary<ParameterExpression, ParameterReplacement> ParameterDictionary { get; private set; }

        //Query Type and Includes
        public SimpleType QueryableType { get; set; }

        //This is set by query analysis when it encounters the constant expression containing our root query.
        public IEntityQuery RootEntityQuery = null;

        //Usually passed in from the context. This allows us to reuse the TypeTranslationUtil for the entire context.
        //TypeTranslationUtil implements caching if Type metadata, and hence it is good to reuse it.
        public TypeTranslationUtil TypeTranslationUtil { get; private set; }
        
        //The call stack enables methods lower down to peek at methods higher up.
        //This should only be used for validation.
        public Stack<MethodCallExpression> CallStack = new Stack<MethodCallExpression>();

        public bool ModifyProjection { get; set; }

        public bool IsOuterExpression
        { 
            get { return CallStack.Count() == 1; }
        }

        public QueryAnalysisContext(TypeTranslationUtil typeTranslationUtil)
            : this(typeTranslationUtil, new Dictionary<ParameterExpression, ParameterReplacement>())
        {
        }

        public QueryAnalysisContext(TypeTranslationUtil typeTranslationUtil, Dictionary<ParameterExpression, ParameterReplacement> paramDictionary)
        {
            this.TypeTranslationUtil = typeTranslationUtil;
            this.ParameterDictionary = new Dictionary<ParameterExpression, ParameterReplacement>();

            if (paramDictionary != null)
                foreach (var item in paramDictionary)
                    this.ParameterDictionary.Add(item.Key, item.Value);
        }
    }
}
