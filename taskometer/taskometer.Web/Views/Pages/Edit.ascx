<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<script type="text/javascript">
	$(document).ready(function() {
		$("#tabs").tabs();
	});
</script>

<h3>
    Title
</h3>
<p>
    <input type="text" size="80" />
</p>

<div id="tabs">
	<ul>
		<li><a href="#visual">Visual</a></li>
		<li><a href="#wiki">Wiki</a></li>
		<li><a href="#html">Raw Html</a></li>
	</ul>
	<div id="visual">
    <% Html.RenderPartial("~/Views/Pages/VisualEditorControl.ascx"); %>
	</div>
	<div id="wiki">
    <% Html.RenderPartial("~/Views/Pages/WikiEditorControl.ascx"); %>
	</div>
	<div id="html">
    <% Html.RenderPartial("~/Views/Pages/HtmlEditorControl.ascx"); %>
	</div>
</div>
