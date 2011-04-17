<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<taskometer.Web.ViewModels.Feed.RSS>" %>
<script type="text/C#" runat="server">
    const string DATE_FORMAT = "ddd, dd MMMM yyyy h:mm tt";    
    string GetExcerpt(taskometer.Core.Models.Page page)
    {
        var excerpt = string.IsNullOrEmpty(page.Excerpt) ? page.MainContentHtml : page.Excerpt;
        return (excerpt != null && excerpt.Length > 200) ? excerpt.Substring(0, 200) : excerpt;
    }
</script><?xml version="1.0" encoding="UTF-8" ?>
<rss version="2.0" xmlns:content="http://purl.org/rss/1.0/modules/content/" xmlns:wfw="http://wellformedweb.org/CommentAPI/"
    xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns:atom="http://www.w3.org/2005/Atom"
    xmlns:sy="http://purl.org/rss/1.0/modules/syndication/" xmlns:slash="http://purl.org/rss/1.0/modules/slash/">

<channel>
	<title><%= Model.Website.Name %></title>
	<atom:link href="<%= Model.Website.GetAbsoluteUrl("/feed/") %>" rel="self" type="application/rss+xml" />
	<link><%= Model.Website.GetAbsoluteUrl("/") %></link>
	<description><%= Model.Website.Title %></description>
	<lastBuildDate><%= DateTime.Now.ToString(DATE_FORMAT)%></lastBuildDate>

	<generator>http://agilehead.com/?v=1.0.0</generator>
	<language>en</language>
	<sy:updatePeriod>hourly</sy:updatePeriod>
	<sy:updateFrequency>1</sy:updateFrequency>
	<% foreach (var page in Model.Pages)
    { %>
	<item>
		<title><%= page.Title %></title>

		<link><%= Model.Website.GetAbsoluteUrl(page.UniquePath) %></link>
		<comments><%= Model.Website.GetAbsoluteUrl(page.UniquePath) %>#comments</comments>
		<pubDate><%= page.DateTime.ToString(DATE_FORMAT) %></pubDate>
		<dc:creator>jeswinpk</dc:creator>
				<category><![CDATA[general]]></category>

		<guid isPermaLink="false"><%= Model.Website.GetAbsoluteUrl(page.UniquePath) %></guid>

		<description><![CDATA[<%= GetExcerpt(page) %>[...]]]></description>
			<content:encoded><![CDATA[<%= page.MainContentHtml %>]]></content:encoded>
			<wfw:commentRss><%= Model.Website.GetAbsoluteUrl(page.UniquePath) %>/feed/</wfw:commentRss>
		<slash:comments><%= Html.Encode(page.Comments.Count) %></slash:comments>
		</item>
		<% } %>
	</channel>
</rss>
