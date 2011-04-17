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

namespace AgileFx.ORM
{
    public class SetOverride : IDisposable
    {
        public SetOverride(EntityContext context)
        {
            EntityContext.InternalServices.AssertOverridable();
            EntityContext.InternalServices.OverridingContext = context;
        }

        public void Dispose()
        {
            EntityContext.InternalServices.OverridingContext = null;
        }
    }
}
