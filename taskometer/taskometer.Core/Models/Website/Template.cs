using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace taskometer.Core.Models
{
    public partial class Template
    {
        public static Template Create(string name, string html, string placeHolders, Website website)
        {
            return new Template
            {
                Name = name,
                Html = html,
                Placeholders = placeHolders,
                Website = website,
                Tenant = website.Tenant
            };
        }
    }
}
