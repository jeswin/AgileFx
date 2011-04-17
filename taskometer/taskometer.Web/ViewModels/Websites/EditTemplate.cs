using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using taskometer.Core.Models;

namespace taskometer.Web.ViewModels.Websites
{
    public class EditTemplate : WebsiteViewModel
    {
        public Template Item { get; set; }
    }
}
