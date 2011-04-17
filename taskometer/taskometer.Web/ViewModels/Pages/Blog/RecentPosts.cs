using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using taskometer.Core.Models;

namespace taskometer.Web.ViewModels.Websites.Controls
{
    public class RecentPosts
    {
        public List<Page> Items { get; set; }
        public int MaxItems { get; set; }
    }
}
