using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

using AgileFx.ORM;
using taskometer.Core.Models;
using taskometer.Core.Utils;
using taskometer.Web.ViewModels.Feed;

namespace taskometer.Web.Controllers
{
    public class FeedController : Controller
    {
        public ActionResult Index(Website website)
        {
            var pages = website.EntityContext.CreateQuery<Page>().Where(p => p.Category.Website.Id == website.Id)
                .LoadRelated(p => p.Category.Website, p => p.Author, p => p.Comments).OrderByDescending(p => p.DateTime).Take(10).ToList();
            var xml = HtmlUtil.RenderPartialToString(this, "Index", new RSS { Website = website, Pages = pages });
            return new ContentResult
            {
                Content = xml,
                ContentEncoding = System.Text.Encoding.Default,
                ContentType = "application/xhtml+xml"
            };
        }

    }
}
