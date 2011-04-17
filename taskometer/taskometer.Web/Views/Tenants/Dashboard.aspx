<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Tenants.Master" Inherits="System.Web.Mvc.ViewPage<taskometer.Web.ViewModels.Tenants.Dashboard>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Dashboard
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenuContent" runat="server">
    <% Html.RenderPartial("~/Views/Tenants/MenuControl.ascx", Model); %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <%--<h3>
        CMS</h3>
    <% if (Model.CMS.Count > 0) { %>
    <table>
        <% foreach (var cms in Model.CMS)
           { %><tr>
               <td>
                   <a href="/Admin/<%= Html.Encode(cms.Id) %>/">
                       <%= cms.Name%></a>
               </td>
           </tr>
        <% } %>
    </table>
    <%  } else { %>There are no CMS projects.<% } %>
    <h3>
        Blogs</h3>
    <% if (Model.Blogs.Count > 0) { %>
    <table>
        <% foreach (var blog in Model.Blogs)
               { %><tr>
                   <td>
                       <a href="/Admin/<%= Html.Encode(blog.Id) %>/">
                           <%= blog.Name%></a>
                   </td>
               </tr>
        <% } %>
    </table>
    <%  } else { %>There are no Blog projects.<% } %>
    <h3>
        Wikis</h3>
    <% if (Model.Wikis.Count > 0) { %>
    ]
    <table>
        <% foreach (var wiki in Model.Wikis)
               { %><tr>
                   <td>
                       <a href="/Admin/<%= Html.Encode(wiki.Id) %>/">
                           <%= wiki.Name%></a>
                   </td>
               </tr>
        <% } %>
    </table>
    <%  } else { %>There are no Wiki projects.<% } %>--%>
    <div class="dashboardPage">
        Hello world
    </div>
</asp:Content>
