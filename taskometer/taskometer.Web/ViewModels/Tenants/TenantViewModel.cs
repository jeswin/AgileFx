using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AgileFx.MVC;
using taskometer.Core.Models;
using AgileFx.MVC.ViewModels;

namespace taskometer.Web.ViewModels.Tenants
{
    public class TenantViewModel : DefaultViewModel
    {
        public User LoggedInUser { get; set; }
        public Tenant Tenant { get; set; }
        public List<Website> CMS { get; set; }
        public List<Website> Wikis { get; set; }
        public List<Website> Blogs { get; set; }
    }
}
