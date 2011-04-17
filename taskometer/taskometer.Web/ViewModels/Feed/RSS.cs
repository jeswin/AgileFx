using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using taskometer.Core.Models;

namespace taskometer.Web.ViewModels.Feed
{
    public class RSS
    {
        public Website Website { get; set; }
        public IEnumerable<Page> Pages { get; set; }
    }
}
