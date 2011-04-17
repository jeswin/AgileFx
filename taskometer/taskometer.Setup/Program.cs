using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using Microsoft.SqlServer.Management.Common;

using AgileFx;
using AgileFx.ORM;
using taskometer.Core;
using taskometer.Core.Models;

namespace taskometer.Setup
{
    class Program
    {
        static Entities context = new Entities();

        static void Main(string[] args)
        {
            SetupDB();
            CreateControls();
            CreateTenants();
            CreateUsers();
            CreateTemplates();
            CreateCategories();
            CreatePages();
            //CreateMeta();
            //CreateComments();
        }

        static void SetupDB()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DbSetup"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            ServerConnection svrConnection = new ServerConnection(sqlConnection);
            ExecuteSqlFile(svrConnection, "../../../taskometer.Core/DbScripts/killdb.sql");
            ExecuteSqlFile(svrConnection, "../../../taskometer.Core/DbScripts/taskometerDb.sql");
            ExecuteSqlFile(svrConnection, "../../../taskometer.Core/DbScripts/prefill.sql");
            Console.WriteLine("Database setup complete.....");
        }

        static void ExecuteSqlFile(ServerConnection connection, string filePath)
        {
            connection.ExecuteNonQuery(System.IO.File.ReadAllText(filePath));
        }

        private static void CreateControls()
        {
            context.AddObject(new Control { Name = "Default PageTitle", Type = "PageTitle", VirtualPath = "~/Views/Pages/Blog/PageTitleControl.ascx" });
            context.AddObject(new Control { Name = "Default BlogPost", Type = "BlogPost", VirtualPath = "~/Views/Pages/Blog/BlogPostControl.ascx" });
            context.AddObject(new Control { Name = "Default BlogPost List", Type = "BlogPostList", VirtualPath = "~/Views/Pages/Blog/BlogPostListControl.ascx" });
            context.AddObject(new Control { Name = "Default Archives", Type = "Archives", VirtualPath = "~/Views/Pages/Blog/ArchivesControl.ascx" });
            context.AddObject(new Control { Name = "Default Tags", Type = "Tags", VirtualPath = "~/Views/Pages/Blog/TagsControl.ascx" });
            context.AddObject(new Control { Name = "Default Comments", Type = "Comments", VirtualPath = "~/Views/Pages/Blog/CommentsControl.ascx" });
            context.AddObject(new Control { Name = "Default RecentPosts", Type = "RecentPosts", VirtualPath = "~/Views/Pages/Blog/RecentPostsControl.ascx" });
            context.AddObject(new Control { Name = "Default Meta", Type = "Meta", VirtualPath = "~/Views/Pages/Blog/MetaControl.ascx" });
            context.SaveChanges();
        }

        const string DEFAULT_LOGO = "/images/default-logo.png";
        static void CreateTenants()
        {
            //Create Tenants.
            var agileFx_Tenant = Tenant.Create("AgileFx", "agilefx.org", "premium", "+5:30", DEFAULT_LOGO, context);
            var agilehead_Tenant = Tenant.Create("AgileHead", "agilehead.com", "premium", "+5:30", DEFAULT_LOGO, context);

            context.SaveChanges();

            CreateWebsite("AgileFx Site", WEBSITE_TYPE.CMS, "AgileFx Framework", "agilefx.org", "/", agileFx_Tenant);
            CreateWebsite("AgileFx Blog", WEBSITE_TYPE.BLOG, "AgileFx Framework Blog", "blog.agilefx.org", "/", agileFx_Tenant);
            CreateWebsite("AgileHead Site", WEBSITE_TYPE.CMS, "AgileHead Technology Consulting", "agilehead.com", "/", agilehead_Tenant);
            CreateWebsite("Technobabble: by AgileHead", WEBSITE_TYPE.BLOG, "AgileHead Blog", "blog.agilehead.com", "/", agilehead_Tenant);
        }

        static void CreateUsers()
        {
            foreach (var tenant in context.CreateQuery<Tenant>().ToList())
            {
                var admin = User.Create("admin", "Anup", "Kesavan", ROLES.ADMINISTRATOR, tenant);
                admin.Account.SetPassword("secret");

                var user = User.Create("hemchand", "Hemchand", "Thalanchery", ROLES.USER, tenant);
                user.Account.SetPassword("secret");

                var user2 = User.Create("jeswin", "Jeswin", "Kumar", ROLES.USER, tenant);
                user2.Account.SetPassword("secret");
                context.SaveChanges();
            }
        }

