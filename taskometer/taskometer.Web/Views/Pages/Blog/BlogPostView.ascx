<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<taskometer.Core.Models.Page>" %>
<script type="text/C#" runat="server">
    string GetTagLinks(taskometer.Core.Models.Page page)
    {
        var website = page.Category.Website;
        var tags = page.Tags.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        return string.Join(", ", tags.Select(t => string.Format("<a href=\"{0}\">{1}</a>", website.GetUrl("/Category/" + t), t)).ToArray());
    }
</script>
<div class="post">
    <h1>
        <%= Model.Title%></h1>
    <small><b>Posted: </b>
        <%= Model.DateTime.ToString("MMMM dd, yyyy") %>
        | <b>Author: </b>
        <%= Model.Author.Account.Username %>
        | <b>Filed under: </b>
        <%= GetTagLinks(Model) %>
        | <a title="Comment on <%= Model.Title %>" href="<%= Model.Category.Website.GetUrl(Model.UniquePath) %>#comments">
            <%= Model.Comments.Where(c => c.IsApproved).Count() > 0 ? Model.Comments.Where(c => c.IsApproved).Count().ToString() : "No"%>
            Comments »</a></small>
    <%= Model.MainContentHtml %>
</div>