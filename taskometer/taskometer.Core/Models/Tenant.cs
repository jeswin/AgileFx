using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AgileFx.ORM;

namespace taskometer.Core.Models
{
    public partial class Tenant
    {
        //This method returns the tenant corresponding to the URL.

        public static Tenant IdentifyTenant(Uri uri)
        {
            return new Entities().Cache().Invoke((_uri, context) =>
                {
                    var tenant = (from t in new Entities().Tenant
                                  where _uri.Host.EndsWith(t.Domain)
                                  select t).First();
                    return tenant;
                }, null, uri);
        }

        public static Tenant Create(string name, string domain, string plan, string timezone, string logo, EntityContext context)
        {
            var tenant = new Tenant();
            
            tenant.Name = name;
            tenant.Domain = domain;
            tenant.Plan = plan;
            tenant.Timezone = timezone;
            tenant.Logo = logo;

            CreateRoles(tenant);
            //CreateProjectWebsite(tenant);
            //CreateProjectPortal(tenant);
            //CreateIssuesPortal(tenant);
            
            context.AddObject(tenant);
            context.SaveChanges();
            
            return tenant;
        }

        private static void CreateRoles(Tenant tenant)
        {
            var user = new Role() { Name = ROLES.USER };
            var admin = new Role() { Name = ROLES.ADMINISTRATOR };

            tenant.Roles.Add(user);
            tenant.Roles.Add(admin);
        }

        private static void CreateProjectWebsite(Tenant tenant)
        {
            var website = Website.Create(WEBSITE_TYPE.CMS, "", tenant.Domain, "", "", tenant);
        }

        private static void CreateProjectPortal(Tenant tenant)
        {
            var projectPortal = new ProjectPortal();
            projectPortal.Domain = ""; //Cannot set this as yet.
            projectPortal.Path = "projects";
            tenant.ProjectPortal = projectPortal;
        }

        private static void CreateIssuesPortal(Tenant tenant)
        {
            var issuesPortal = new IssuesPortal();
            issuesPortal.Domain = ""; //Cannot set this as yet.
            issuesPortal.Path = "issues";
            tenant.IssuesPortal = issuesPortal;
        }
    }
}
