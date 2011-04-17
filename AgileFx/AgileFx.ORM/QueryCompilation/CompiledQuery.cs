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
using AgileFx.ORM.Materialization;
using AgileFx.ORM.QueryAnalysis.TypeTracking;

namespace AgileFx.ORM.QueryCompilation
{
    public class CompiledQuery<T> : EntityQuery<T>, IEnumerable
    {
        object compiledBackendMethod;
        QueryCompiler queryCompiler;
        object[] args;
        SimpleType queryableType;

        public CompiledQuery(EntityQueryProvider provider, QueryCompiler queryCompiler, 
            object[] args, object compiledBackendMethod, SimpleType queryableType) : base(provider)
        {
            this.queryCompiler = queryCompiler;
            this.compiledBackendMethod = compiledBackendMethod;
            this.args = args;
            this.queryableType = queryableType;
        }

        public override IEnumerator<T> GetEnumerator()
        {
            var entityContext = (Provider as EntityQueryProvider).EntityContext;
            var iqueryableOfTableEntity = queryCompiler.InvokeCompiledMethod(compiledBackendMethod, entityContext, args);

            //backendResults is an IQueryable.
            var tableEntityResults = new List<object>();
            
            foreach(var item in (IQueryable)iqueryableOfTableEntity)
            {
                tableEntityResults.Add(item);
            }

            var materializedItems = new ResultListMaterializer<T>(entityContext)
                .MakeResultList(tableEntityResults, queryableType);

            foreach (var item in (IEnumerable)materializedItems)
            {
                yield return (T)item;
            }
        }

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
