using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using AgileFx;
using AgileFx.ORM;
using taskometer.Core.Utils;

namespace taskometer.Core.Models
{
    public partial class Page
    {
        public static Page Create(string title, User author, string contentType, string contents, Template template, Category category, string[] tags, bool allowComments, PermissionSet permSet)
        {
            var page = new Page
            {
                Title = title,
                Author = author,
                DisplayTemplate = template,
                ContentType = contentType,
                Category = category,
                Tags = string.Format("|{0}|", string.Join("|", tags)),
                AllowComments = allowComments,
                DateTime = DateTime.Now,
                Tenant = category.Tenant,
                UniquePath = category.UniquePath + SlugHelper.Generate(title) + "/",
                PermissionSet = permSet
            };
            page.Revisions.Add(new Revision
            {
                Contents = contents,
                DateTime = DateTime.Now,
                Tenant = category.Tenant
            });
            page.SetHtml();
            return page;
        }

        public void SetHtml()
        {
            var latestRevision = this.Revisions.Last();
            var pageContent = latestRevision.Contents.GetObjectFromXml<PageContents>();
            var html = this.DisplayTemplate.Html;
            foreach (var block in pageContent)
                html = Regex.Replace(html, "<%=\\s*Content\\[\"" + block.Name + "\"\\]\\s*%>", RenderContent(block), RegexOptions.IgnoreCase);
            if (this.Category.Website.Type == WEBSITE_TYPE.BLOG)
            {
                this.MainContentHtml = RenderContent(pageContent.Find(b => b.Name == Settings.MAIN_CONTENT_BLOCK));
                latestRevision.MainContentHtml = this.MainContentHtml;
            }
            this.Html = html;
            latestRevision.Html = html;
        }

        private string RenderContent(ContentBlock block)
        {
            if (block.Name == Settings.MAIN_CONTENT_BLOCK)
            {
                switch (this.ContentType)
                {
                    case PAGE_CONTENT_TYPE.WIKI:
                        return WikiRenderer.Render(block.Value);
                    case PAGE_CONTENT_TYPE.HTML:
                    default:
                        return block.Value;
                }
            }
            else
            {
                return block.Value;
            }
        }

        public static void RenderAll()
        {
            var context = new Entities();
            var pages = context.Page.Where(p => p.Html == null)
                .LoadRelated(p => p.Revisions, p => p.DisplayTemplate, p => p.Category.Website).ToList();
            pages.ForEach(p => p.SetHtml());
            context.SaveChanges();
        }
    }
}
