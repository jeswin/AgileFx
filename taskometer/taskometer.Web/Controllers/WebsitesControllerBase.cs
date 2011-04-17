using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using AgileFx.MVC;
using AgileFx.MVC.ViewModels;
using AgileFx.MVC.Controllers;

namespace taskometer.Web.Controllers
{
    public class WebsitesControllerBase<T> : DefaultController<T>
        where T : DefaultViewModel, new()
    {
    }
}
