using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using taskometer.Web.Routing;
using System.Web.Configuration;
using taskometer.Core.Models;

namespace taskometer.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.Add("Page", new PageRoute("{action}", new { controller = "Pages", action = "Display" }));
            routes.Add("Dashboard", new TenantRoute("Admin", new { controller = "Tenants", action = "Dashboard" }));
            routes.Add("Login ", new TenantRoute("Admin/Login", new { controller = "Tenants", action = "Login" }));
            routes.Add("Admin", new TenantRoute("Admin/{websiteid}/{action}/{id}", new { controller = "Tenants", action = "SiteDashboard", id = 0 }));
            //routes.Add("Website", new WebsiteRoute("{action}", new { controller = "Websites", action = "Display" }));

            routes.MapRoute(
                "Site",                                             // Route name
                "{action}",                                         // URL with parameters
                new { controller = "Site", action = "Index" }       // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);

            WikiPlex.Macros.Register<taskometer.Core.Wiki.CustomImageTagMacro>();
            WikiPlex.Macros.Register<taskometer.Core.Wiki.CustomAnchorTagMacro>();
            WikiPlex.Renderers.Register<taskometer.Core.Wiki.CustomImageTagRenderer>();
            WikiPlex.Renderers.Register<taskometer.Core.Wiki.CustomAnchorTagRenderer>();
            WikiPlex.Macros.Unregister<WikiPlex.Compilation.Macros.LinkMacro>();
            WikiPlex.Renderers.Unregister<WikiPlex.Formatting.LinkRenderer>();

            taskometer.Core.SiteSettings.HTTP_PORT = Convert.ToInt32(WebConfigurationManager.AppSettings["HTTP_PORT"]);
            taskometer.Core.SiteSettings.HTTPS_PORT = Convert.ToInt32(WebConfigurationManager.AppSettings["HTTPS_PORT"]);
            taskometer.Core.SiteSettings.Domain = WebConfigurationManager.AppSettings["DOMAIN"];

            Page.RenderAll();
        }
    }
}