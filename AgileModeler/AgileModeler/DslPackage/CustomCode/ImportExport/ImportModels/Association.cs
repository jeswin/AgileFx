using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Practices.RepositoryFactory.SchemaDiscovery.ObjectModel.Base
{
    public class Association
    {
        public string Name { get; set; }
        public string ChildTable { get; set; }
        public string ChildMember { get; set; }
        public string ParentTable { get; set; }
        public string ParentMember { get; set; }
    }
}
