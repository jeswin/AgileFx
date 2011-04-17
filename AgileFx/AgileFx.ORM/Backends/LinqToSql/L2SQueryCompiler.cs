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
using System.Data.Linq;

using AgileFx.ORM.QueryCompilation;
using AgileFx.ORM.Reflection;
using AgileFx.ORM.Utils;

namespace AgileFx.ORM.Backends.LinqToSql
{
    class L2SQueryCompiler : QueryCompiler
    {
        public override TypeTranslationUtil GetTypeTranslationUtil()
        {
            return new L2STypeTranslationUtil();
        }

        public override System.Reflection.MethodInfo GetCompileMethod(CompilableQueryAnalysisResult analysisResult)
        {
            return MethodFinder.LinqToSqlCompiledQuery
                .Compile(analysisResult.TranslatedCompilableLambda.Type.GetGenericArguments());
        }

        public override object InvokeCompiledMethod(object compiledBackendMethod, EntityContext entityContext, params object[] args)
        {
            var allArgs = new List<object>();
            allArgs.Add((entityContext._InternalServices.TableEntityContext as DataContextWrapper).LinqToSqlContext);
            allArgs.AddRange(args);

            var result = compiledBackendMethod.GetType().GetMethod("Invoke")
                .Invoke(compiledBackendMethod, allArgs.ToArray());

            return result;
        }

        public override EntityQueryProvider GetQueryProvider(EntityContext entityContext)
        {
            return new L2SQueryProvider(entityContext);
        }
    }
}
