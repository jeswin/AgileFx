using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Net;
using System.Web.UI;

namespace taskometer.Core.Utils
{
    public static class HtmlUtil
    {
        ///// <summary>Renders a view to string.</summary>
        //public static string RenderPartialToString(string controlName, object viewData)
        //{
        //    ViewDataDictionary vd = new ViewDataDictionary(viewData);
        //    ViewPage vp = new ViewPage { ViewData = vd, ViewContext = new ViewContext() };
        //    Control control = vp.LoadControl(controlName);

        //    vp.Controls.Add(control);

        //    StringBuilder sb = new StringBuilder();
        //    using (StringWriter sw = new StringWriter(sb))
        //    {
        //        using (HtmlTextWriter tw = new HtmlTextWriter(sw))
        //        {
        //            vp.RenderControl(tw);
        //        }
        //    }

        //    return sb.ToString();
        //}

        /// <summary>Renders a view to string.</summary>
        public static string RenderPartialToString(this Controller controller,
                                                string viewName, object viewData)
        {
            //Create memory writer
            var sb = new StringBuilder();
            var memWriter = new StringWriter(sb);

            //Create fake http context to render the view
            var fakeResponse = new HttpResponse(memWriter);
            var fakeContext = new HttpContext(HttpContext.Current.Request, fakeResponse);
            var fakeControllerContext = new ControllerContext(
                new HttpContextWrapper(fakeContext),
                controller.ControllerContext.RouteData,
                controller.ControllerContext.Controller);

            var oldContext = HttpContext.Current;
            HttpContext.Current = fakeContext;

            //Use HtmlHelper to render partial view to fake context
            var html = new HtmlHelper(
                new ViewContext(fakeControllerContext, new FakeView(), new ViewDataDictionary(),
                    new TempDataDictionary())
                , new ViewPage());
            html.RenderPartial(viewName, viewData);

            //Restore context
            HttpContext.Current = oldContext;

            //Flush memory and return output
            memWriter.Flush();
            return sb.ToString();
        }

        /// <summary>Fake IView implementation used to instantiate an HtmlHelper.</summary>
        class FakeView : IView
        {
            #region IView Members

            public void Render(ViewContext viewContext, System.IO.TextWriter writer)
            {
                throw new NotImplementedException();
            }

            #endregion
        }
    }
}
