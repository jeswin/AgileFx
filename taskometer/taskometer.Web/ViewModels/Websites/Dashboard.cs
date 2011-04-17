using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using taskometer.Core.Models;

namespace taskometer.Web.ViewModels.Websites
{
    public class Dashboard : WebsiteViewModel
    {
        public List<Category> Categories { get; set; }
        public List<Page> Pages { get; set; }
        public List<Template> Templates { get; set; }
    }
}
