<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<taskometer.Web.ViewModels.Pages.Blog.Archives>" %>
<div class="block">
    <h3>
        Archives</h3>
    <% if (Model.PagesByMonth.Count > 0)
       { %>
    <ul>
        <% foreach (var kvp in Model.PagesByMonth)
           {
               var arr = kvp.Key.Split('/').Select(x => int.Parse(x)).ToArray();
               var month = new DateTime(arr[0], arr[1], 1);
        %><li><a href="<%= Model.Website.GetUrl("/" + kvp.Key) %>">
            <%= string.Format("{0:MMMM yyyy} ({1})", month, kvp.Count())%></a></li><% } %>
    </ul>
    <% }
       else
       { %>No posts yet.<% } %>
</div>
