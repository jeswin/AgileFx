using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Practices.RepositoryFactory.SchemaDiscovery.Properties
{
    public class Resources
    {
        public static string EscapeObjectName { get { return "[{0}]"; } }
        public static string EscapeObjectNameWithSchema { get { return "[{0}].[{1}]"; } }
        public static string FullNameWithSchema { get { return "{0}.{1}"; } }
        public static string NotImplementedException { get { return "The method or operation is not implemented."; } }
    }
}
