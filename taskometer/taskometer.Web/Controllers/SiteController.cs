using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AgileFx.MVC;
using taskometer.Core.Models;
using taskometer.Core;
using AgileFx.MVC.ViewModels;
using AgileFx.MVC.Controllers;

namespace taskometer.Web.Controllers
{
    [HandleError]
    public class SiteController : DefaultController<DefaultViewModel>
    {
        public ActionResult Index()
        {  
            return View();
        }

        public ActionResult Features()
        {
            return View();
        }

        public ActionResult Pricing()
        {
            return View();
        }

        public ActionResult Signup(string plan)
        {
            return View<ViewModels.Signup>(v =>
                {
                    v.Plan = SiteSettings.ActivePlans.Find(s => s.keyword == plan);
                    v.SetupWebsite = true;
                    v.SetupProjectManagement = true;
                    v.SetupIssueTracker = true;
                    v.SetupWiki = true;
                    v.SetupBlog = true;
                    v.WebsiteUrlType = ViewModels.Signup.AppUrlTypes.USE_DOMAIN;
                    v.ProjectManagementUrlType = ViewModels.Signup.AppUrlTypes.SUB_DIRECTORY;
                    v.ProjectManagementSubDirectory = "projects";
                    v.IssueTrackerUrlType = ViewModels.Signup.AppUrlTypes.SUB_DIRECTORY;
                    v.IssueTrackerSubDirectory = "issues";
                    v.WikiUrlType = ViewModels.Signup.AppUrlTypes.SUB_DIRECTORY;
                    v.WikiSubDirectory = "wiki";
                    v.BlogUrlType = ViewModels.Signup.AppUrlTypes.SUB_DIRECTORY;
                    v.BlogSubDirectory = "blog";
                    v.AdminUrlType = ViewModels.Signup.AppUrlTypes.SUB_DIRECTORY;
                    v.AdminSubDirectory = "admin";
                });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Signup(string plan, FormCollection coll)
        {
            return Content("Invalid invite code. Please try again.");
            //try
            //{
            //    var context = new taskometer.Core.Models.Entities();

            //    var signupViewModel = new taskometer.Web.ViewModels.Signup();
            //    signupViewModel.UpdateFromCollection(coll);
                
            //    var logo = SiteSettings.DEFAULT_LOGO;
            //    var domain = signupViewModel.IsCustomDomain ? signupViewModel.Domain : signupViewModel.Domain + "." + SiteSettings.Domain;

            //    var tenant = Tenant.Create(signupViewModel.Company, domain, plan, signupViewModel.Timezone, logo, context);

            //    if (signupViewModel.SetupWebsite)
            //    {
            //        var urlType = coll["websiteUrlType"];
            //        if (urlType == "subDirectory")
            //        {
                        
            //        }
                   
            //    }

            //    context.SaveChanges();

            //    var admin = taskometer.Core.Models.User.Create(coll["AdminUsername"], coll["FirstName"],
            //        coll["LastName"], ROLES.ADMINISTRATOR, tenant);
            //    admin.Account.SetPassword(coll["Password"]);
            //    admin.Account.ProviderType = "Application";
            //    context.SaveChanges();

            //    return Redirect("http://" + coll["Domain"] + "/admin");
            //}
            //catch (Exception)
            //{
            //    return View<ViewModels.Signup>(v =>
            //    {
            //        v.AdminUsername = coll["AdminUsername"];
            //        v.Company = coll["Company"];
            //        v.Domain = coll["Domain"];
            //        v.Email = coll["Email"];
            //        v.FirstName = coll["FirstName"];
            //        v.LastName = coll["LastName"];
            //        v.Plan = SiteSettings.ActivePlans.Find(s => s.keyword == plan);
            //        v.Timezone = coll["Timezone"];
            //    });
            //}
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Terms()
        {
            return View();
        }
    }
}
