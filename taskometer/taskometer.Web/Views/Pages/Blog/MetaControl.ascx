<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<taskometer.Core.Models.Meta>>" %>
<div class="block">
    <h3>
        Meta</h3>
    <ul>
        <% foreach (var meta in Model)
           { %><li><a href="<%= meta.Url %>">
       <%= meta.Text%></a></li><% } %>
    </ul>
</div>
