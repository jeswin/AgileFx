<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<taskometer.Core.Models.Page>" %>

<script type="text/C#" runat="server">
    string GetCommenterDisplay(taskometer.Core.Models.Comment comment)
    {
        if (string.IsNullOrEmpty(comment.AddedByWebsite)) return comment.AddedByDisplayName;
        else
        {
            var url = comment.AddedByWebsite.StartsWith("http") ? comment.AddedByWebsite : "http://" + comment.AddedByWebsite;
            return string.Format("<a href=\"{0}\" rel=\"external nofollow\" class=\"url\">{1}</a>", url, comment.AddedByDisplayName);
        }
    }
</script>

<div id="comments">
    <% if (Model.Comments.Count > 0)
       { %>
    <h3>
        <%= Html.Encode(Model.Comments.Count)%>
        Comments on "<%= Model.Title%>"</h3>
    <ol class="commentlist">
        <% int ctr = 0;
           var newCommentId = string.IsNullOrEmpty(Request["newcomment"]) ? 0 : long.Parse(Request["newcomment"]);
           foreach (var comment in Model.Comments.Where(c => c.IsApproved || c.Id == newCommentId))
           { %>
        <li class="alt" id="comment-<%= Html.Encode(comment.Id) %>"><span>
            <%= Html.Encode(++ctr) %></span> <cite>
                <%= GetCommenterDisplay(comment) %>
                &nbsp;said at
                <%= comment.AddedOn.ToString("h:mm tt on MMMM dd, yyyy ") %>:</cite>
            <%= comment.Body%>
            <% if (comment.Id == newCommentId)
               { %><p>Your comment will be displayed after approval from author.</p><% } %>
            <% } %>
    </ol>
    <hr />
    <% } %>
    <div class="replyForm">
        <h4 class="center">
            Leave a Reply</h4>
        <% using (Html.BeginForm())
           { %>
        <ul class="formlist">
            <li>
                <input type="text" onclick="if(this.value == 'Name (required)') this.value='';" onblur="if(this.value.length == 0) this.value='Name (required)';"
                    aria-required="true" tabindex="1" size="22" value="Name (required)" id="author"
                    name="author"></li>
            <li>
                <input type="text" onclick="if(this.value == 'Mail (will not be published) (required)') this.value='';"
                    onblur="if(this.value.length == 0) this.value='Mail (will not be published) (required)';"
                    aria-required="true" tabindex="2" size="22" value="Mail (will not be published) (required)"
                    id="email" name="email"></li>
            <li>
                <input type="text" onclick="if(this.value == 'Website') this.value='';" onblur="if(this.value.length == 0) this.value='Website';"
                    tabindex="3" size="22" value="Website" id="url" name="url"></li>
            <li>
                <textarea value="Enter comment here." tabindex="4" rows="10" cols="70%" id="comment"
                    name="comment"></textarea></li>
            <li class="submitbutton">
                <input type="submit" value="Submit Comment" tabindex="5" id="submit" name="submit"></li>
            <input type="hidden" value="10" name="comment_post_ID">
        </ul>
        <% } %>
    </div>
</div>
