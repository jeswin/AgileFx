<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Tenants.Master" Inherits="System.Web.Mvc.ViewPage<taskometer.Web.ViewModels.Websites.EditTemplate>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Model.PageTitle %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <h1>
        <%= Model.Website.Title %></h1>
    <h3>
        <%= Model.PageTitle %></h3>
    
    <% using (Html.BeginForm()) {%>

        <fieldset>
            <legend>Fields</legend>
            <p>
                <label for="Name">Name:</label>
                <%= Html.TextBox("Name", Model.Item.Name) %>
            </p>
            <p>
                <label for="Html">Html:</label>
                <%= Html.TextArea("Html", Model.Item.Html, new { Rows = 10, Cols = 80 })%>
            </p>
            <p>
                <label for="Placeholders">Placeholders:</label>
                <%= Html.TextArea("Placeholders", Model.Item.Placeholders, new { Rows = 10, Cols = 80 })%>
            </p>
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MenuContent" runat="server">
    <% Html.RenderPartial("~/Views/Tenants/MenuControl.ascx", Model); %>
</asp:Content>
