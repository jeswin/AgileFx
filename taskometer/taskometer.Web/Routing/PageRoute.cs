using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;

using AgileFx.ORM;
using taskometer.Core;
using taskometer.Core.Models;
using System.Text.RegularExpressions;

namespace taskometer.Web.Routing
{
    public class PageRoute : Route
    {
        const string ARCHIVE_REGEXP = "\\/(?<year>\\d{4})\\/(?<month>\\d{1,2})";
        const string TAG_REGEXP = "\\/Category\\/(?<tag>\\w*)";
        const string FEED_REGEXP = "\\/Feed[\\/]?";
        public PageRoute(string url)
            : base(url, new MvcRouteHandler())
        {
        }

        public PageRoute(string url, object defaults)
            : base(url, new RouteValueDictionary(defaults), new MvcRouteHandler())
        {
        }

        public PageRoute(string url, object defaults, object constraints)
            : base(url, new RouteValueDictionary(defaults), new RouteValueDictionary(constraints), new MvcRouteHandler())
        {
        }

        public PageRoute(string url, object defaults, object constraints, object dataTokens)
            : base(url, new RouteValueDictionary(defaults), new RouteValueDictionary(constraints), new RouteValueDictionary(dataTokens), new MvcRouteHandler())
        {
        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            //Hack for testing in the visual studio, but this wil work in live too..
            string host = httpContext.Request.Headers["Host"].Split(':')[0];
            if (host.IndexOf("www.", StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                host = host.Substring(4);
            }
            var requestedDomainAndPath = host + httpContext.Request.Path.TrimEnd('/') + '/';

            var context = new Entities();
            //var websites = context.CreateQuery<Website>().LoadRelated(w => w.Tenant).ToList();
            var websites = context.Cache().Invoke((_context) => _context.CreateQuery<Website>()
                .LoadRelated(w => w.Tenant, w => w.Templates).ToList(), this);

            var website = websites.Where(w => w.HandlesUrl(requestedDomainAndPath))
                            .OrderByDescending(w => w.DomainAndPath.Length)
                            .FirstOrDefault();

            if (website != null)
            {
                var pages = context.Cache().Invoke((ws, _context) => _context.CreateQuery<Page>()
                    .Where(p => p.Category.Website.Id == ws.Id).LoadRelated(p => p.Category.Website, p => p.Author.Account,
                    p => p.Comments, p => p.DisplayTemplate.Controls, p => p.PermissionSet.Permissions, p => p.Tenant)
                    .LoadRelatedInCollection(p => p.Comments, c => c.AddedBy).ToList(), this, website);

                var url = "/" + website.RemoveBasePath(requestedDomainAndPath);
                var page = pages.Where(p => p.UniquePath == url).FirstOrDefault()
                    ?? pages.Where(p => p.Category.UniquePath == url 
                        && string.Equals(p.Title, p.Category.DefaultPage, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

                var routeData = new RouteData(this, this.RouteHandler);
                this.Defaults.ToList().ForEach(kvp => routeData.Values.Add(kvp.Key, kvp.Value));
                routeData.Values["website"] = website;
                routeData.Values["tenant"] = website.Tenant;

                if (page != null)
                {
                    routeData.Values["page"] = page;
                    return routeData;
                }
                else if (website.Type == WEBSITE_TYPE.BLOG)
                {
                    var categories = context.Cache().Invoke((ws, _context) => _context.CreateQuery<Category>()
                        .Where(c => c.Website.Id == ws.Id).LoadRelated(c => c.Website).ToList(), this, website);
                    var category = categories.Where(c => c.UniquePath == url).FirstOrDefault();
                    if (category != null)
                    {
                        routeData.Values["action"] = "DisplayByCategory";
                        routeData.Values["category"] = category;
                        routeData.Values["page"] = httpContext.Request["page"];
                        return routeData;
                    }
                    else if (Regex.Match(url, TAG_REGEXP, RegexOptions.IgnoreCase).Success)
                    {
                        var match = Regex.Match(url, TAG_REGEXP, RegexOptions.IgnoreCase);
                        routeData.Values["action"] = "DisplayByTag";
                        routeData.Values["tag"] = match.Groups["tag"].Value;
                        routeData.Values["page"] = httpContext.Request["page"];
                        return routeData;
                    }
                    else if (Regex.Match(url, ARCHIVE_REGEXP).Success)
                    {
                        var match2 = Regex.Match(url, ARCHIVE_REGEXP);
                        routeData.Values["action"] = "DisplayByMonth";
                        routeData.Values["year"] = match2.Groups["year"].Value;
                        routeData.Values["month"] = match2.Groups["month"].Value;
                        routeData.Values["page"] = httpContext.Request["page"];
                        return routeData;
                    }
                    else if (Regex.Match(url, FEED_REGEXP, RegexOptions.IgnoreCase).Success)
                    {
                        routeData.Values["controller"] = "Feed";
                        routeData.Values["action"] = "Index";
                        return routeData;
                    }
                }
            }

            return null;
        }
    }
}
