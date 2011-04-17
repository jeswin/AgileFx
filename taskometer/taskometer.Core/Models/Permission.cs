using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace taskometer.Core.Models
{
    public partial class Permission
    {
        public static Permission Create(long assigneeId, string assigneeType, bool edit, bool view, PermissionSet permissionSet, Tenant tenant)
        {
            return new Permission()
            {
                Assignee = assigneeId,
                AssigneeType = assigneeType,
                Edit = edit,
                View = view,
                PermissionSet = permissionSet,
                Tenant = tenant
            };
        }
    }
}
