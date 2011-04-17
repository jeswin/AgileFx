<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Tenants-Simple.Master"
    Inherits="System.Web.Mvc.ViewPage<taskometer.Web.ViewModels.Tenants.Login>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Login
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <div class="loginPage">
        <p>
            <img src="<%= Model.Tenant.Logo %>" alt="<%= Model.Tenant.Name %>" />
        </p>
        <h2>
            <%= Model.Tenant.Name %></h2>
        <% using(Html.BeginForm())
           { %>
        <p>
            <label for="username">
                Username</label>
            <br />
            <%= Html.TextBox("Username", Model.Username) %>
        </p>
        <p>
            <label for="password">
                Password</label>
            <br />
            <%= Html.Password("Password") %>
        </p>
        <p>
            <%= Html.CheckBox("RememberMe") %><label for="password" class="cb">Remember me</label>
        </p>
        <p>
            <input type="submit" value="Sign in" />
            <span class="small forgot"><a href="/Users/ForgotPassword">Forgot</a> username or password?</span>
        </p>
        <% } %>
    </div>
</asp:Content>
