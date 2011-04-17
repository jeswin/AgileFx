using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using taskometer.Core.Utils;

namespace taskometer.Core.Models
{
    public partial class Category
    {
        public static Category Create(string name, string defaultPage, Category parent, Website website, PermissionSet permSet)
        {
            var slug = SlugHelper.Generate(name);
            return new Category()
            {
                Name = name,
                UrlAlias = slug,
                Parent = parent,
                DefaultPage = defaultPage,
                Website = website,
                Tenant = website.Tenant,
                IsRoot = (null == parent),
                UniquePath = (parent != null) ? string.Format("{0}{1}/", parent.UniquePath, slug) : "/",
                PermissionSet = permSet
            };
        }

        public static Category CreateRoot(string name, Website website, PermissionSet permSet)
        {
            return Create(name, "index", null, website, permSet);
        }
    }
}
