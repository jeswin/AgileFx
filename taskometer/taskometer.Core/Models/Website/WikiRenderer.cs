using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WikiPlex;
using WikiPlex.Formatting;

namespace taskometer.Core.Models
{
    public static class WikiRenderer
    {
        public static string Render(string source)
        {
            var wikiEngine = new WikiEngine();
            return wikiEngine.Render(source, new MacroFormatter(Renderers.All));
        }
    }
}
