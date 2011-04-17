using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace AgileFx.ORM.QueryAnalysis.MethodTranslation
{
    public abstract class MethodTranslatorList
    {
        Dictionary<MethodInfo, MethodTranslator> translators = new Dictionary<MethodInfo, MethodTranslator>();

        public void AddTranslator(MethodInfo methodInfo, MethodTranslator translator)
        {
            translators[methodInfo] = translator;
        }

        public Dictionary<MethodInfo, MethodTranslator> GetTranslators()
        {
            return translators;
        }
    }
}
