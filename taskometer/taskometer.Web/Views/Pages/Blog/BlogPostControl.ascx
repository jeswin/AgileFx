<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<taskometer.Core.Models.Page>" %>

<div id="content">
    <% Html.RenderPartial("~/Views/Pages/Blog/BlogPostView.ascx", Model); %>
    <% if (Model.AllowComments)
       { %>
    <hr />
    <% Html.RenderPartial("~/Views/Pages/Blog/CommentsControl.ascx", Model); %>
    <% } %>
</div>
