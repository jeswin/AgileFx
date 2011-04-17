<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Tenants.Master" Inherits="System.Web.Mvc.ViewPage<taskometer.Web.ViewModels.Websites.EditPage>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Model.PageTitle %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">

    <script type="text/javascript">
        var categoryId = <%= Html.Encode(Model.Item.Category.Id) %>;
    </script>

    <h1>
        <%= Model.Website.Title %></h1>
    <h3>
        <%= Model.PageTitle %></h3>
    <% using (Html.BeginForm())
       {%>
    <fieldset>
        <legend>Fields</legend>
        <p>
            <label for="Category">
                Category:</label>
            <%= Html.Encode(Model.Item.Category.Name) %>
        </p>
        <p>
            <label for="DisplayTemplate">
                DisplayTemplate:</label>
            <%= Html.DropDownList("DisplayTemplate", Model.TemplateList, Model.Item.Id > 0 ? new { disabled = "disabled" } 
                    : (object)new { onchange="javascript:document.location.href = './NewPage?action=new&categoryId=' + categoryId + '&templateId=' + this.value;" })%>
        </p>
        <p>
            <label for="Title">
                Title:</label>
            <%= Html.TextBox("Title", Model.Item.Title) %>
        </p>
        <p>
            <label for="AllowComments">
                AllowComments:</label>
            <%= Html.CheckBox("AllowComments", Model.Item.AllowComments)%>
        </p>
        <p>
            <label for="ContentType">
                ContentType:</label>
            <%= Html.DropDownList("ContentType", Model.ContentTypeList)%>
        </p>
        <p>
            <label for="Excerpt">
                Excerpt:</label>
            <%= Html.TextArea("Excerpt", Model.Item.Excerpt, new { Rows = 5, Cols = 80 })%>
        </p>
        <p>
            <label for="Syndicate">
                Syndicate:</label>
            <%= Html.CheckBox("Syndicate", Model.Item.Syndicate)%>
        </p>
        <p>
            <label for="Tags">
                Tags:</label>
            <%= Html.TextBox("Tags", Model.Item.Tags)%>
        </p>
        <h3>Contents</h3>
        <% foreach (var placeHolder in Model.PlaceHolders)
           {
               var contentValue = "";
               if (Model.Item.Id > 0)
               {
                   var block = Model.ContentBlocks.FirstOrDefault(b => b.Name == placeHolder.Name);
                   if (block != null) contentValue = block.Value;
               }
               switch (placeHolder.Type)
               {
                   case TEMPLATE_PLACEHOLDER_TYPE.SINGLE_VALUE: %>
        <p><label for="Contents">
                <%= placeHolder.Name %>:</label>
            <%= Html.TextBox("Contents-" + placeHolder.Name, contentValue)%></p>
                        <% break;
                   case TEMPLATE_PLACEHOLDER_TYPE.LIST:
                       var listValues = ((taskometer.Core.Models.ListPlaceHolder)placeHolder).ListValues
                           .Select(x => new SelectListItem { Text = x, Value = x, Selected = (x == contentValue) }).ToList(); %>
        <p><label for="Contents">
                <%= placeHolder.Name %>:</label>
            <%= Html.DropDownList("Contents-" + placeHolder.Name, listValues)%></p>
                        <% break;
                   case TEMPLATE_PLACEHOLDER_TYPE.MULTI_LINE: %>
            <label for="Contents">
                <%= placeHolder.Name %>:</label>
            <%= Html.TextArea("Contents-" + placeHolder.Name, contentValue, new { Rows = 20, Cols = 80 })%>
                        <% break;
               }
           } %>
        <p>
            <input type="submit" value="Save" />
        </p>
    </fieldset>
    <% } %>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MenuContent" runat="server">
    <% Html.RenderPartial("~/Views/Tenants/MenuControl.ascx", Model); %>
</asp:Content>
