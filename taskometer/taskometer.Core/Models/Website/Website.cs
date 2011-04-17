using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Web;

using AgileFx.ORM;

namespace taskometer.Core.Models
{
    public partial class Website
    {
        const string ADMIN_URL_PREFIX = "admin/";

        public static Website Create(string type, string name, string domain, string path, string title, Tenant tenant)
        {
            if (!path.StartsWith("/"))
                path = "/" + path;

            if (!path.EndsWith("/"))
                path = path + "/";

            return new Website()
            {
                Name = name,
                Type = type,
                Domain = domain,
                Path = path,
                Title = title,
                Tenant = tenant
            };
        }

        public bool HandlesUrl(string url)
        {
            if (url.StartsWith("http://") || url.StartsWith("https://"))
                throw new Exception("The input parameter to this method should not contain scheme prefixes like http. It should be a string like www.example.com/path1");

            //When we get a request like www.agilefx.org/blog we need to see if there is a website that handles /blog.
            //  - The problem here is that we store base paths as /blog/, instead of just /blog.
            //      Hence, we should append a slash to the end and see if it matches.  
            //      ie, url becomes url + "/"
            return (url + "/").StartsWith(DomainAndPath);
        }

        public string RemoveBasePath(string url)
        {
            //Add a trailing slash for matching. Refer comment in HandlesUrl()
            url = url + "/";
            url = url.Substring(DomainAndPath.Length);
            if (url.EndsWith("/"))
                url = url.Substring(0, url.Length - 1);

            return url;
        }

        public string DomainAndPath
        {
            get { return Domain + Path; }
        }

        private string GetBaseUrl(string scheme, int port)
        {
            if (scheme == "http")
            {
                if (port == 80)
                    return scheme + "://" + Domain + Path;
            }
            else if (scheme == "https")
            {
                if (port == 443)
                    return scheme + "://" + Domain + Path;
            }
            return scheme + "://" + Domain + ":" + port.ToString() + Path;

        }

        public string GetAdminUrl()
        {
            return GetBaseUrl("http", SiteSettings.HTTP_PORT) + ADMIN_URL_PREFIX + Id;
        }

        public void SetRouteData(string url, RouteData routeData)
        {
            if (IsAdminPage(url))
            {
                url = RemoveBasePath(url);
                url = url.Substring(ADMIN_URL_PREFIX.Length); //remove 'ADMIN_URL_PREFIX'

                url = url.ToLower();

                //  http://www.example.com/blog/_admin/ should show the "New Page" screen.
                if (url == "")
                    url = "pages/new";

                if (!url.Contains("/"))
                    throw new Exception("Unrecognized Url.");

                var parts = url.Split('/');

                routeData.Values["controller"] = parts[0];
                routeData.Values["action"] = parts[1];
                routeData.Values["parameters"] = parts.Count() > 2 ? parts[2] : null;

            }
            else
            {
                routeData.Values["controller"] = "Pages";
                routeData.Values["action"] = "Show";
                routeData.Values["parameters"] = url;
            }
        }

        private bool IsAdminPage(string url)
        {
            return url.StartsWith(Domain + Path + ADMIN_URL_PREFIX);
        }

        public string GetUrl(string virtualPath)
        {
            return string.Format("{0}{1}", Path.TrimEnd('/'), virtualPath);
        }

        public string GetAbsoluteUrl(string virtualPath)
        {            
            return GetBaseUrl("http", SiteSettings.HTTP_PORT).TrimEnd('/') + virtualPath;
        }

        public List<string> GetTags()
        {
            return this.EntityContext.Cache().Invoke((id, _context) =>
            {
                var tags = new Dictionary<string, string>();
                var tagGroups = _context.CreateQuery<Page>()
                    .Where(p => p.Category.Website.Id == id).Select(p => p.Tags).ToList();
                tagGroups.ForEach(tg =>
                {
                    tg.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToList()
                        .ForEach(t =>
                        {
                            if (!tags.ContainsKey(t.ToLower())) tags.Add(t.ToLower(), t);
                        });
                });
                return tags.Values.OrderBy(x => x).ToList();
            }, null, this.Id);
        }
    }
}
