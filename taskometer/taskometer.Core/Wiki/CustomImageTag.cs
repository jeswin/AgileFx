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
    public class CustomImageTagScopeName
    {
        public const string ImageSource = "Wiki Image Source";
    }

    public class CustomImageTagMacro : IMacro
    {
        public string Id
        {
            get { return "Custom Image Tag"; }
        }

        public IList<WikiPlex.Compilation.MacroRule> Rules
        {
            get
            {
                return new List<MacroRule>
                           {
                               new MacroRule(EscapeRegexPatterns.CurlyBraceEscape),
                               new MacroRule(@"(?i)(\[image:)((?>[^\]]+))(\])",
                                             new Dictionary<int, string>
                                                 {
                                                     {1, ScopeName.Remove},
                                                     {2, CustomImageTagScopeName.ImageSource},
                                                     {3, ScopeName.Remove}
                                                 }
                                   )
                           };
            }
        }
    }

    public class CustomImageTagRenderer : IRenderer
    {
        private const string ImageTagFormat = "<img title=\"{0}\" alt=\"{0}\" src=\"{1}\" />";
        private const string SimpleImageTagFormat = "<img src=\"{0}\" />";

        public string Id
        {
            get { return "Custom Image Tag Renderer"; }
        }

        public bool CanExpand(string scopeName)
        {
            return scopeName == CustomImageTagScopeName.ImageSource;
        }

        public string Expand(string scopeName, string input, Func<string, string> htmlEncode, Func<string, string> attributeEncode)
        {
            var splitterPos = input.IndexOf('|');
            if (splitterPos > -1)
                return string.Format(ImageTagFormat, attributeEncode(input.Substring(0, splitterPos)), attributeEncode(input.Substring(splitterPos + 1)));
            else
                return string.Format(ImageTagFormat, attributeEncode(input));
        }
    }
}
