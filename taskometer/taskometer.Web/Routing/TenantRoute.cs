using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;
using taskometer.Core.Models;


namespace taskometer.Web.Routing
{
    public class TenantRoute : Route
    {
        public TenantRoute(string url)
            : base(url, new MvcRouteHandler())
        {
        }

        public TenantRoute(string url, object defaults)
            : base(url, new RouteValueDictionary(defaults), new MvcRouteHandler())
        {
        }

        public TenantRoute(string url, object defaults, object constraints)
            : base(url, new RouteValueDictionary(defaults), new RouteValueDictionary(constraints), new MvcRouteHandler())
        {
        }

        public TenantRoute(string url, object defaults, object constraints, object dataTokens)
            : base(url, new RouteValueDictionary(defaults), new RouteValueDictionary(constraints), new RouteValueDictionary(dataTokens), new MvcRouteHandler())
        {
        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            var routeData = base.GetRouteData(httpContext);
            if (routeData != null)
            {
                string host = httpContext.Request.Headers["Host"].Split(':')[0];
                if (host.IndexOf("www.", StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    host = host.Substring(4);
                }
                var context = new Entities();
                var tenant = context.CreateQuery<Tenant>().SingleOrDefault(t => t.Domain == host);
                if (tenant != null)
                {
                    routeData.Values["tenant"] = tenant;
                    return routeData;
                }
                else
                    return null;
            }
            else
                return null;
        }
    }
}
