using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using taskometer.Core.Models;

namespace taskometer.Web.ViewModels.Pages.Blog
{
    public class BlogPost
    {
        public Page Item { get; set; }
        public List<Comment> Comments { get; set; }
        public bool ShowComments { get; set; }
    }
}
