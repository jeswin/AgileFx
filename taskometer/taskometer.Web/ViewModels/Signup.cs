using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AgileFx.MVC;
using taskometer.Core;
using AgileFx.MVC.ViewModels;

namespace taskometer.Web.ViewModels
{
    public class Signup : DefaultViewModel
    {

        public class AppUrlTypes
        {
            public const string USE_DOMAIN = "useDomain";
            public const string SUB_DIRECTORY = "subDirectory";
            public const string CUSTOM_DOMAIN = "customDomain";
        }

        public SubscriptionPlan Plan { get; set; }

        public string InviteCode { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public string Timezone { get; set; }

        public string AdminUsername { get; set; }
        public string Password { get; set; }
        
        public bool IsCustomDomain { get; set; }
        public string Domain { get; set; }
        
        public bool SetupWebsite { get; set; }
        public string WebsiteUrlType { get; set; }
        public string WebsiteSubDirectory { get; set; }
        public string WebsiteCustomDomain { get; set; }

        public bool SetupProjectManagement { get; set; }
        public string ProjectManagementUrlType { get; set; }
        public string ProjectManagementSubDirectory { get; set; }
        public string ProjectManagementCustomDomain { get; set; }

        public bool SetupIssueTracker { get; set; }
        public string IssueTrackerUrlType { get; set; }
        public string IssueTrackerSubDirectory { get; set; }
        public string IssueTrackerCustomDomain { get; set; }

        public bool SetupWiki { get; set; }
        public string WikiUrlType { get; set; }
        public string WikiSubDirectory { get; set; }
        public string WikiCustomDomain { get; set; }

        public bool SetupBlog { get; set; }
        public string BlogUrlType { get; set; }
        public string BlogSubDirectory { get; set; }
        public string BlogCustomDomain { get; set; }

        public bool SetupAdmin { get; set; }
        public string AdminUrlType { get; set; }
        public string AdminSubDirectory { get; set; }
        public string AdminCustomDomain { get; set; }
    }
}
