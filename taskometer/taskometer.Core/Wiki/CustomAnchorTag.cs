using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using WikiPlex;
using WikiPlex.Compilation;
using WikiPlex.Compilation.Macros;
using WikiPlex.Formatting;

namespace taskometer.Core.Wiki
{
    public class CustomAnchorTagScopeName
    {
        public const string HrefLocation = "Wiki Href Location";
    }

    public class CustomAnchorTagMacro : IMacro
    {
        public string Id
        {
            get { return "Custom Anchor Tag"; }
        }

        public IList<WikiPlex.Compilation.MacroRule> Rules
        {
            get
            {
                return new List<MacroRule>
                           {
                               new MacroRule(EscapeRegexPatterns.CurlyBraceEscape),
                               new MacroRule(@"(?i)(\[url:)((?>[^\]]+))(\])",
                                             new Dictionary<int, string>
                                                 {
                                                     {1, ScopeName.Remove},
                                                     {2, CustomAnchorTagScopeName.HrefLocation},
                                                     {3, ScopeName.Remove}
                                                 }
                                   )
                           };
            }
        }
    }

    public class CustomAnchorTagRenderer : IRenderer
    {
        private const string AnchorTagFormat = "<a href=\"{0}\">{1}</a>";

        public string Id
        {
            get { return "Custom Anchor Tag Renderer"; }
        }

        public bool CanExpand(string scopeName)
        {
            return scopeName == CustomAnchorTagScopeName.HrefLocation;
        }

        public string Expand(string scopeName, string input, Func<string, string> htmlEncode, Func<string, string> attributeEncode)
        {
            var splitterPos = input.IndexOf('|');
            if (splitterPos > -1)
                return string.Format(AnchorTagFormat, attributeEncode(input.Substring(splitterPos + 1)), attributeEncode(input.Substring(0, splitterPos)));
            else
                return string.Format(AnchorTagFormat, attributeEncode(input), attributeEncode(input));
        }
    }
}
