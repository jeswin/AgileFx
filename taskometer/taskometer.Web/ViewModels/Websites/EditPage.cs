using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AgileFx;
using taskometer.Core;
using taskometer.Core.Models;

namespace taskometer.Web.ViewModels.Websites
{
    public class EditPage : WebsiteViewModel
    {
        public Page Item { get; set; }
        public string Contents { get; set; }

        public List<Template> Templates { get; set; }
        public List<SelectListItem> TemplateList
        {
            get
            {
                return Templates.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = (x.Id == ((Item.DisplayTemplate != null) ? Item.DisplayTemplate.Id : 0)) }).ToList();
            }
        }

        string[] ContentTypes = new[] { PAGE_CONTENT_TYPE.HTML, PAGE_CONTENT_TYPE.WIKI };
        public List<SelectListItem> ContentTypeList
        {
            get
            {
                return ContentTypes.Select(x => new SelectListItem { Text = x, Value = x, Selected = (Item.ContentType == x) }).ToList();
            }
        }

        public TemplatePlaceHolderCollection PlaceHolders
        {
            get
            {
                return Item.DisplayTemplate.Placeholders.GetObjectFromXml<TemplatePlaceHolderCollection>();
            }
        }

        public PageContents ContentBlocks
        {
            get
            {
                return Contents.GetObjectFromXml<PageContents>();
            }
        }
    }
}
