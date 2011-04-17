using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;
using taskometer.Core.Models;

using AgileFx.ORM;

namespace taskometer.Web.Routing
{
    public class WebsiteRoute : Route
    {
        public WebsiteRoute(string url)
            : base(url, new MvcRouteHandler())
        {
        }

        public WebsiteRoute(string url, object defaults)
            : base(url, new RouteValueDictionary(defaults), new MvcRouteHandler())
        {
        }

        public WebsiteRoute(string url, object defaults, object constraints)
            : base(url, new RouteValueDictionary(defaults), new RouteValueDictionary(constraints), new MvcRouteHandler())
        {
        }

        public WebsiteRoute(string url, object defaults, object constraints, object dataTokens)
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
            var requestedDomainAndPath = host + httpContext.Request.RawUrl;

            var context = new Entities();
            //var websites = context.CreateQuery<Website>().LoadRelated(w => w.Tenant).ToList();
            var websites = context.Cache().Invoke((_context) => _context.CreateQuery<Website>()
                .LoadRelated(w => w.Tenant).ToList(), this);

            var website = websites.Where(w => w.HandlesUrl(requestedDomainAndPath))
                            .OrderByDescending(w => w.DomainAndPath.Length)
                            .FirstOrDefault();

            if (website != null)
            {
                var routeData = new RouteData(this, this.RouteHandler);

                routeData.Values["website"] = website;
                routeData.Values["tenant"] = website.Tenant;

                website.SetRouteData(requestedDomainAndPath, routeData);

                return routeData;
            }

            return null;
        }
    }
}