        private static void CreateWebsite(string siteName, string siteType, string siteTitle, string domain, string path, Tenant tenant)
        {
            context.AddObject(Website.Create(siteType, siteName, domain, path, siteTitle, tenant));
            //context.AddObject(Website.Create(WEBSITE_TYPE.WIKI, siteName + " Wiki", domain, "/wiki", siteTitle + " Wiki and Documentation", tenant));
            //context.AddObject(Website.Create(WEBSITE_TYPE.BLOG, siteName + " Blog", domain, "/blog", siteTitle + " Blogs", tenant));
            context.SaveChanges();
        }

        private static void CreateTemplates()
        {
            var controls = context.CreateQuery<Control>().ToList();
            context.CreateQuery<Website>().LoadRelated(w => w.Tenant).ToList()
                .ForEach(website =>
                {
                    var placeHolder1 = new MultiLinePlaceHolder("MainContent");
                    var placeHolder2 = new SingleValuePlaceHolder("Title");
                    var placeHolder3 = new ListPlaceHolder("Category", new[] { "Good", "Bad", "Ugly" });

                    if (website.Type != WEBSITE_TYPE.BLOG)
                    {
                        //var defaultHtml = "<html><body><%= Content[\"MainContent\"] %></body></html>";
                        //var defaultPlaceholders = new TemplatePlaceHolderCollection(new TemplatePlaceHolder[] { placeHolder1 });
                        //context.AddObject(Template.Create("default", defaultHtml, defaultPlaceholders.ToXml(), website));

                        //var html2 = "<html><head><title><%= Content[\"Title\"] %></title></head><body><%= Content[\"MainContent\"] %></body></html>";
                        //var placeholders2 = new TemplatePlaceHolderCollection(new TemplatePlaceHolder[] { placeHolder1, placeHolder2 });
                        //context.AddObject(Template.Create("type1", html2, placeholders2.ToXml(), website));

                        var html3 = "<html><head><title><%= Content[\"Title\"] %></title></head><body><h1><%= Content[\"Category\"] %></h1><%= Content[\"MainContent\"] %></body></html>";
                        var placeholders3 = new TemplatePlaceHolderCollection(new TemplatePlaceHolder[] { placeHolder1, placeHolder2, placeHolder3 });
                        context.AddObject(Template.Create("type2", html3, placeholders3.ToXml(), website));
                    }
                    else
                    {
                        var html4 = "<html><head><title>Testing &raquo; <%= Control[\"PageTitle\"] %></title></head><body><table><tr><td>" +
                            "<div><%= Control[\"BlogPost\"] %></div><div><%= Control[\"Comments\"] %></div>" +
                            "</td><td><div><%= Control[\"RecentPosts\"] %></div><div><%= Control[\"Tags\"] %>" +
                            "</div><div><%= Control[\"Meta\"] %></div><div><%= Control[\"Archives\"] %></div></td></tr></body></html>";
                        var placeholders4 = new TemplatePlaceHolderCollection(new TemplatePlaceHolder[] { placeHolder1, placeHolder2, placeHolder3 });
                        var blogTemplate1 = Template.Create("singlepost", html4, placeholders4.ToXml(), website);
                        controls.ForEach(c => blogTemplate1.Controls.Add(c));
                        context.AddObject(blogTemplate1);

                        var html5 = "<html><head><title>Testing &raquo; <%= Control[\"PageTitle\"] %></title></head><body><table><tr><td>" +
                            "<div><%= Control[\"BlogPostList\"] %></div></td><td><div><%= Control[\"RecentPosts\"] %></div><div><%= Control[\"Tags\"] %>" +
                            "</div><div><%= Control[\"Meta\"] %></div><div><%= Control[\"Archives\"] %></div></td></tr></body></html>";
                        var placeholders5 = new TemplatePlaceHolderCollection(new TemplatePlaceHolder[] { placeHolder1 });
                        var blogTemplate2 = Template.Create("multiplepost", html5, placeholders5.ToXml(), website);
                        controls.ForEach(c => blogTemplate2.Controls.Add(c));
                        context.AddObject(blogTemplate2);
                    }
                });
            context.SaveChanges();
        }

