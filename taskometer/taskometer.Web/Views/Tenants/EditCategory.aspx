<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Tenants.Master" Inherits="System.Web.Mvc.ViewPage<taskometer.Web.ViewModels.Websites.EditCategory>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    New Category
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <h1>
        <%= Model.Website.Title %></h1>
    <h3>
        New Category</h3>
    
    <% using (Html.BeginValidatedForm()) {%>

        <fieldset>
            <legend>Fields</legend>
            <p>
                <label for="ParentId">Parent Category:</label>
                <%= (Model.Item.Parent != null) ? Html.Encode(Model.Item.Parent.Name) : "" %>
            </p>
            <p>
                <label for="Name">Name:</label>
                <%= Html.TextBox("Name", Model.Item.Name, new Validation("Name").Required()) %>
            </p>
            <p>
                <label for="DefaultPage">Default Page:</label>
                <%= Html.TextBox("DefaultPage", Model.Item.DefaultPage, new Validation("DefaultPage").Required())%>
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
