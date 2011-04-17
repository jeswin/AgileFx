<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Tenants.Master" Inherits="System.Web.Mvc.ViewPage<taskometer.Web.ViewModels.Websites.Dashboard>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Dashboard
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenuContent" runat="server">
    <% Html.RenderPartial("~/Views/Tenants/MenuControl.ascx", Model); %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <h1>
        <%= Model.Website.Title %></h1>
    <h3>
        Categories</h3>
    <table>
        <colgroup>
            <col width="160px" />
            <col width="160px" />
            <col width="120px" />
            <col width="120px" />
        </colgroup>
        <tr>
            <th>
                Category
            </th>
            <th>
                Path
            </th>
            <th>
                &nbsp;
            </th>
            <th>
                &nbsp;
            </th>
        </tr>
        <% foreach (var category in Model.Categories)
           { %><tr>
               <td>
                   <a href="<%= Html.Encode("/Admin/" + Model.Website.Id + "/EditCategory/" + category.Id + "/") %>"><%= Html.Encode(category.Name) %></a>
               </td>
               <td>
                   <%= Html.Encode(category.UniquePath) %>
               </td>
               <td>
                   <a href="<%= Html.Encode("/Admin/" + Model.Website.Id + "/NewCategory/" + category.Id + "/") %>">Add
                       Sub Category</a>
               </td>
               <td>
                   <a href="<%= "/Admin/" + Model.Website.Id + "/NewPage?action=new&categoryId=" + category.Id %>">Add Page</a>
               </td>
           </tr>
        <% } %>
    </table>
    <h3>
        Pages</h3>
    <table>
        <colgroup>
            <col width="160px" />
            <col width="160px" />
            <col width="120px" />
            <col width="120px" />
        </colgroup>
        <tr>
            <th>
                Page
            </th>
            <th>
                Path
            </th>
            <th>
                &nbsp;
            </th>
            <th>
                &nbsp;
            </th>
        </tr>
        <% foreach (var page in Model.Pages)
           { %><tr>
               <td>
                   <%= Html.Encode(page.Title)%>
               </td>
               <td>
                   <%= Html.Encode(page.UniquePath)%>
               </td>
               <td>
                   <a target="_blank" href="<%= Model.Website.GetAbsoluteUrl(page.UniquePath) %>">View Page</a>
               </td>
               <td>
                   <a href="<%= "/Admin/" + Model.Website.Id + "/EditPage/" + page.Id %>">Edit Page</a>
               </td>
           </tr>
        <% } %>
    </table>
    <h3>
        Templates</h3>
    <table>
        <colgroup>
            <col width="120px" />
        </colgroup>
        <tr>
            <th>
                Template
            </th>
        </tr>
        <% foreach (var template in Model.Templates)
           { %><tr>
               <td>
                   <a href="<%= "/Admin/" + Model.Website.Id + "/EditTemplate/" + template.Id %>"><%= Html.Encode(template.Name)%></a> 
               </td>
           </tr>
        <% } %>
    </table>
    <p><a href="<%= "/Admin/" + Model.Website.Id + "/EditTemplate?action=new" %>">Add new template</a></p>
    <%--<div class="dashboardPage">
        Hello world
    </div>--%>
</asp:Content>
