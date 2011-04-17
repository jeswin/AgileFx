<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<taskometer.Web.ViewModels.Tenants.TenantViewModel>" %>

    <script runat="server" type="text/C#">
        string trimMenuItem(string text)
        {
            var maxLength = 20;
            if (text.Length <= maxLength) return text;
            else return text.Substring(0, maxLength - 3) + "...";
        }
    </script>

    <ul id="mainmenu" class="topnav">
        <li><a href="/" class="selected">Dashboard</a></li>
        <li><a href="/projects">Projects</a>
            <ul class="subnav">
                <li><a href="1">Technobabble</a></li>
                <li><a href="2">AgileFx Team Blog</a></li>
                <li><a href="3">Personal</a></li>
            </ul>
        </li>
        <li><a href="/projects">Issues</a>
            <ul class="subnav">
                <li><a href="1">Technobabble</a></li>
                <li><a href="2">AgileFx Team Blog</a></li>
                <li><a href="3">Personal</a></li>
            </ul>
        </li>
        <li><a href="/websites">Sites</a>
            <ul class="subnav">
                <% foreach (var website in Model.CMS)
                   { %>
                <li><a href="/Admin/<%= Html.Encode(website.Id) %>/">
                    <%= trimMenuItem(website.Name)%></a></li>
                <% } %>
            </ul>
        </li>
        <li><a href="/websites">Wikis</a>
            <ul class="subnav">
                <% foreach (var website in Model.Wikis)
                   { %>
                <li><a href="/Admin/<%= Html.Encode(website.Id) %>/">
                    <%= trimMenuItem(website.Name)%></a></li>
                <% } %>
            </ul>
        </li>
        <li><a href="/websites">Blogs</a>
            <ul class="subnav">
                <% foreach (var website in Model.Blogs)
                   { %>
                <li><a href="/Admin/<%= Html.Encode(website.Id) %>/">
                    <%= trimMenuItem(website.Name)%></a></li>
                <% } %>
            </ul>
        </li>
        <li><a href="/users">Users</a></li>
    </ul>