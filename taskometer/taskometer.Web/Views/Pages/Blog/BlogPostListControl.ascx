<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<taskometer.Core.Models.Page>>" %>
<div id="content">
    <% for (int i = 0; i < Model.Count; i++)
       { %>
    <% Html.RenderPartial("~/Views/Pages/Blog/BlogPostView.ascx", Model[i]); %>
    <hr />
    <% } %>
</div>
