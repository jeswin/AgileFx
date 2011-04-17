using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AgileFx;
using AgileFx.ORM;
using AgileFx.MVC;
using AgileFx.MVC.ViewModels;
using AgileFx.MVC.Utils;
using AgileFx.MVC.Controllers;
using AgileFx.Security;

using taskometer.Core;
using taskometer.Core.Models;

namespace taskometer.Web.Controllers
{
    [HandleError]
    public class TenantsController : DefaultController<DefaultViewModel>
    {
        public ActionResult Login(Tenant tenant)
        {
            return View<ViewModels.Tenants.Login>(v => v.Tenant = tenant);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Login(Tenant tenant, string username, string password, bool rememberMe, string returnUrl)
        {
            var account = Account.VerifyCredentials(tenant.Id, username, password);
            if (account != null)
            {
                var authCookie = AuthHelper.GetAuthTicketWithRoles(username, string.Join(",", account.Roles.Select(r => r.Name).ToArray())
                    , rememberMe, new TimeSpan(0, 30, 0));
                Response.Cookies.Add(authCookie);

                var url = string.IsNullOrEmpty(returnUrl) ? "/" : returnUrl;
                return Redirect(url);
            }
            else
            {
                return View<ViewModels.Tenants.Login>(v =>
                {
                    v.Tenant = new Entities().Tenant.Single(t => t.Id == tenant.Id);
                    v.MessageCode = "AUTHENTICATION_FAILED";
                    v.Username = username;
                });
            }
        }

        [Authorize()]
        public ActionResult ConfigureApps(Tenant tenant)
        {
            return View<ViewModels.Tenants.TenantViewModel>();
        }

        [Authorize()]
        public ActionResult Dashboard(Tenant tenant)
        {
            tenant.Load(tenant.EntityContext, t => t.Websites);
            return View<ViewModels.Tenants.Dashboard>(v =>
            {
                FillTenantViewModel(v, tenant);
            });
        }

        [Authorize()]
        public ActionResult SiteDashboard(Tenant tenant, long websiteId)
        {
            tenant.Load(tenant.EntityContext, t => t.Websites);
            var website = tenant.EntityContext.CreateQuery<Website>()
                .Single(w => w.Id == websiteId && w.Tenant.Id == tenant.Id);
            return View<ViewModels.Websites.Dashboard>(v =>
            {
                FillTenantViewModel(v, tenant);
                v.Website = website;
                v.Categories = website.EntityContext.CreateQuery<Category>().Where(c => c.Website.Id == websiteId).ToList();
                v.Pages = website.EntityContext.CreateQuery<Page>().Where(p => p.Category.Website.Id == websiteId)
                    .LoadRelated(p => p.Category).ToList();
                v.Templates = website.EntityContext.CreateQuery<Template>().Where(c => c.Website.Id == websiteId).ToList();
            });
        }

        [Authorize()]
        public ActionResult NewCategory(Tenant tenant, long websiteId, long id)
        {
            tenant.Load(tenant.EntityContext, t => t.Websites);
            return View<ViewModels.Websites.EditCategory>("EditCategory", v =>
            {
                v.Website = tenant.EntityContext.CreateQuery<Website>().Single(w => w.Id == websiteId && w.Tenant.Id == tenant.Id);
                FillTenantViewModel(v, tenant);
                v.Item = new Category
                {
                    DefaultPage = "index",
                    Parent = tenant.EntityContext.CreateQuery<Category>().Single(c => c.Website.Id == websiteId && c.Id == id)
                };
            });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [Authorize()]
        public ActionResult NewCategory(Tenant tenant, long websiteId, long id, FormCollection coll)
        {
            var website = tenant.EntityContext.CreateQuery<Website>().LoadRelated(w => w.Tenant)
                .Single(w => w.Id == websiteId && w.Tenant.Id == tenant.Id);
            var parentCategory = tenant.EntityContext.CreateQuery<Category>().Single(c => c.Website.Id == websiteId && c.Id == id);
            var category = Category.Create(coll["Name"], coll["DefaultPage"], parentCategory, website, CreateDefaultPermissionSet(tenant));
            website.EntityContext.SaveChanges();

            return Redirect("/Admin/" + website.Id);
        }

        [Authorize()]
        public ActionResult EditCategory(Tenant tenant, long websiteId, long id)
        {
            tenant.Load(tenant.EntityContext, t => t.Websites);
            return View<ViewModels.Websites.EditCategory>("EditCategory", v =>
            {
                v.Website = tenant.EntityContext.CreateQuery<Website>().Single(w => w.Id == websiteId && w.Tenant.Id == tenant.Id);
                FillTenantViewModel(v, tenant);
                v.Item = tenant.EntityContext.CreateQuery<Category>().Single(c => c.Id == id);
            });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [Authorize()]
        public ActionResult EditCategory(Tenant tenant, long websiteId, long id, FormCollection coll)
        {
            var category = tenant.EntityContext.CreateQuery<Category>().LoadRelated(c => c.Parent)
                .Single(c => c.Id == id && c.Website.Id == websiteId);
            UpdateModel<Category>(category, "", new[] { "Name", "DefaultPage"});
            tenant.EntityContext.SaveChanges();

            return Redirect("/Admin/" + websiteId);
        }

        [Authorize()]
        public ActionResult EditTemplate(Tenant tenant, long websiteId, long id)
        {
            tenant.Load(tenant.EntityContext, t => t.Websites);
            return View<ViewModels.Websites.EditTemplate>("EditTemplate", v =>
            {
                v.PageTitle = id > 0 ? "Edit Template" : "New Template";
                v.Website = tenant.EntityContext.CreateQuery<Website>().Single(w => w.Id == websiteId && w.Tenant.Id == tenant.Id);
                FillTenantViewModel(v, tenant);
                v.Item = id > 0 ? tenant.EntityContext.CreateQuery<Template>().Single(t => t.Id == id) : new Template();
            });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult EditTemplate(Tenant tenant, long websiteId, long id, FormCollection coll)
        {
            var website = tenant.EntityContext.CreateQuery<Website>().LoadRelated(w => w.Tenant)
                .Single(w => w.Id == websiteId && w.Tenant.Id == tenant.Id);

            var template = id > 0 ? tenant.EntityContext.CreateQuery<Template>().Single(t => t.Id == id)
                : tenant.EntityContext.Create(AddEntity<Template>(t =>
                {
                    t.Website = website;
                    t.Tenant = tenant;
                }));
            UpdateModel<Template>(template, new[] { "Name", "Html", "PlaceHolders" });
            tenant.EntityContext.SaveChanges();
            tenant.EntityContext.CreateQuery<Page>().LoadRelated(p => p.DisplayTemplate, p => p.Revisions, p => p.Category.Website)
                .Where(p => p.DisplayTemplate.Id == template.Id).ToList()
                .ForEach(p => p.SetHtml());
            tenant.EntityContext.SaveChanges();

            return Redirect("/Admin/" + website.Id);
        }

        [Authorize()]
        public ActionResult NewPage(Tenant tenant, long websiteId, long categoryId, long? templateId)
        {
            tenant.Load(tenant.EntityContext, t => t.Websites);
            return View<ViewModels.Websites.EditPage>("EditPage", v =>
            {
                v.PageTitle = "New Page";
                v.Website = tenant.EntityContext.CreateQuery<Website>().Single(w => w.Id == websiteId && w.Tenant.Id == tenant.Id);
                FillTenantViewModel(v, tenant);
                v.Templates = tenant.EntityContext.CreateQuery<Template>().Where(t => t.Website.Id == websiteId).ToList();
                var templateQuery = tenant.EntityContext.CreateQuery<Template>().Where(t => t.Website.Id == websiteId);
                v.Item = new Page
                {
                    Category = tenant.EntityContext.CreateQuery<Category>().Single(c => c.Id == categoryId),
                    DisplayTemplate = (templateId != null) ? templateQuery.Single(t => t.Id == templateId) : templateQuery.FirstOrDefault()
                };
            });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        [Authorize()]
        public ActionResult NewPage(Tenant tenant, long websiteId, long categoryId, FormCollection coll)
        {
            var website = tenant.EntityContext.CreateQuery<Website>().LoadRelated(w => w.Tenant)
                .Single(w => w.Id == websiteId && w.Tenant.Id == tenant.Id);

            var category = tenant.EntityContext.CreateQuery<Category>().LoadRelated(c => c.Website).Single(c => c.Id == categoryId);
            var templateId = long.Parse(coll["DisplayTemplate"]);
            var permissionSet = CreateDefaultPermissionSet(tenant);
            var page = tenant.EntityContext.Create(AddEntity<Page>(p =>
            {
                UpdateModel<Page>(p, new[] { "Title", "AllowComments", "ContentType", "Excerpt", "Syndicate", "Tags" });
                p.Author = tenant.EntityContext.CreateQuery<User>().Single(u => u.Tenant.Id == tenant.Id && u.Account.Username == HttpContext.User.Identity.Name);
                p.DateTime = DateTime.Now;
                p.Category = category;
                p.UniquePath = category.UniquePath + taskometer.Core.Utils.SlugHelper.Generate(p.Title) + "/";
                p.PermissionSet = permissionSet;
            }).SetRef(p => p.DisplayTemplate, t => t.Id == templateId)
            .SetRef(p => p.Tenant, t => t.Id == tenant.Id));

            tenant.EntityContext.Create(AddEntity<Revision>(r =>
            {
                r.Page = page;
                r.Contents = GetPageContents(coll);
                r.Tenant = tenant;
                r.DateTime = DateTime.Now;
            }));
            page.SetHtml();
            tenant.EntityContext.SaveChanges();

            return Redirect("/Admin/" + website.Id);
        }

        private string GetPageContents(FormCollection coll)
        {
            var pageContents = new PageContents();
            var prefix = "Contents-";
            foreach (var key in coll.AllKeys.Where(x => x.StartsWith(prefix)))
            {
                pageContents.Add(key.Substring(prefix.Length), coll[key]);
            }
            return pageContents.ToXml();
        }

        public ActionResult EditPage(Tenant tenant, long websiteId, long id)
        {
            tenant.Load(tenant.EntityContext, t => t.Websites);
            return View<ViewModels.Websites.EditPage>("EditPage", v =>
            {
                v.PageTitle = "Edit Page";
                v.Website = tenant.EntityContext.CreateQuery<Website>().Single(w => w.Id == websiteId && w.Tenant.Id == tenant.Id);
                FillTenantViewModel(v, tenant);
                v.Templates = tenant.EntityContext.CreateQuery<Template>().Where(t => t.Website.Id == websiteId).ToList();
                v.Item = tenant.EntityContext.CreateQuery<Page>().LoadRelated(p => p.DisplayTemplate, p => p.Category, p => p.Revisions).Single(p => p.Id == id);
                v.Contents = tenant.EntityContext.CreateQuery<Revision>().OrderByDescending(r => r.Id).First(r => r.Page.Id == id).Contents;
            });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        [Authorize()]
        public ActionResult EditPage(Tenant tenant, long websiteId, long id, FormCollection coll)
        {
            var website = tenant.EntityContext.CreateQuery<Website>().LoadRelated(w => w.Tenant)
                .Single(w => w.Id == websiteId && w.Tenant.Id == tenant.Id);

            var page = tenant.EntityContext.CreateQuery<Page>()
                .LoadRelated(p => p.Category.Website, p => p.DisplayTemplate, p => p.Revisions).Single(p => p.Id == id);
            UpdateModel<Page>(page, new[] { "Title", "AllowComments", "ContentType", "Excerpt", "Syndicate", "Tags" });
            page.Author = tenant.EntityContext.CreateQuery<User>().Single(u => u.Tenant.Id == tenant.Id && u.Account.Username == HttpContext.User.Identity.Name);
            page.DateTime = DateTime.Now;
            page.UniquePath = page.Category.UniquePath + taskometer.Core.Utils.SlugHelper.Generate(page.Title) + "/";

            tenant.EntityContext.Create(AddEntity<Revision>(r =>
            {
                r.Page = page;
                r.Contents = GetPageContents(coll);
                r.Tenant = tenant;
                r.DateTime = DateTime.Now;
            }));
            page.SetHtml();
            tenant.EntityContext.SaveChanges();

            return Redirect("/Admin/" + website.Id);
        }

        PermissionSet CreateDefaultPermissionSet(Tenant tenant)
        {
            var permSet = PermissionSet.Create(tenant);
            tenant.EntityContext.AddObject(permSet);
            tenant.EntityContext.AddObject(Permission.Create(tenant.EntityContext.CreateQuery<Role>().Single(r => r.Name == ROLES.USER && r.Tenant.Id == tenant.Id).Id, "ROLE", false, true, permSet, tenant));
            tenant.EntityContext.SaveChanges();

            return permSet;
        }

        private void FillTenantViewModel(taskometer.Web.ViewModels.Tenants.TenantViewModel viewModel, Tenant tenant)
        {
            viewModel.Tenant = tenant;
            viewModel.CMS = tenant.Websites.Where(w => w.Type == WEBSITE_TYPE.CMS).ToList();
            viewModel.Wikis = tenant.Websites.Where(w => w.Type == WEBSITE_TYPE.WIKI).ToList();
            viewModel.Blogs = tenant.Websites.Where(w => w.Type == WEBSITE_TYPE.BLOG).ToList();
        }
    }
}
