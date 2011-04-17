using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AgileFx.MVC;

namespace taskometer.Web.ViewModels.Tenants
{
    public class Login : TenantViewModel
    {
        public string Username { get; set; }
    }
}
