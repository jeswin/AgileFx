using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using taskometer.Core.Models;

namespace taskometer.Web.ViewModels.Websites.Blog
{
    public class CategoryList
    {
        public List<Category> Items { get; set; }
        public int MaxItems { get; set; }
    }
}
