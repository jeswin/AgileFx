using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace taskometer.Core.Models
{
    public partial class PermissionSet
    {
        public static PermissionSet Create(Tenant tenant)
        {
            return new PermissionSet() { Tenant = tenant };
        }
    }
}
