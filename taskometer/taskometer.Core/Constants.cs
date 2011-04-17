using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace taskometer.Core
{
    public struct ROLES
    {
        public const string USER = "user";
        public const string ADMINISTRATOR = "administrator";
    }

    public struct ACCOUNT_STATUS
    {
        public const string ACTIVE = "active";
        public const string DISABLED = "disabled";
    }

    public struct WEBSITE_TYPE
    {
        public const string CMS = "cms";
        public const string WIKI = "wiki";
        public const string BLOG = "blog";
    }

    public struct PAGE_CONTENT_TYPE
    {
        public const string HTML = "html";
        public const string WIKI = "wiki";
    }

    public struct TEMPLATE_PLACEHOLDER_TYPE
    {
        public const string SINGLE_VALUE = "single-value";
        public const string MULTI_LINE = "multi-line";
        public const string LIST = "list";
    }
}
