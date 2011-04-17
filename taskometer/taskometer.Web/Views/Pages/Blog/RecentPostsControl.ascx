<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<taskometer.Core.Models.Page>>" %>
<div class="block">
    <h3>
        Recent Posts</h3>
    <ul>
        <% foreach (var page in Model)
           { %><li><a href="<%= page.Category.Website.GetUrl(page.UniquePath) %>"><%= page.Title %></a></li><% } %>
    </ul>
</div>
