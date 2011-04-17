using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using taskometer.Core.Models;

namespace taskometer.Web.ViewModels.Pages.Blog
{
    public class Archives
    {
        public Website Website { get; set; }
        public List<IGrouping<string, Page>> PagesByMonth { get; set; }
    }
}
