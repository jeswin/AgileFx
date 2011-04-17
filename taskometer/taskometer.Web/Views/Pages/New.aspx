<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WebsitesAdmin.Master"
    Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="HeaderContent" runat="server">

    <script type="text/javascript" src="/scripts/jquery-ui.js"></script>

    <link type="text/css" rel="Stylesheet" href="/styles/jquery-ui/jquery-ui.css" />
</asp:Content>
<asp:Content ContentPlaceHolderID="MenuContent" runat="server">
    <ul id="mainmenu" class="topnav">
        <li><a href="/" class="selected">New</a></li>
        <li><a href="/projects">Edit</a></li>
        <li><a href="/projects">Comments</a></li>
        <li><a href="/projects">Stats</a></li>
    </ul>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Compose
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <% Html.RenderPartial("~/Views/Pages/Edit.ascx"); %>
</asp:Content>
