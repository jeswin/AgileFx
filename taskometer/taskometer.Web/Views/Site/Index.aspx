<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Welcome to Taskometer
</asp:Content>
<asp:Content ContentPlaceHolderID="MenuContent" runat="server">
    <ul id="mainmenu" class="topnav">
        <li><a href="/" class="selected">Home</a></li>
        <%--<li><a href="/features">Features</a></li>--%>
        <li><a href="/pricing">Pricing &amp; Signup</a></li>
        <li><a href="/about">About</a></li>
    </ul>
</asp:Content>
<asp:Content ID="indexContent" ContentPlaceHolderID="PageContent" runat="server">
    <div class="frontPage">
        <div class="content">
            <a href="/pricing">
                <img src="/images/frontPage/go.png" alt="go" />
            </a>
        </div>
    </div>
</asp:Content>
