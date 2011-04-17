using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using taskometer.Web.ViewModels.Tenants;
using taskometer.Core.Models;

namespace taskometer.Web.ViewModels.Websites
{
    public class WebsiteViewModel : TenantViewModel
    {
        public Website Website { get; set; }
    }
}
