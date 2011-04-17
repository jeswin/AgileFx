<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="aboutTitle" ContentPlaceHolderID="TitleContent" runat="server">
    About Taskometer
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MenuContent" runat="server">
    <ul id="mainmenu" class="topnav">
        <li><a href="/">Home</a></li>
        <%--<li><a href="/features">Features</a></li>--%>
        <li><a href="/pricing">Pricing &amp; Signup</a></li>
        <li><a href="/about" class="selected">About</a></li>
    </ul>
</asp:Content>
<asp:Content ID="aboutContent" ContentPlaceHolderID="PageContent" runat="server">
    <div class="aboutPage" style="line-height:1.3em">
        <div class="leftPane">
            <h1>
                About</h1>
            <p>
                Taskometer is developed by <a href="http://www.agilehead.com">AgileHead Consulting</a>.
                It started as a tool to serve AgileHead's internal requirements; a CMS, Blog, Wiki,
                Issue Tracker and Project Management Tool. We wanted a single portal which had these
                features, instead of buying separate products.</p>
            <p>
                Soon we realized that this product has a market on its own; every product and services
                company needed these applications. In the absence of Taskometer, they will have
                to use five different applications for this purpose.</p>
            <p>
                That would have been ok had they been completely independent tools, but they aren't.
                For instance, Wikis, Blogs and CMS are all content management tools. It would be
                good for CMSs to support Wiki-syntax and Wikis to support generic CMS style pages.
                And they might share the same set of user accounts. If they were different applications
                users would have had to login multiple times.
            </p>
            <p>
                Similarly, Issue Tracking and Project Management share a lot of common features.
                Since they are both fundamentally about accomplishing tasks, it made sense to combine
                them so that users can track developmental tasks and bugs in one place.</p>
            <p>
                Hence Taskometer was born. A one-stop application for a product company. It manages
                their content, and it takes care of their project planning and issue tracking. The
                application is not available publicly yet; we plan to launch in the next 2 months.
            </p>
            <p>
                Taskometer will be fully open-sourced when we launch.
            </p>
        </div>
        <div class="rightPane">
            <h2>
                Office:</h2>
            AgileHead Consulting,<br />
            Bangalore, India<br />
            Phone: +91-80-41491080<br />
            <a href="mailto:services@agilehead.com">services@agilehead.com</a>
        </div>
    </div>
</asp:Content>
