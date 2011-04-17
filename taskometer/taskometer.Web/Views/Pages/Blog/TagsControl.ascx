<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<taskometer.Core.Models.Website>" %>
<div class="block">
    <h3>
        Categories</h3>
    <ul>
        <% foreach (var tag in Model.GetTags())
           { %><li><a href="<%= Model.GetUrl("/Category/" + tag) %>"><%= tag %></a></li><% } %>
    </ul>
</div>
