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
using System.Data.Linq;

namespace AgileFx.ORM.Backends.LinqToSql
{
    public class L2SQuery<T> : EntityQuery<T>
    {       
        public L2SQuery(EntityQueryProvider provider)
            : base(provider)
        {
        }

        public L2SQuery(EntityQueryProvider provider, Expression expression)
            : base(provider, expression)
        {
        }
    }
}
