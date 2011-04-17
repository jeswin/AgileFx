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

using AgileFx.ORM.Utils;
using AgileFx.ORM.QueryCompilation;

namespace AgileFx.ORM.Backends.LinqToSql
{
    public abstract class L2SDataContext : EntityContext
    {
       
        public abstract DataContext GetLinqToSqlDataContext();

        public L2SDataContext(string connectionString) : base(connectionString)
        {
        }

        protected override ITableEntityContext GetTableEntityContext()
        {
            var l2scontext = GetLinqToSqlDataContext();
            l2scontext.DeferredLoadingEnabled = false;
            return new DataContextWrapper(l2scontext);
        }

        protected override EntityQueryProvider GetQueryProvider()
        {
            return new L2SQueryProvider(this);
        }

        public override QueryCompiler GetQueryCompiler()
        {
            return new L2SQueryCompiler();
        }

        protected override TypeTranslationUtil GetTypeTranslationUtil()
        {
            return new L2STypeTranslationUtil();
        }
    }
}