        static PermissionSet CreateDefaultPermissionSet(Tenant tenant)
        {
            var permSet = PermissionSet.Create(tenant);
            context.AddObject(permSet);
            context.AddObject(Permission.Create(context.CreateQuery<Role>().Single(r => r.Name == ROLES.USER && r.Tenant.Id == tenant.Id).Id, "ROLE", false, true, permSet, tenant));
            context.SaveChanges();

            return permSet;
        }

        static void CreateCategories()
        {
            foreach (var website in context.CreateQuery<Website>().LoadRelated(w => w.Tenant).ToList())
            {
                var root = Category.CreateRoot("Root", website, CreateDefaultPermissionSet(website.Tenant));
                root.UniquePath = "/";
                //context.AddObject(root);
                //context.AddObject(Category.Create("Sub1", root, website, CreateDefaultPermissionSet(website.Tenant)));
                //context.AddObject(Category.Create("Sub2", root, website, CreateDefaultPermissionSet(website.Tenant)));
                //context.SaveChanges();
            };
        }

        private static void CreatePages()
        {
            context.CreateQuery<Category>().LoadRelated(c => c.Website.Templates, c => c.Tenant.Users).ToList()
                .ForEach(category =>
                {
                    var author = category.Tenant.Users.First();
                    var templates = category.Website.Templates.ToArray();

                    var pageContent = new PageContents();
                    pageContent.Add("MainContent", "<strong>Sample MainContent</strong>");
                    //if (category.Website.Type != WEBSITE_TYPE.BLOG)
                    //{
                    //    context.AddObject(Page.Create("index", author, PAGE_CONTENT_TYPE.HTML, pageContent.ToList().ToXml(),
                    //        (category.Website.Type == WEBSITE_TYPE.BLOG) ? templates[3] : templates[0], category, new[] { "default", "technology" }, true, CreateDefaultPermissionSet(category.Tenant)));
                    //}

                    pageContent.Add("Title", "Sample Page Title");
                    //context.AddObject(Page.Create("page1", author, PAGE_CONTENT_TYPE.HTML, pageContent.ToList().ToXml(),
                    //    (category.Website.Type == WEBSITE_TYPE.BLOG) ? templates[3] : templates[1], category, new[] { "default", "technology" }, true, CreateDefaultPermissionSet(category.Tenant)));

                    pageContent.Add("Category", "Good");
                    context.AddObject(Page.Create("page2", author, PAGE_CONTENT_TYPE.HTML, pageContent.ToList().ToXml(),
                        (category.Website.Type == WEBSITE_TYPE.BLOG) ? templates[1] : templates[0], category, new[] { "default", "technology" }, true, CreateDefaultPermissionSet(category.Tenant)));
                    context.SaveChanges();
                });
        }

        static void CreateComments()
        {
            context.CreateQuery<Page>().LoadRelated(p => p.Tenant.Users).Where(p => p.Category.Website.Type == WEBSITE_TYPE.BLOG).ToList()
                .ForEach(page =>
                {
                    context.AddObject(Comment.Create(page, "Test comment 1.", "Commenter1", "commenter1@me.com", "www.me.com", "127.0.0.1", null, page.Tenant));
                    context.AddObject(Comment.Create(page, "Test comment 2.", "Commenter2", "commenter2@me.com", "www.me.com", "127.0.0.1", page.Tenant.Users.Last(), page.Tenant));
                    context.SaveChanges();
                });
        }

        static void CreateMeta()
        {
            context.CreateQuery<Website>().Where(w => w.Type == WEBSITE_TYPE.BLOG).LoadRelated(w => w.Tenant).ToList()
                .ForEach(w =>
                {
                    w.Meta.Add(new Meta { Tenant = w.Tenant, Website = w, Text = "AgileHead", Url = "http://www.agilehead.com" });
                    w.Meta.Add(new Meta { Tenant = w.Tenant, Website = w, Text = "RSS", Url = w.GetUrl("/feed") });
                });
            context.SaveChanges();
        }
    }
}
