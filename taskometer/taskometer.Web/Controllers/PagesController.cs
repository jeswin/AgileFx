using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

using AgileFx;
using AgileFx.ORM;
using AgileFx.MVC;
using AgileFx.MVC.ViewModels;

using taskometer.Core;
using taskometer.Core.Models;
using System.Text.RegularExpressions;
using taskometer.Core.Utils;

namespace taskometer.Web.Controllers
{
    public class PagesController : WebsitesControllerBase<DefaultViewModel>
    {
        public PagesController()
        {
        }

        public ActionResult New()
        {
            return View("New");
        }

        //[OutputCache(Duration = 30, VaryByParam = "Any")]
        public ActionResult Display(Page page, Website website)
        {
            var html = page.Html;
            html = RenderControls(html, page, website);
            return new ContentResult
            {
                Content = html,
                ContentEncoding = System.Text.Encoding.Default,
                ContentType = "text/html"
            };
        }

        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Display(Page page, Website website, FormCollection coll)
        {
            var authorEmail = coll["email"];
            if (!Regex.IsMatch(authorEmail, @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")) return new ContentResult { Content = "Error: please enter a valid email address." };
            var url = coll["url"];
            if (!url.ToLower().StartsWith("http"))  url = "http://" + url;
            var comment = new Comment
            {
                AddedByDisplayName = coll["author"],
                AddedByEmail = authorEmail,
                AddedByWebsite = url,
                Body = CleanUpComment(coll["comment"]),
                Page = page,
                Tenant = page.Tenant,
                IPAddress = Request.UserHostAddress,
                AddedOn = DateTime.Now
            };
            page.Comments.Add(comment);
            page.EntityContext.SaveChanges();

            Request.QueryString["newComment"] = comment.Id.ToString();
            return Display(page, website);
        }

        string[] tagsToRemove = new string[] { "html", "head", "body", "meta", "script", "p", "div", "br" };
        private string CleanUpComment(string comment)
        {
            var text = comment;
            foreach (var tag in tagsToRemove) text = Regex.Replace(text, "<" + tag + "[^>]*?>", " ");
            text = Regex.Replace(text, "<a ", "<a rel=\"nofollow\"");
            return "<p>" + string.Join("</p><p>", Regex.Split(text, "\\r?\\n")) + "</p>";
        }

        public ActionResult DisplayByCategory(Category category, Website website, int? page)
        {
            var pages = category.EntityContext.CreateQuery<Page>().LoadRelated(p => p.Category.Website)
                .Where(p => p.Category.Website.Id == website.Id && p.Category.UniquePath.StartsWith(category.UniquePath))
                .OrderByDescending(p => p.DateTime)
                .ToPartialResultSet(Settings.GetPageStart(page, Settings.ITEMS_PER_PAGE), Settings.ITEMS_PER_PAGE);
            return DisplayMultiplePosts(category.IsRoot ? website.Title : category.Name, pages, website);
        }

        public ActionResult DisplayByTag(string tag, Website website, int? page)
        {
            var tagSearchKey = string.Format("|{0}|", tag);
            var pages = website.EntityContext.CreateQuery<Page>().LoadRelated(p => p.Category.Website, p => p.Author.Account)
                .Where(p => p.Category.Website.Id == website.Id && p.Tags.Contains(tagSearchKey))
                .OrderByDescending(p => p.DateTime)
                .ToPartialResultSet(Settings.GetPageStart(page, Settings.ITEMS_PER_PAGE), Settings.ITEMS_PER_PAGE);
            return DisplayMultiplePosts(string.Format("Category - {0}", tag), pages, website);
        }

        public ActionResult DisplayByMonth(int year, int month, Website website, int? page)
        {
            var pages = website.EntityContext.CreateQuery<Page>().LoadRelated(p => p.Category.Website)
                .Where(p => p.Category.Website.Id == website.Id && p.DateTime.Year == year && p.DateTime.Month == month)
                .OrderByDescending(p => p.DateTime)
                .ToPartialResultSet(Settings.GetPageStart(page, Settings.ITEMS_PER_PAGE), Settings.ITEMS_PER_PAGE);
            return DisplayMultiplePosts(string.Format("{0:yyyy MMMM}", new DateTime(year, month, 1)), pages, website);
        }

        ActionResult DisplayMultiplePosts(string title, PartialResultset<Page> pages, Website website)
        {
            var html = RenderControls(title, pages, website);
            return new ContentResult
            {
                Content = html,
                ContentEncoding = System.Text.Encoding.Default,
                ContentType = "text/html"
            };
        }

        string RenderControls(string html, Page page, Website website)
        {
            var regExp = "<%=[\\s\\n]*Control\\[\"(?<ControlType>.*?)\"[\\s\\n]*\\][\\s\\n]*%>";
            var matches = Regex.Matches(html, regExp, RegexOptions.Multiline | RegexOptions.IgnoreCase);
            foreach (Match match in matches)
            {
                var controlType = match.Groups["ControlType"].Value;
                string control = page.DisplayTemplate.Controls.FirstOrDefault(c => c.Type == controlType).VirtualPath;
                var contentHtml = string.Empty;
                switch (controlType.ToLower())
                {
                    case "pagetitle":
                        contentHtml = HtmlUtil.RenderPartialToString(this, control, page.Title);
                        break;
                    case "blogpost":
                        contentHtml = HtmlUtil.RenderPartialToString(this, control, page);
                        break;
                    case "comments":
                        contentHtml = HtmlUtil.RenderPartialToString(this, control, page.Comments.Where(c => c.IsApproved));
                        break;
                    default:
                        contentHtml = RenderControls(controlType, control, website);
                        break;
                }
                html = Regex.Replace(html, regExp.Replace("(?<ControlType>.*?)", controlType), contentHtml);
            }
            return html;
        }

        string RenderControls(string pageTitle, PartialResultset<Page> pages, Website website)
        {
            var regExp = "<%=[\\s\\n]*Control\\[\"(?<ControlType>.*?)\"[\\s\\n]*\\][\\s\\n]*%>";
            var blogListTemplate = website.EntityContext.CreateQuery<Template>().LoadRelated(t => t.Controls)
                .Single(t => t.Website.Id == website.Id && t.Name == "multiplepost");
            var html = blogListTemplate.Html;
            var matches = Regex.Matches(html, regExp, RegexOptions.Multiline | RegexOptions.IgnoreCase);
            foreach (Match match in matches)
            {
                var controlType = match.Groups["ControlType"].Value;
                string control = blogListTemplate.Controls.FirstOrDefault(c => c.Type == controlType).VirtualPath;
                var contentHtml = string.Empty;
                switch (controlType.ToLower())
                {
                    case "pagetitle":
                        contentHtml = HtmlUtil.RenderPartialToString(this, control, pageTitle);
                        break;
                    case "blogpostlist":
                        contentHtml = HtmlUtil.RenderPartialToString(this, control, pages);
                        break;
                    default:
                        contentHtml = RenderControls(controlType, control, website);
                        break;
                }
                html = Regex.Replace(html, regExp.Replace("(?<ControlType>.*?)", controlType), contentHtml);
            }
            return html;
        }

        string RenderControls(string controlType, string control, Website website)
        {
            switch (controlType.ToLower())
            {
                case "recentposts":
                    var recentPosts = website.EntityContext.CreateQuery<Page>().Where(p => p.Category.Website.Id == website.Id)
                        .OrderByDescending(p => p.DateTime).Take(10).ToList();
                    return HtmlUtil.RenderPartialToString(this, control, recentPosts);
                case "tags":
                    return HtmlUtil.RenderPartialToString(this, control, website);
                case "archives":
                    var pagesByMonth = website.EntityContext.CreateQuery<Page>().Where(p => p.Category.Website.Id == website.Id)
                        .OrderByDescending(p => p.DateTime).ToList().GroupBy(p => p.DateTime.Year + "/" + p.DateTime.Month).Take(10).ToList();
                    return HtmlUtil.RenderPartialToString(this, control, new taskometer.Web.ViewModels.Pages.Blog.Archives { Website = website, PagesByMonth = pagesByMonth });
                case "meta":
                    var websiteMeta = website.EntityContext.CreateQuery<Meta>().Where(m => m.Website.Id == website.Id).ToList();
                    return HtmlUtil.RenderPartialToString(this, control, websiteMeta);
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
