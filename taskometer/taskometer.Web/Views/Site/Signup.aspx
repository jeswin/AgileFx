<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Signup>" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Taskometer Signup
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

    <script type="text/javascript">

        //? TODO: This section is a quick hack. Improve code quality.

        function setCustomDomainType(isCustomDomain) {
            $('#IsCustomDomain').val(isCustomDomain);
            displayAppUrls();
        }

        function getDomainName() {
            if ($('#IsCustomDomain').val() == 'false') {
                var subDomain = $('#subDomain').val();
                if (subDomain == '')
                    return '';
                return '<span style="color:#FB8">http://</span>' + subDomain + ".taskometer.com";
            }
            else {
                var customDomain = $('#customDomain').val();
                if (customDomain == '')
                    return '';
                return '<span style="color:#FB8">http://</span>' + customDomain;
            }
        }

        function getAppUrlByType(appType) {
            var urlType = $('#' + appType + "UrlType").val();
            var subDir = $('#' + appType + "SubDirectory").val();
            var customDomain = $('#' + appType + "CustomDomain").val();

            return getAppUrl(urlType, subDir, customDomain);
        }

        function getAppUrl(urlTypeVal, subDirVal, customDomain) {
            var domain = getDomainName();
            if (domain == '')
                return ''
            if (urlTypeVal == 'useDomain')
                return domain;
            if (urlTypeVal == 'subDirectory')
                return domain + "/" + subDirVal;
            if (urlTypeVal == 'customDomain')
                return '<span style="color:#FB8">http://</span>' + customDomain;
        }

        function displayAppUrls() {
            var apps = getAllApps();
            for (var i = 0; i < apps.length; i++) {
                displayAppUrl(apps[i]);
            }
        }

        function getAllApps() {
            return ['Website', 'ProjectManagement', 'IssueTracker', 'Wiki', 'Blog', 'Admin'];
        }

        function displayAppUrl(appType) {
            var url = getAppUrlByType(appType);
            if (url == '') {
                url = 'Choose a domain first.';
                $('#' + appType + "UrlEdit").hide();
            } else {
                $('#' + appType + "UrlEdit").show();
            }
            $('#' + appType + "EffectiveUrl").html(url);
        }

        function domainChanged() {
            displayAppUrls();
        }

        function getAppName(appType) {
            if (appType == 'Website')
                return 'Website';
            if (appType == 'ProjectManagement')
                return 'Project Management';
            if (appType == 'IssueTracker')
                return 'Issue Tracker';
            if (appType == 'Wiki')
                return 'Wiki';
            if (appType == 'Blog')
                return 'Blog';
            if (appType == 'Admin')
                return 'Admin';
        }

        function editAppUrl(appType) {
            var title = getAppName(appType);

            $('.domainPlaceholder').html(getDomainName());

            var urlType = $('#' + appType + "UrlType").val();
            var subDir = $('#' + appType + "SubDirectory").val();
            var customDomain = $('#' + appType + "CustomDomain").val();

            setEditUrlDialogVisibility(urlType, subDir, customDomain);

            $('#editAppUrlDialog').dialog({ height: 280, width: 450, modal: true, title: 'Edit ' + title + ' Url', resizable: false,
                buttons: {
                    "Cancel": function() { $(this).dialog("close"); },
                    "Ok": function() {
                        if ($('#appUrlTypeDomainRoot').attr('checked'))
                            urlType = 'useDomain';
                        if ($('#appUrlTypeSubDirectory').attr('checked'))
                            urlType = 'subDirectory';
                        if ($('#appUrlTypeCustomDomain').attr('checked'))
                            urlType = 'customDomain';

                        subDir = $('#appUrlTypeSubDirectoryTextBox').val();
                        customDomain = $('#appUrlTypeCustomDomainTextBox').val();

                        //Check if this domain-name already exists.
                        var allApps = getAllApps();
                        for (var i = 0; i < allApps.length; i++) {
                            var app = allApps[i];

                            if (app == appType)
                                continue;

                            if (getAppUrlByType(app) == getAppUrl(urlType, subDir, customDomain)) {
                                alert('This url is already in use by the ' + getAppName(app) + ' application.');
                                return;
                            }
                        }

                        setAppUrl(appType, urlType, subDir, customDomain);

                        $(this).dialog("close");
                    }
                }
            });
        }

        function setAppUrl(appType, urlType, subDir, customDomain) {
            if (/^http:\/\//.test(customDomain))
                customDomain = customDomain.replace(/^http:\/\//, '');
            $('#' + appType + "UrlType").val(urlType);
            $('#' + appType + "SubDirectory").val(subDir);
            $('#' + appType + "CustomDomain").val(customDomain);
            displayAppUrls();
        }

        function setEditUrlDialogVisibility(urlType, subDir, customDomain) {
            if (urlType == 'useDomain') {
                $('#appUrlTypeSubDirectoryExample').show();
                $('#appUrlTypeCustomDomainExample').show();
                $('#appUrlTypeSubDirectoryTextBox').hide();
                $('#appUrlTypeCustomDomainTextBox').hide();
                $('#appUrlTypeDomainRoot').attr('checked', 'checked');
            }
            else if (urlType == 'subDirectory') {
                $('#appUrlTypeSubDirectoryExample').hide();
                $('#appUrlTypeCustomDomainExample').show();
                $('#appUrlTypeSubDirectoryTextBox').show();
                $('#appUrlTypeCustomDomainTextBox').hide();
                $('#appUrlTypeSubDirectory').attr('checked', 'checked');
                $('#appUrlTypeSubDirectoryTextBox').val(subDir);
            }
            else if (urlType == 'customDomain') {
                $('#appUrlTypeSubDirectoryExample').show();
                $('#appUrlTypeCustomDomainExample').hide();
                $('#appUrlTypeSubDirectoryTextBox').hide();
                $('#appUrlTypeCustomDomainTextBox').show();
                $('#appUrlTypeCustomDomain').attr('checked', 'checked');
                $('#appUrlTypeCustomDomainTextBox').val(customDomain);
            }
        }


        function toggleBlock(to, from) {
            $('#' + from).hide();
            $('#' + to).show();
        }

        function submitForm() {
            if ($('#IsCustomDomain').val() == 'false')
                $('#Domain').val($('#subDomain').val());
            else if ($('#IsCustomDomain').val() == 'true')
                $('#Domain').val($('#customDomain').val());

            document.signupForm.submit();
        }

        $(document).ready(function() {
            displayAppUrls();
            setCustomDomainType(false);
        });
    </script>

    <div class="signupPage">
        <div id="editAppUrlDialog" title="Edit Url" style="display: none;">
            <p>
                <input type="radio" id="appUrlTypeDomainRoot" name="appUrlType" onclick="javascript:setEditUrlDialogVisibility('useDomain', '', '')" />Use
                Domain Root</p>
            <p>
                <span class="small domainPlaceholder"></span>.
            </p>
            <p>
                <input type="radio" id="appUrlTypeSubDirectory" name="appUrlType" onclick="javascript:setEditUrlDialogVisibility('subDirectory', '', '')" />Use
                Sub-Directory</p>
            <p>
                <span class="small domainPlaceholder"></span><span class="small">/</span><span id="appUrlTypeSubDirectoryExample"
                    class="small">subdirectory</span>
                <input type="text" id="appUrlTypeSubDirectoryTextBox" />
            </p>
            <p>
                <input type="radio" id="appUrlTypeCustomDomain" name="appUrlType" onclick="javascript:setEditUrlDialogVisibility('customDomain', '', '')" />Custom
                Domain</p>
            <p class="customDomain">
                <span id="appUrlTypeCustomDomainExample" class="small">stealth.yourcompany.com</span>
                <input type="text" id="appUrlTypeCustomDomainTextBox" size="50" />
            </p>
        </div>
        <div class="form leftPane">
            <h1>
                Fill up this form, and you are on your way.</h1>
            <p class="banner">
                Invite Code:                
                <%= Html.TextBox(Model, m => m.InviteCode)%>
                <br />
                <span class="small">An invite code is required during the private beta. No payment details
                    are necessary.</span>
            </p>
            <% using (Html.BeginValidatedForm(new { name = "signupForm", id = "signupForm" }))
               { %>
            <h2>
                <%= Model.Plan.Name%>
                Plan: $<%= Model.Plan.MonthlyFee%>/month</h2>
            <table class="labelControlPair">
                <tr>
                    <td>
                        <label for="firstName">
                            First name</label>
                    </td>
                    <td>
                        <%= Html.TextBox(Model, m => m.FirstName)%>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label for="lastName">
                            Last name</label>
                    </td>
                    <td>
                        <%= Html.TextBox(Model, m => m.LastName)%>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label for="company">
                            Company</label>
                    </td>
                    <td>
                        <%= Html.TextBox(Model, m => m.Company)%>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label for="company">
                            Email</label>
                    </td>
                    <td>
                        <%= Html.TextBox(Model, m => m.Email)%>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label for="company">
                            Timezone</label>
                    </td>
                    <td>
                        <select name="Timezone" id="Timezone">
                            <option value="-10:00">(GMT-10:00) America/Adak (Hawaii-Aleutian Standard Time)</option>
                            <option value="-10:00">(GMT-10:00) America/Atka (Hawaii-Aleutian Standard Time)</option>
                            <option value="-9:00">(GMT-9:00) America/Anchorage (Alaska Standard Time)</option>
                            <option value="-9:00">(GMT-9:00) America/Juneau (Alaska Standard Time)</option>
                            <option value="-9:00">(GMT-9:00) America/Nome (Alaska Standard Time)</option>
                            <option value="-9:00">(GMT-9:00) America/Yakutat (Alaska Standard Time)</option>
                            <option value="-8:00">(GMT-8:00) America/Dawson (Pacific Standard Time)</option>
                            <option value="-8:00">(GMT-8:00) America/Ensenada (Pacific Standard Time)</option>
                            <option value="-8:00">(GMT-8:00) America/Los_Angeles (Pacific Standard Time)</option>
                            <option value="-8:00">(GMT-8:00) America/Tijuana (Pacific Standard Time)</option>
                            <option value="-8:00">(GMT-8:00) America/Vancouver (Pacific Standard Time)</option>
                            <option value="-8:00">(GMT-8:00) America/Whitehorse (Pacific Standard Time)</option>
                            <option value="-8:00">(GMT-8:00) Canada/Pacific (Pacific Standard Time)</option>
                            <option value="-8:00">(GMT-8:00) Canada/Yukon (Pacific Standard Time)</option>
                            <option value="-8:00">(GMT-8:00) Mexico/BajaNorte (Pacific Standard Time)</option>
                            <option value="-7:00">(GMT-7:00) America/Boise (Mountain Standard Time)</option>
                            <option value="-7:00">(GMT-7:00) America/Cambridge_Bay (Mountain Standard Time)</option>
                            <option value="-7:00">(GMT-7:00) America/Chihuahua (Mountain Standard Time)</option>
                            <option value="-7:00">(GMT-7:00) America/Dawson_Creek (Mountain Standard Time)</option>
                            <option value="-7:00">(GMT-7:00) America/Denver (Mountain Standard Time)</option>
                            <option value="-7:00">(GMT-7:00) America/Edmonton (Mountain Standard Time)</option>
                            <option value="-7:00">(GMT-7:00) America/Hermosillo (Mountain Standard Time)</option>
                            <option value="-7:00">(GMT-7:00) America/Inuvik (Mountain Standard Time)</option>
                            <option value="-7:00">(GMT-7:00) America/Mazatlan (Mountain Standard Time)</option>
                            <option value="-7:00">(GMT-7:00) America/Phoenix (Mountain Standard Time)</option>
                            <option value="-7:00">(GMT-7:00) America/Shiprock (Mountain Standard Time)</option>
                            <option value="-7:00">(GMT-7:00) America/Yellowknife (Mountain Standard Time)</option>
                            <option value="-7:00">(GMT-7:00) Canada/Mountain (Mountain Standard Time)</option>
                            <option value="-7:00">(GMT-7:00) Mexico/BajaSur (Mountain Standard Time)</option>
                            <option value="-6:00">(GMT-6:00) America/Belize (Central Standard Time)</option>
                            <option value="-6:00">(GMT-6:00) America/Cancun (Central Standard Time)</option>
                            <option value="-6:00">(GMT-6:00) America/Chicago (Central Standard Time)</option>
                            <option value="-6:00">(GMT-6:00) America/Costa_Rica (Central Standard Time)</option>
                            <option value="-6:00">(GMT-6:00) America/El_Salvador (Central Standard Time)</option>
                            <option value="-6:00">(GMT-6:00) America/Guatemala (Central Standard Time)</option>
                            <option value="-6:00">(GMT-6:00) America/Knox_IN (Central Standard Time)</option>
                            <option value="-6:00">(GMT-6:00) America/Managua (Central Standard Time)</option>
                            <option value="-6:00">(GMT-6:00) America/Menominee (Central Standard Time)</option>
                            <option value="-6:00">(GMT-6:00) America/Merida (Central Standard Time)</option>
                            <option value="-6:00">(GMT-6:00) America/Mexico_City (Central Standard Time)</option>
                            <option value="-6:00">(GMT-6:00) America/Monterrey (Central Standard Time)</option>
                            <option value="-6:00">(GMT-6:00) America/Rainy_River (Central Standard Time)</option>
                            <option value="-6:00">(GMT-6:00) America/Rankin_Inlet (Central Standard Time)</option>
                            <option value="-6:00">(GMT-6:00) America/Regina (Central Standard Time)</option>
                            <option value="-6:00">(GMT-6:00) America/Swift_Current (Central Standard Time)</option>
                            <option value="-6:00">(GMT-6:00) America/Tegucigalpa (Central Standard Time)</option>
                            <option value="-6:00">(GMT-6:00) America/Winnipeg (Central Standard Time)</option>
                            <option value="-6:00">(GMT-6:00) Canada/Central (Central Standard Time)</option>
                            <option value="-6:00">(GMT-6:00) Canada/East-Saskatchewan (Central Standard Time)</option>
                            <option value="-6:00">(GMT-6:00) Canada/Saskatchewan (Central Standard Time)</option>
                            <option value="-6:00">(GMT-6:00) Chile/EasterIsland (Easter Is. Time)</option>
                            <option value="-6:00">(GMT-6:00) Mexico/General (Central Standard Time)</option>
                            <option value="-5:00">(GMT-5:00) America/Atikokan (Eastern Standard Time)</option>
                            <option value="-5:00">(GMT-5:00) America/Bogota (Colombia Time)</option>
                            <option value="-5:00">(GMT-5:00) America/Cayman (Eastern Standard Time)</option>
                            <option value="-5:00">(GMT-5:00) America/Coral_Harbour (Eastern Standard Time)</option>
                            <option value="-5:00">(GMT-5:00) America/Detroit (Eastern Standard Time)</option>
                            <option value="-5:00">(GMT-5:00) America/Fort_Wayne (Eastern Standard Time)</option>
                            <option value="-5:00">(GMT-5:00) America/Grand_Turk (Eastern Standard Time)</option>
                            <option value="-5:00">(GMT-5:00) America/Guayaquil (Ecuador Time)</option>
                            <option value="-5:00">(GMT-5:00) America/Havana (Cuba Standard Time)</option>
                            <option value="-5:00">(GMT-5:00) America/Indianapolis (Eastern Standard Time)</option>
                            <option value="-5:00">(GMT-5:00) America/Iqaluit (Eastern Standard Time)</option>
                            <option value="-5:00">(GMT-5:00) America/Jamaica (Eastern Standard Time)</option>
                            <option value="-5:00">(GMT-5:00) America/Lima (Peru Time)</option>
                            <option value="-5:00">(GMT-5:00) America/Louisville (Eastern Standard Time)</option>
                            <option value="-5:00">(GMT-5:00) America/Montreal (Eastern Standard Time)</option>
                            <option value="-5:00">(GMT-5:00) America/Nassau (Eastern Standard Time)</option>
                            <option value="-5:00">(GMT-5:00) America/New_York (Eastern Standard Time)</option>
                            <option value="-5:00">(GMT-5:00) America/Nipigon (Eastern Standard Time)</option>
                            <option value="-5:00">(GMT-5:00) America/Panama (Eastern Standard Time)</option>
                            <option value="-5:00">(GMT-5:00) America/Pangnirtung (Eastern Standard Time)</option>
                            <option value="-5:00">(GMT-5:00) America/Port-au-Prince (Eastern Standard Time)</option>
                            <option value="-5:00">(GMT-5:00) America/Resolute (Eastern Standard Time)</option>
                            <option value="-5:00">(GMT-5:00) America/Thunder_Bay (Eastern Standard Time)</option>
                            <option value="-5:00">(GMT-5:00) America/Toronto (Eastern Standard Time)</option>
                            <option value="-5:00">(GMT-5:00) Canada/Eastern (Eastern Standard Time)</option>
                            <option value="-4:-30">(GMT-4:-30) America/Caracas (Venezuela Time)</option>
                            <option value="-4:00">(GMT-4:00) America/Anguilla (Atlantic Standard Time)</option>
                            <option value="-4:00">(GMT-4:00) America/Antigua (Atlantic Standard Time)</option>
                            <option value="-4:00">(GMT-4:00) America/Aruba (Atlantic Standard Time)</option>
                            <option value="-4:00">(GMT-4:00) America/Asuncion (Paraguay Time)</option>
                            <option value="-4:00">(GMT-4:00) America/Barbados (Atlantic Standard Time)</option>
                            <option value="-4:00">(GMT-4:00) America/Blanc-Sablon (Atlantic Standard Time)</option>
                            <option value="-4:00">(GMT-4:00) America/Boa_Vista (Amazon Time)</option>
                            <option value="-4:00">(GMT-4:00) America/Campo_Grande (Amazon Time)</option>
                            <option value="-4:00">(GMT-4:00) America/Cuiaba (Amazon Time)</option>
                            <option value="-4:00">(GMT-4:00) America/Curacao (Atlantic Standard Time)</option>
                            <option value="-4:00">(GMT-4:00) America/Dominica (Atlantic Standard Time)</option>
                            <option value="-4:00">(GMT-4:00) America/Eirunepe (Amazon Time)</option>
                            <option value="-4:00">(GMT-4:00) America/Glace_Bay (Atlantic Standard Time)</option>
                            <option value="-4:00">(GMT-4:00) America/Goose_Bay (Atlantic Standard Time)</option>
                            <option value="-4:00">(GMT-4:00) America/Grenada (Atlantic Standard Time)</option>
                            <option value="-4:00">(GMT-4:00) America/Guadeloupe (Atlantic Standard Time)</option>
                            <option value="-4:00">(GMT-4:00) America/Guyana (Guyana Time)</option>
                            <option value="-4:00">(GMT-4:00) America/Halifax (Atlantic Standard Time)</option>
                            <option value="-4:00">(GMT-4:00) America/La_Paz (Bolivia Time)</option>
                            <option value="-4:00">(GMT-4:00) America/Manaus (Amazon Time)</option>
                            <option value="-4:00">(GMT-4:00) America/Marigot (Atlantic Standard Time)</option>
                            <option value="-4:00">(GMT-4:00) America/Martinique (Atlantic Standard Time)</option>
                            <option value="-4:00">(GMT-4:00) America/Moncton (Atlantic Standard Time)</option>
                            <option value="-4:00">(GMT-4:00) America/Montserrat (Atlantic Standard Time)</option>
                            <option value="-4:00">(GMT-4:00) America/Port_of_Spain (Atlantic Standard Time)</option>
                            <option value="-4:00">(GMT-4:00) America/Porto_Acre (Amazon Time)</option>
                            <option value="-4:00">(GMT-4:00) America/Porto_Velho (Amazon Time)</option>
                            <option value="-4:00">(GMT-4:00) America/Puerto_Rico (Atlantic Standard Time)</option>
                            <option value="-4:00">(GMT-4:00) America/Rio_Branco (Amazon Time)</option>
                            <option value="-4:00">(GMT-4:00) America/Santiago (Chile Time)</option>
                            <option value="-4:00">(GMT-4:00) America/Santo_Domingo (Atlantic Standard Time)</option>
                            <option value="-4:00">(GMT-4:00) America/St_Barthelemy (Atlantic Standard Time)</option>
                            <option value="-4:00">(GMT-4:00) America/St_Kitts (Atlantic Standard Time)</option>
                            <option value="-4:00">(GMT-4:00) America/St_Lucia (Atlantic Standard Time)</option>
                            <option value="-4:00">(GMT-4:00) America/St_Thomas (Atlantic Standard Time)</option>
                            <option value="-4:00">(GMT-4:00) America/St_Vincent (Atlantic Standard Time)</option>
                            <option value="-4:00">(GMT-4:00) America/Thule (Atlantic Standard Time)</option>
                            <option value="-4:00">(GMT-4:00) America/Tortola (Atlantic Standard Time)</option>
                            <option value="-4:00">(GMT-4:00) America/Virgin (Atlantic Standard Time)</option>
                            <option value="-4:00">(GMT-4:00) Antarctica/Palmer (Chile Time)</option>
                            <option value="-4:00">(GMT-4:00) Atlantic/Bermuda (Atlantic Standard Time)</option>
                            <option value="-4:00">(GMT-4:00) Atlantic/Stanley (Falkland Is. Time)</option>
                            <option value="-4:00">(GMT-4:00) Brazil/Acre (Amazon Time)</option>
                            <option value="-4:00">(GMT-4:00) Brazil/West (Amazon Time)</option>
                            <option value="-4:00">(GMT-4:00) Canada/Atlantic (Atlantic Standard Time)</option>
                            <option value="-4:00">(GMT-4:00) Chile/Continental (Chile Time)</option>
                            <option value="-3:-30">(GMT-3:-30) America/St_Johns (Newfoundland Standard Time)</option>
                            <option value="-3:-30">(GMT-3:-30) Canada/Newfoundland (Newfoundland Standard Time)</option>
                            <option value="-3:00">(GMT-3:00) America/Araguaina (Brasilia Time)</option>
                            <option value="-3:00">(GMT-3:00) America/Bahia (Brasilia Time)</option>
                            <option value="-3:00">(GMT-3:00) America/Belem (Brasilia Time)</option>
                            <option value="-3:00">(GMT-3:00) America/Buenos_Aires (Argentine Time)</option>
                            <option value="-3:00">(GMT-3:00) America/Catamarca (Argentine Time)</option>
                            <option value="-3:00">(GMT-3:00) America/Cayenne (French Guiana Time)</option>
                            <option value="-3:00">(GMT-3:00) America/Cordoba (Argentine Time)</option>
                            <option value="-3:00">(GMT-3:00) America/Fortaleza (Brasilia Time)</option>
                            <option value="-3:00">(GMT-3:00) America/Godthab (Western Greenland Time)</option>
                            <option value="-3:00">(GMT-3:00) America/Jujuy (Argentine Time)</option>
                            <option value="-3:00">(GMT-3:00) America/Maceio (Brasilia Time)</option>
                            <option value="-3:00">(GMT-3:00) America/Mendoza (Argentine Time)</option>
                            <option value="-3:00">(GMT-3:00) America/Miquelon (Pierre & Miquelon Standard Time)</option>
                            <option value="-3:00">(GMT-3:00) America/Montevideo (Uruguay Time)</option>
                            <option value="-3:00">(GMT-3:00) America/Paramaribo (Suriname Time)</option>
                            <option value="-3:00">(GMT-3:00) America/Recife (Brasilia Time)</option>
                            <option value="-3:00">(GMT-3:00) America/Rosario (Argentine Time)</option>
                            <option value="-3:00">(GMT-3:00) America/Santarem (Brasilia Time)</option>
                            <option value="-3:00">(GMT-3:00) America/Sao_Paulo (Brasilia Time)</option>
                            <option value="-3:00">(GMT-3:00) Antarctica/Rothera (Rothera Time)</option>
                            <option value="-3:00">(GMT-3:00) Brazil/East (Brasilia Time)</option>
                            <option value="-2:00">(GMT-2:00) America/Noronha (Fernando de Noronha Time)</option>
                            <option value="-2:00">(GMT-2:00) Atlantic/South_Georgia (South Georgia Standard Time)</option>
                            <option value="-2:00">(GMT-2:00) Brazil/DeNoronha (Fernando de Noronha Time)</option>
                            <option value="-1:00">(GMT-1:00) America/Scoresbysund (Eastern Greenland Time)</option>
                            <option value="-1:00">(GMT-1:00) Atlantic/Azores (Azores Time)</option>
                            <option value="-1:00">(GMT-1:00) Atlantic/Cape_Verde (Cape Verde Time)</option>
                            <option value="+0:00">(GMT+0:00) Africa/Abidjan (Greenwich Mean Time)</option>
                            <option value="+0:00">(GMT+0:00) Africa/Accra (Ghana Mean Time)</option>
                            <option value="+0:00">(GMT+0:00) Africa/Bamako (Greenwich Mean Time)</option>
                            <option value="+0:00">(GMT+0:00) Africa/Banjul (Greenwich Mean Time)</option>
                            <option value="+0:00">(GMT+0:00) Africa/Bissau (Greenwich Mean Time)</option>
                            <option value="+0:00">(GMT+0:00) Africa/Casablanca (Western European Time)</option>
                            <option value="+0:00">(GMT+0:00) Africa/Conakry (Greenwich Mean Time)</option>
                            <option value="+0:00">(GMT+0:00) Africa/Dakar (Greenwich Mean Time)</option>
                            <option value="+0:00">(GMT+0:00) Africa/El_Aaiun (Western European Time)</option>
                            <option value="+0:00">(GMT+0:00) Africa/Freetown (Greenwich Mean Time)</option>
                            <option value="+0:00">(GMT+0:00) Africa/Lome (Greenwich Mean Time)</option>
                            <option value="+0:00">(GMT+0:00) Africa/Monrovia (Greenwich Mean Time)</option>
                            <option value="+0:00">(GMT+0:00) Africa/Nouakchott (Greenwich Mean Time)</option>
                            <option value="+0:00">(GMT+0:00) Africa/Ouagadougou (Greenwich Mean Time)</option>
                            <option value="+0:00">(GMT+0:00) Africa/Sao_Tome (Greenwich Mean Time)</option>
                            <option value="+0:00">(GMT+0:00) Africa/Timbuktu (Greenwich Mean Time)</option>
                            <option value="+0:00">(GMT+0:00) America/Danmarkshavn (Greenwich Mean Time)</option>
                            <option value="+0:00">(GMT+0:00) Atlantic/Canary (Western European Time)</option>
                            <option value="+0:00">(GMT+0:00) Atlantic/Faeroe (Western European Time)</option>
                            <option value="+0:00">(GMT+0:00) Atlantic/Faroe (Western European Time)</option>
                            <option value="+0:00">(GMT+0:00) Atlantic/Madeira (Western European Time)</option>
                            <option value="+0:00">(GMT+0:00) Atlantic/Reykjavik (Greenwich Mean Time)</option>
                            <option value="+0:00">(GMT+0:00) Atlantic/St_Helena (Greenwich Mean Time)</option>
                            <option value="+0:00">(GMT+0:00) Europe/Belfast (Greenwich Mean Time)</option>
                            <option value="+0:00">(GMT+0:00) Europe/Dublin (Greenwich Mean Time)</option>
                            <option value="+0:00">(GMT+0:00) Europe/Guernsey (Greenwich Mean Time)</option>
                            <option value="+0:00">(GMT+0:00) Europe/Isle_of_Man (Greenwich Mean Time)</option>
                            <option value="+0:00">(GMT+0:00) Europe/Jersey (Greenwich Mean Time)</option>
                            <option value="+0:00">(GMT+0:00) Europe/Lisbon (Western European Time)</option>
                            <option value="+0:00">(GMT+0:00) Europe/London (Greenwich Mean Time)</option>
                            <option value="+1:00">(GMT+1:00) Africa/Algiers (Central European Time)</option>
                            <option value="+1:00">(GMT+1:00) Africa/Bangui (Western African Time)</option>
                            <option value="+1:00">(GMT+1:00) Africa/Brazzaville (Western African Time)</option>
                            <option value="+1:00">(GMT+1:00) Africa/Ceuta (Central European Time)</option>
                            <option value="+1:00">(GMT+1:00) Africa/Douala (Western African Time)</option>
                            <option value="+1:00">(GMT+1:00) Africa/Kinshasa (Western African Time)</option>
                            <option value="+1:00">(GMT+1:00) Africa/Lagos (Western African Time)</option>
                            <option value="+1:00">(GMT+1:00) Africa/Libreville (Western African Time)</option>
                            <option value="+1:00">(GMT+1:00) Africa/Luanda (Western African Time)</option>
                            <option value="+1:00">(GMT+1:00) Africa/Malabo (Western African Time)</option>
                            <option value="+1:00">(GMT+1:00) Africa/Ndjamena (Western African Time)</option>
                            <option value="+1:00">(GMT+1:00) Africa/Niamey (Western African Time)</option>
                            <option value="+1:00">(GMT+1:00) Africa/Porto-Novo (Western African Time)</option>
                            <option value="+1:00">(GMT+1:00) Africa/Tunis (Central European Time)</option>
                            <option value="+1:00">(GMT+1:00) Africa/Windhoek (Western African Time)</option>
                            <option value="+1:00">(GMT+1:00) Arctic/Longyearbyen (Central European Time)</option>
                            <option value="+1:00">(GMT+1:00) Atlantic/Jan_Mayen (Central European Time)</option>
                            <option value="+1:00">(GMT+1:00) Europe/Amsterdam (Central European Time)</option>
                            <option value="+1:00">(GMT+1:00) Europe/Andorra (Central European Time)</option>
                            <option value="+1:00">(GMT+1:00) Europe/Belgrade (Central European Time)</option>
                            <option value="+1:00">(GMT+1:00) Europe/Berlin (Central European Time)</option>
                            <option value="+1:00">(GMT+1:00) Europe/Bratislava (Central European Time)</option>
                            <option value="+1:00">(GMT+1:00) Europe/Brussels (Central European Time)</option>
                            <option value="+1:00">(GMT+1:00) Europe/Budapest (Central European Time)</option>
                            <option value="+1:00">(GMT+1:00) Europe/Copenhagen (Central European Time)</option>
                            <option value="+1:00">(GMT+1:00) Europe/Gibraltar (Central European Time)</option>
                            <option value="+1:00">(GMT+1:00) Europe/Ljubljana (Central European Time)</option>
                            <option value="+1:00">(GMT+1:00) Europe/Luxembourg (Central European Time)</option>
                            <option value="+1:00">(GMT+1:00) Europe/Madrid (Central European Time)</option>
                            <option value="+1:00">(GMT+1:00) Europe/Malta (Central European Time)</option>
                            <option value="+1:00">(GMT+1:00) Europe/Monaco (Central European Time)</option>
                            <option value="+1:00">(GMT+1:00) Europe/Oslo (Central European Time)</option>
                            <option value="+1:00">(GMT+1:00) Europe/Paris (Central European Time)</option>
                            <option value="+1:00">(GMT+1:00) Europe/Podgorica (Central European Time)</option>
                            <option value="+1:00">(GMT+1:00) Europe/Prague (Central European Time)</option>
                            <option value="+1:00">(GMT+1:00) Europe/Rome (Central European Time)</option>
                            <option value="+1:00">(GMT+1:00) Europe/San_Marino (Central European Time)</option>
                            <option value="+1:00">(GMT+1:00) Europe/Sarajevo (Central European Time)</option>
                            <option value="+1:00">(GMT+1:00) Europe/Skopje (Central European Time)</option>
                            <option value="+1:00">(GMT+1:00) Europe/Stockholm (Central European Time)</option>
                            <option value="+1:00">(GMT+1:00) Europe/Tirane (Central European Time)</option>
                            <option value="+1:00">(GMT+1:00) Europe/Vaduz (Central European Time)</option>
                            <option value="+1:00">(GMT+1:00) Europe/Vatican (Central European Time)</option>
                            <option value="+1:00">(GMT+1:00) Europe/Vienna (Central European Time)</option>
                            <option value="+1:00">(GMT+1:00) Europe/Warsaw (Central European Time)</option>
                            <option value="+1:00">(GMT+1:00) Europe/Zagreb (Central European Time)</option>
                            <option value="+1:00">(GMT+1:00) Europe/Zurich (Central European Time)</option>
                            <option value="+2:00">(GMT+2:00) Africa/Blantyre (Central African Time)</option>
                            <option value="+2:00">(GMT+2:00) Africa/Bujumbura (Central African Time)</option>
                            <option value="+2:00">(GMT+2:00) Africa/Cairo (Eastern European Time)</option>
                            <option value="+2:00">(GMT+2:00) Africa/Gaborone (Central African Time)</option>
                            <option value="+2:00">(GMT+2:00) Africa/Harare (Central African Time)</option>
                            <option value="+2:00">(GMT+2:00) Africa/Johannesburg (South Africa Standard Time)</option>
                            <option value="+2:00">(GMT+2:00) Africa/Kigali (Central African Time)</option>
                            <option value="+2:00">(GMT+2:00) Africa/Lubumbashi (Central African Time)</option>
                            <option value="+2:00">(GMT+2:00) Africa/Lusaka (Central African Time)</option>
                            <option value="+2:00">(GMT+2:00) Africa/Maputo (Central African Time)</option>
                            <option value="+2:00">(GMT+2:00) Africa/Maseru (South Africa Standard Time)</option>
                            <option value="+2:00">(GMT+2:00) Africa/Mbabane (South Africa Standard Time)</option>
                            <option value="+2:00">(GMT+2:00) Africa/Tripoli (Eastern European Time)</option>
                            <option value="+2:00">(GMT+2:00) Asia/Amman (Eastern European Time)</option>
                            <option value="+2:00">(GMT+2:00) Asia/Beirut (Eastern European Time)</option>
                            <option value="+2:00">(GMT+2:00) Asia/Damascus (Eastern European Time)</option>
                            <option value="+2:00">(GMT+2:00) Asia/Gaza (Eastern European Time)</option>
                            <option value="+2:00">(GMT+2:00) Asia/Istanbul (Eastern European Time)</option>
                            <option value="+2:00">(GMT+2:00) Asia/Jerusalem (Israel Standard Time)</option>
                            <option value="+2:00">(GMT+2:00) Asia/Nicosia (Eastern European Time)</option>
                            <option value="+2:00">(GMT+2:00) Asia/Tel_Aviv (Israel Standard Time)</option>
                            <option value="+2:00">(GMT+2:00) Europe/Athens (Eastern European Time)</option>
                            <option value="+2:00">(GMT+2:00) Europe/Bucharest (Eastern European Time)</option>
                            <option value="+2:00">(GMT+2:00) Europe/Chisinau (Eastern European Time)</option>
                            <option value="+2:00">(GMT+2:00) Europe/Helsinki (Eastern European Time)</option>
                            <option value="+2:00">(GMT+2:00) Europe/Istanbul (Eastern European Time)</option>
                            <option value="+2:00">(GMT+2:00) Europe/Kaliningrad (Eastern European Time)</option>
                            <option value="+2:00">(GMT+2:00) Europe/Kiev (Eastern European Time)</option>
                            <option value="+2:00">(GMT+2:00) Europe/Mariehamn (Eastern European Time)</option>
                            <option value="+2:00">(GMT+2:00) Europe/Minsk (Eastern European Time)</option>
                            <option value="+2:00">(GMT+2:00) Europe/Nicosia (Eastern European Time)</option>
                            <option value="+2:00">(GMT+2:00) Europe/Riga (Eastern European Time)</option>
                            <option value="+2:00">(GMT+2:00) Europe/Simferopol (Eastern European Time)</option>
                            <option value="+2:00">(GMT+2:00) Europe/Sofia (Eastern European Time)</option>
                            <option value="+2:00">(GMT+2:00) Europe/Tallinn (Eastern European Time)</option>
                            <option value="+2:00">(GMT+2:00) Europe/Tiraspol (Eastern European Time)</option>
                            <option value="+2:00">(GMT+2:00) Europe/Uzhgorod (Eastern European Time)</option>
                            <option value="+2:00">(GMT+2:00) Europe/Vilnius (Eastern European Time)</option>
                            <option value="+2:00">(GMT+2:00) Europe/Zaporozhye (Eastern European Time)</option>
                            <option value="+3:00">(GMT+3:00) Africa/Addis_Ababa (Eastern African Time)</option>
                            <option value="+3:00">(GMT+3:00) Africa/Asmara (Eastern African Time)</option>
                            <option value="+3:00">(GMT+3:00) Africa/Asmera (Eastern African Time)</option>
                            <option value="+3:00">(GMT+3:00) Africa/Dar_es_Salaam (Eastern African Time)</option>
                            <option value="+3:00">(GMT+3:00) Africa/Djibouti (Eastern African Time)</option>
                            <option value="+3:00">(GMT+3:00) Africa/Kampala (Eastern African Time)</option>
                            <option value="+3:00">(GMT+3:00) Africa/Khartoum (Eastern African Time)</option>
                            <option value="+3:00">(GMT+3:00) Africa/Mogadishu (Eastern African Time)</option>
                            <option value="+3:00">(GMT+3:00) Africa/Nairobi (Eastern African Time)</option>
                            <option value="+3:00">(GMT+3:00) Antarctica/Syowa (Syowa Time)</option>
                            <option value="+3:00">(GMT+3:00) Asia/Aden (Arabia Standard Time)</option>
                            <option value="+3:00">(GMT+3:00) Asia/Baghdad (Arabia Standard Time)</option>
                            <option value="+3:00">(GMT+3:00) Asia/Bahrain (Arabia Standard Time)</option>
                            <option value="+3:00">(GMT+3:00) Asia/Kuwait (Arabia Standard Time)</option>
                            <option value="+3:00">(GMT+3:00) Asia/Qatar (Arabia Standard Time)</option>
                            <option value="+3:00">(GMT+3:00) Europe/Moscow (Moscow Standard Time)</option>
                            <option value="+3:00">(GMT+3:00) Europe/Volgograd (Volgograd Time)</option>
                            <option value="+3:00">(GMT+3:00) Indian/Antananarivo (Eastern African Time)</option>
                            <option value="+3:00">(GMT+3:00) Indian/Comoro (Eastern African Time)</option>
                            <option value="+3:00">(GMT+3:00) Indian/Mayotte (Eastern African Time)</option>
                            <option value="+3:30">(GMT+3:30) Asia/Tehran (Iran Standard Time)</option>
                            <option value="+4:00">(GMT+4:00) Asia/Baku (Azerbaijan Time)</option>
                            <option value="+4:00">(GMT+4:00) Asia/Dubai (Gulf Standard Time)</option>
                            <option value="+4:00">(GMT+4:00) Asia/Muscat (Gulf Standard Time)</option>
                            <option value="+4:00">(GMT+4:00) Asia/Tbilisi (Georgia Time)</option>
                            <option value="+4:00">(GMT+4:00) Asia/Yerevan (Armenia Time)</option>
                            <option value="+4:00">(GMT+4:00) Europe/Samara (Samara Time)</option>
                            <option value="+4:00">(GMT+4:00) Indian/Mahe (Seychelles Time)</option>
                            <option value="+4:00">(GMT+4:00) Indian/Mauritius (Mauritius Time)</option>
                            <option value="+4:00">(GMT+4:00) Indian/Reunion (Reunion Time)</option>
                            <option value="+4:30">(GMT+4:30) Asia/Kabul (Afghanistan Time)</option>
                            <option value="+5:00">(GMT+5:00) Asia/Aqtau (Aqtau Time)</option>
                            <option value="+5:00">(GMT+5:00) Asia/Aqtobe (Aqtobe Time)</option>
                            <option value="+5:00">(GMT+5:00) Asia/Ashgabat (Turkmenistan Time)</option>
                            <option value="+5:00">(GMT+5:00) Asia/Ashkhabad (Turkmenistan Time)</option>
                            <option value="+5:00">(GMT+5:00) Asia/Dushanbe (Tajikistan Time)</option>
                            <option value="+5:00">(GMT+5:00) Asia/Karachi (Pakistan Time)</option>
                            <option value="+5:00">(GMT+5:00) Asia/Oral (Oral Time)</option>
                            <option value="+5:00">(GMT+5:00) Asia/Samarkand (Uzbekistan Time)</option>
                            <option value="+5:00">(GMT+5:00) Asia/Tashkent (Uzbekistan Time)</option>
                            <option value="+5:00">(GMT+5:00) Asia/Yekaterinburg (Yekaterinburg Time)</option>
                            <option value="+5:00">(GMT+5:00) Indian/Kerguelen (French Southern & Antarctic Lands
                                Time)</option>
                            <option value="+5:00">(GMT+5:00) Indian/Maldives (Maldives Time)</option>
                            <option selected="selected" value="+5:30">(GMT+5:30) Asia/Calcutta (India Standard Time)</option>
                            <option value="+5:30">(GMT+5:30) Asia/Colombo (India Standard Time)</option>
                            <option value="+5:30">(GMT+5:30) Asia/Kolkata (India Standard Time)</option>
                            <option value="+5:45">(GMT+5:45) Asia/Katmandu (Nepal Time)</option>
                            <option value="+6:00">(GMT+6:00) Antarctica/Mawson (Mawson Time)</option>
                            <option value="+6:00">(GMT+6:00) Antarctica/Vostok (Vostok Time)</option>
                            <option value="+6:00">(GMT+6:00) Asia/Almaty (Alma-Ata Time)</option>
                            <option value="+6:00">(GMT+6:00) Asia/Bishkek (Kirgizstan Time)</option>
                            <option value="+6:00">(GMT+6:00) Asia/Dacca (Bangladesh Time)</option>
                            <option value="+6:00">(GMT+6:00) Asia/Dhaka (Bangladesh Time)</option>
                            <option value="+6:00">(GMT+6:00) Asia/Novosibirsk (Novosibirsk Time)</option>
                            <option value="+6:00">(GMT+6:00) Asia/Omsk (Omsk Time)</option>
                            <option value="+6:00">(GMT+6:00) Asia/Qyzylorda (Qyzylorda Time)</option>
                            <option value="+6:00">(GMT+6:00) Asia/Thimbu (Bhutan Time)</option>
                            <option value="+6:00">(GMT+6:00) Asia/Thimphu (Bhutan Time)</option>
                            <option value="+6:00">(GMT+6:00) Indian/Chagos (Indian Ocean Territory Time)</option>
                            <option value="+6:30">(GMT+6:30) Asia/Rangoon (Myanmar Time)</option>
                            <option value="+6:30">(GMT+6:30) Indian/Cocos (Cocos Islands Time)</option>
                            <option value="+7:00">(GMT+7:00) Antarctica/Davis (Davis Time)</option>
                            <option value="+7:00">(GMT+7:00) Asia/Bangkok (Indochina Time)</option>
                            <option value="+7:00">(GMT+7:00) Asia/Ho_Chi_Minh (Indochina Time)</option>
                            <option value="+7:00">(GMT+7:00) Asia/Hovd (Hovd Time)</option>
                            <option value="+7:00">(GMT+7:00) Asia/Jakarta (West Indonesia Time)</option>
                            <option value="+7:00">(GMT+7:00) Asia/Krasnoyarsk (Krasnoyarsk Time)</option>
                            <option value="+7:00">(GMT+7:00) Asia/Phnom_Penh (Indochina Time)</option>
                            <option value="+7:00">(GMT+7:00) Asia/Pontianak (West Indonesia Time)</option>
                            <option value="+7:00">(GMT+7:00) Asia/Saigon (Indochina Time)</option>
                            <option value="+7:00">(GMT+7:00) Asia/Vientiane (Indochina Time)</option>
                            <option value="+7:00">(GMT+7:00) Indian/Christmas (Christmas Island Time)</option>
                            <option value="+8:00">(GMT+8:00) Antarctica/Casey (Western Standard Time (Australia))</option>
                            <option value="+8:00">(GMT+8:00) Asia/Brunei (Brunei Time)</option>
                            <option value="+8:00">(GMT+8:00) Asia/Choibalsan (Choibalsan Time)</option>
                            <option value="+8:00">(GMT+8:00) Asia/Chongqing (China Standard Time)</option>
                            <option value="+8:00">(GMT+8:00) Asia/Chungking (China Standard Time)</option>
                            <option value="+8:00">(GMT+8:00) Asia/Harbin (China Standard Time)</option>
                            <option value="+8:00">(GMT+8:00) Asia/Hong_Kong (Hong Kong Time)</option>
                            <option value="+8:00">(GMT+8:00) Asia/Irkutsk (Irkutsk Time)</option>
                            <option value="+8:00">(GMT+8:00) Asia/Kashgar (China Standard Time)</option>
                            <option value="+8:00">(GMT+8:00) Asia/Kuala_Lumpur (Malaysia Time)</option>
                            <option value="+8:00">(GMT+8:00) Asia/Kuching (Malaysia Time)</option>
                            <option value="+8:00">(GMT+8:00) Asia/Macao (China Standard Time)</option>
                            <option value="+8:00">(GMT+8:00) Asia/Macau (China Standard Time)</option>
                            <option value="+8:00">(GMT+8:00) Asia/Makassar (Central Indonesia Time)</option>
                            <option value="+8:00">(GMT+8:00) Asia/Manila (Philippines Time)</option>
                            <option value="+8:00">(GMT+8:00) Asia/Shanghai (China Standard Time)</option>
                            <option value="+8:00">(GMT+8:00) Asia/Singapore (Singapore Time)</option>
                            <option value="+8:00">(GMT+8:00) Asia/Taipei (China Standard Time)</option>
                            <option value="+8:00">(GMT+8:00) Asia/Ujung_Pandang (Central Indonesia Time)</option>
                            <option value="+8:00">(GMT+8:00) Asia/Ulaanbaatar (Ulaanbaatar Time)</option>
                            <option value="+8:00">(GMT+8:00) Asia/Ulan_Bator (Ulaanbaatar Time)</option>
                            <option value="+8:00">(GMT+8:00) Asia/Urumqi (China Standard Time)</option>
                            <option value="+8:00">(GMT+8:00) Australia/Perth (Western Standard Time (Australia))</option>
                            <option value="+8:00">(GMT+8:00) Australia/West (Western Standard Time (Australia))</option>
                            <option value="+8:45">(GMT+8:45) Australia/Eucla (Central Western Standard Time (Australia))</option>
                            <option value="+9:00">(GMT+9:00) Asia/Dili (Timor-Leste Time)</option>
                            <option value="+9:00">(GMT+9:00) Asia/Jayapura (East Indonesia Time)</option>
                            <option value="+9:00">(GMT+9:00) Asia/Pyongyang (Korea Standard Time)</option>
                            <option value="+9:00">(GMT+9:00) Asia/Seoul (Korea Standard Time)</option>
                            <option value="+9:00">(GMT+9:00) Asia/Tokyo (Japan Standard Time)</option>
                            <option value="+9:00">(GMT+9:00) Asia/Yakutsk (Yakutsk Time)</option>
                            <option value="+9:30">(GMT+9:30) Australia/Adelaide (Central Standard Time (South Australia))</option>
                            <option value="+9:30">(GMT+9:30) Australia/Broken_Hill (Central Standard Time (South
                                Australia/New South Wales))</option>
                            <option value="+9:30">(GMT+9:30) Australia/Darwin (Central Standard Time (Northern Territory))</option>
                            <option value="+9:30">(GMT+9:30) Australia/North (Central Standard Time (Northern Territory))</option>
                            <option value="+9:30">(GMT+9:30) Australia/South (Central Standard Time (South Australia))</option>
                            <option value="+9:30">(GMT+9:30) Australia/Yancowinna (Central Standard Time (South
                                Australia/New South Wales))</option>
                            <option value="+10:00">(GMT+10:00) Antarctica/DumontDUrville (Dumont-d'Urville Time)</option>
                            <option value="+10:00">(GMT+10:00) Asia/Sakhalin (Sakhalin Time)</option>
                            <option value="+10:00">(GMT+10:00) Asia/Vladivostok (Vladivostok Time)</option>
                            <option value="+10:00">(GMT+10:00) Australia/ACT (Eastern Standard Time (New South Wales))</option>
                            <option value="+10:00">(GMT+10:00) Australia/Brisbane (Eastern Standard Time (Queensland))</option>
                            <option value="+10:00">(GMT+10:00) Australia/Canberra (Eastern Standard Time (New South
                                Wales))</option>
                            <option value="+10:00">(GMT+10:00) Australia/Currie (Eastern Standard Time (New South
                                Wales))</option>
                            <option value="+10:00">(GMT+10:00) Australia/Hobart (Eastern Standard Time (Tasmania))</option>
                            <option value="+10:00">(GMT+10:00) Australia/Lindeman (Eastern Standard Time (Queensland))</option>
                            <option value="+10:00">(GMT+10:00) Australia/Melbourne (Eastern Standard Time (Victoria))</option>
                            <option value="+10:00">(GMT+10:00) Australia/NSW (Eastern Standard Time (New South Wales))</option>
                            <option value="+10:00">(GMT+10:00) Australia/Queensland (Eastern Standard Time (Queensland))</option>
                            <option value="+10:00">(GMT+10:00) Australia/Sydney (Eastern Standard Time (New South
                                Wales))</option>
                            <option value="+10:00">(GMT+10:00) Australia/Tasmania (Eastern Standard Time (Tasmania))</option>
                            <option value="+10:00">(GMT+10:00) Australia/Victoria (Eastern Standard Time (Victoria))</option>
                            <option value="+10:30">(GMT+10:30) Australia/LHI (Lord Howe Standard Time)</option>
                            <option value="+10:30">(GMT+10:30) Australia/Lord_Howe (Lord Howe Standard Time)</option>
                            <option value="+11:00">(GMT+11:00) Asia/Magadan (Magadan Time)</option>
                            <option value="+12:00">(GMT+12:00) Antarctica/McMurdo (New Zealand Standard Time)</option>
                            <option value="+12:00">(GMT+12:00) Antarctica/South_Pole (New Zealand Standard Time)</option>
                            <option value="+12:00">(GMT+12:00) Asia/Anadyr (Anadyr Time)</option>
                            <option value="+12:00">(GMT+12:00) Asia/Kamchatka (Petropavlovsk-Kamchatski Time)</option>
                        </select>
                    </td>
                </tr>
            </table>
            <h3>
                Create the administrator account</h3>
            <table class="labelControlPair">
                <tr>
                    <td>
                        <label for="username">
                            Username</label>
                    </td>
                    <td>
                        <%= Html.TextBox(Model, m => m.AdminUsername)%>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label for="password">
                            Password</label>
                    </td>
                    <td>
                        <%= Html.Password(Model, m => m.Password)%>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label for="repeat password">
                            Repeat Password</label>
                    </td>
                    <td>
                        <%= Html.Password("RepeatPassword")%>
                    </td>
                </tr>
            </table>
            <h3>
                Configure domain and applications</h3>
            <div>
                <%= Html.Hidden(Model, m => m.Domain)%>
                <%= Html.Hidden(Model, m => m.IsCustomDomain)%>
                
                <div id="subDomainBlock">
                    http://<input type="text" id="subDomain" onkeyup="javascript:domainChanged()" />.taskometer.com.<br />
                    <span class="small">You can also choose <a href="javascript:toggleBlock('customDomainBlock', 'subDomainBlock'); setCustomDomainType(true);">
                        your own domain</a> (like yourcompany.com).</span></div>
                <div id="customDomainBlock" style="display: none">
                    Your own domain:
                    <input type="text" id="customDomain" onkeyup="javascript:domainChanged()" />
                    <span class="small">eg: yourcompany.com</span><br />
                    <span class="small">Domain has to be purchased separately. Or <a href="javascript:toggleBlock('subDomainBlock', 'customDomainBlock'); setCustomDomainType(false);">
                        use a subdomain</a> for now.</span></div>
                <div class="applicationsBlock">
                    <h4>
                        Applications</h4>
                    <table class="appList">
                        <colgroup>
                            <col width="32px" />
                            <col width="220px" />
                            <col width="380px" />
                        </colgroup>
                        <tbody>
                            <tr>
                                <td style="width: 12px">
                                    <%= Html.CheckBox(Model, m => m.SetupWebsite) %>
                                </td>
                                <td>
                                    <h5>
                                        Website</h5>
                                    <p class="small">
                                        A content managed website for your company or product.</p>
                                </td>
                                <td id="websiteUrlSelector" class="urlSelector">
                                    <%= Html.Hidden(Model, m => m.WebsiteUrlType) %>
                                    <%= Html.Hidden(Model, m => m.WebsiteSubDirectory) %>
                                    <%= Html.Hidden(Model, m => m.WebsiteCustomDomain) %>
                                    <table class="urlDisplay">
                                        <tr>
                                            <td class="url">
                                                <span id="WebsiteEffectiveUrl"></span>
                                            </td>
                                            <td>
                                                <a id="WebsiteUrlEdit" href="javascript:editAppUrl('Website')">edit</a>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 12px">
                                    <%= Html.CheckBox(Model, m => m.SetupProjectManagement) %>
                                </td>
                                <td>
                                    <h5>
                                        Project Management</h5>
                                </td>
                                <td id="projectManagementUrlSelector" class="urlSelector">
                                    <%= Html.Hidden(Model, m => m.ProjectManagementUrlType) %>
                                    <%= Html.Hidden(Model, m => m.ProjectManagementSubDirectory) %>
                                    <%= Html.Hidden(Model, m => m.ProjectManagementCustomDomain) %>
                                    <table class="urlDisplay">
                                        <tr>
                                            <td class="url">
                                                <span id="ProjectManagementEffectiveUrl"></span>
                                            </td>
                                            <td>
                                                <a id="ProjectManagementUrlEdit" href="javascript:editAppUrl('ProjectManagement')">edit</a>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 12px">
                                    <%= Html.CheckBox(Model, m => m.SetupIssueTracker) %>
                                </td>
                                <td>
                                    <h5>
                                        Issue Tracking</h5>
                                </td>
                                <td id="issueTrackerUrlSelector" class="urlSelector">
                                    <%= Html.Hidden(Model, m => m.IssueTrackerUrlType) %>
                                    <%= Html.Hidden(Model, m => m.IssueTrackerSubDirectory) %>
                                    <%= Html.Hidden(Model, m => m.IssueTrackerCustomDomain) %>
                                    <table class="urlDisplay">
                                        <tr>
                                            <td class="url">
                                                <span id="IssueTrackerEffectiveUrl"></span>
                                            </td>
                                            <td>
                                                <a id="IssueTrackerUrlEdit" href="javascript:editAppUrl('IssueTracker')">edit</a>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 12px">
                                    <%= Html.CheckBox(Model, m => m.SetupWiki) %>
                                </td>
                                <td>
                                    <h5>
                                        Wiki</h5>
                                    <p class="small">
                                        Select this if your product needs a wiki.</p>
                                </td>
                                <td id="wikiUrlSelector" class="urlSelector">
                                    <%= Html.Hidden(Model, m => m.WikiUrlType) %>
                                    <%= Html.Hidden(Model, m => m.WikiSubDirectory) %>
                                    <%= Html.Hidden(Model, m => m.WikiCustomDomain) %>
                                    <table class="urlDisplay">
                                        <tr>
                                            <td class="url">
                                                <span id="WikiEffectiveUrl"></span>
                                            </td>
                                            <td>
                                                <a id="WikiUrlEdit" href="javascript:editAppUrl('Wiki')">edit</a>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 12px">
                                    <%= Html.CheckBox(Model, m => m.SetupBlog) %>
                                </td>
                                <td>
                                    <h5>
                                        Blog</h5>
                                    <p class="small">
                                        Your company or product blog.</p>
                                </td>
                                <td id="blogUrlSelector" class="urlSelector">
                                    <%= Html.Hidden(Model, m => m.BlogUrlType) %>
                                    <%= Html.Hidden(Model, m => m.BlogSubDirectory) %>
                                    <%= Html.Hidden(Model, m => m.BlogCustomDomain) %>
                                    <table class="urlDisplay">
                                        <tr>
                                            <td class="url">
                                                <span id="BlogEffectiveUrl"></span>
                                            </td>
                                            <td>
                                                <a id="BlogUrlEdit" href="javascript:editAppUrl('Blog')">edit</a>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 12px">
                                    <span class="small">Yes</span>
                                </td>
                                <td>
                                    <h5>
                                        Admin Website</h5>
                                    <p class="small">
                                        Manage your applications and users.<br />
                                        <em>This url is always http://domain/admin.</em></p>
                                </td>
                                <td id="adminUrlSelector" class="urlSelector">
                                    <%= Html.Hidden(Model, m => m.AdminUrlType) %>
                                    <%= Html.Hidden(Model, m => m.AdminSubDirectory) %>
                                    <%= Html.Hidden(Model, m => m.AdminCustomDomain) %>
                                    <table class="urlDisplay">
                                        <tr>
                                            <td class="url">
                                                <span id="AdminEffectiveUrl"></span>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <p class="small">
                        * You can setup more websites, wikis and blogs using the Admin Website.
                    </p>
                </div>
            </div>
            <h3>
                Terms and Conditions</h3>
            <p>
                <input id="chkAgree" type="checkbox" />I agree to the <a href="/terms">terms and conditions</a>.
            </p>
            <input type="button" class="submitButton" onclick="submitForm();" value="Create my account" />
            <% } %>
        </div>
        <div class="rightPane">
            <img src="/images/signupPage/30-day-trial.png" alt="30-day-trial" />
        </div>
    </div>
</asp:Content>
