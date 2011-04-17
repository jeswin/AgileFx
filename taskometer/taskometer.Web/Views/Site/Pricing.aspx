<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Pricing and Signup
</asp:Content>
<asp:Content ContentPlaceHolderID="MenuContent" runat="server">
    <ul id="mainmenu" class="topnav">
        <li><a href="/">Home</a></li>
        <%--<li><a href="/features">Features</a></li>--%>
        <li><a href="/pricing" class="selected">Pricing &amp; Signup</a></li>
        <li><a href="/about">About</a></li>
    </ul>
</asp:Content>
<asp:Content ID="indexContent" ContentPlaceHolderID="PageContent" runat="server">
    <div class="pricingPage">
        <h1>
            All plans have a 30-day risk free trial.</h1>
        <p>
            <span style="color: Red">Note:</span> Taskometer is currently in private beta. You
            will need an invite code to sign up.
        </p>
        <div class="plans">
            <div class="plan premium">
                <h2>
                    Premium</h2>
                <h3>
                    $100/month</h3>
                <table>
                    <tr>
                        <td>
                            <span class="highlight">100 Projects</span></td>
                    </tr>
                    <tr>
                        <td>
                            CMS, Blogs, Wikis</td>
                    </tr>
                    <tr>
                        <td>
                            Project Management</td>
                    </tr>
                    <tr>
                        <td>
                            Timesheets</td>
                    </tr>
                    <tr>
                        <td>
                            Issue Tracking</td>
                    </tr>
                    <tr>
                        <td>
                            <span class="highlight">1000 Users</span></td>
                    </tr>
                    <tr>
                        <td>
                            <span class="highlight">25 GB Space</span></td>
                    </tr>
                </table>
                <p class="signup">
                    <a href="/signup?plan=premium">Sign Up!</a>
                </p>
            </div>
            <div class="plan pro">
                <h2>
                    Pro</h2>
                <h3>
                    $50/month</h3>
                <table>
                    <tr>
                        <td>
                            <span class="highlight">30 Projects</span></td>
                    </tr>
                    <tr>
                        <td>
                            CMS, Blogs, Wikis</td>
                    </tr>
                    <tr>
                        <td>
                            Project Management</td>
                    </tr>
                    <tr>
                        <td>
                            Timesheets</td>
                    </tr>
                    <tr>
                        <td>
                            Issue Tracking</td>
                    </tr>
                    <tr>
                        <td>
                            <span class="highlight">300 Users</span></td>
                    </tr>
                    <tr>
                        <td>
                            <span class="highlight">10 GB Space</span></td>
                    </tr>
                </table>
                <p class="signup">
                    <a href="/signup?plan=pro">Sign Up!</a>
                </p>
            </div>
            <div class="plan standard">
                <h2>
                    Standard</h2>
                <h3>
                    $25/month</h3>
                <table>
                    <tr>
                        <td>
                            <span class="highlight">15 Projects</span></td>
                    </tr>
                    <tr>
                        <td>
                            CMS, Blogs, Wikis</td>
                    </tr>
                    <tr>
                        <td>
                            Project Management</td>
                    </tr>
                    <tr>
                        <td>
                            Timesheets</td>
                    </tr>
                    <tr>
                        <td>
                            Issue Tracking</td>
                    </tr>
                    <tr>
                        <td>
                            <span class="highlight">100 Users</span></td>
                    </tr>
                    <tr>
                        <td>
                            <span class="highlight">5 GB Space</span></td>
                    </tr>
                </table>
                <p class="signup">
                    <a href="/signup?plan=standard">Sign Up!</a>
                </p>
            </div>
            <div class="plan free last">
                <h2>
                    Free</h2>
                <h3>
                    Ad supported</h3>
                <table>
                    <tr>
                        <td>
                            <span class="highlight">3 Projects</span></td>
                    </tr>
                    <tr>
                        <td>
                            CMS, Blogs, Wikis</td>
                    </tr>
                    <tr>
                        <td>
                            Project Management</td>
                    </tr>
                    <tr>
                        <td>
                            Timesheets</td>
                    </tr>
                    <tr>
                        <td>
                            Issue Tracking</td>
                    </tr>
                    <tr>
                        <td>
                            <span class="highlight">10 Users</span></td>
                    </tr>
                    <tr>
                        <td>
                            <span class="highlight">50 MB Space</span></td>
                    </tr>
                </table>
                <p class="signup">
                    <a href="/signup?plan=free">Sign Up!</a>
                </p>
            </div>
        </div>
    </div>
</asp:Content>
