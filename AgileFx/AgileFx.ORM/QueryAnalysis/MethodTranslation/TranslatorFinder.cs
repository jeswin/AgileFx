using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;

namespace AgileFx.ORM.QueryAnalysis.MethodTranslation
{
    public class TranslatorFinder
    {
        static Dictionary<MethodInfo, MethodTranslator> translators = new Dictionary<MethodInfo, MethodTranslator>();
        
        static TranslatorFinder()
        {
            var translatorLists = new List<MethodTranslatorList>();

            translatorLists.Add(new All<object>());
            translatorLists.Add(new Any<object>());
            translatorLists.Add(new Average<object>());
            translatorLists.Add(new Concat<object>());
            translatorLists.Add(new Contains<object>());
            translatorLists.Add(new Count<object>());
            translatorLists.Add(new Distinct<object>());
            translatorLists.Add(new ElementAt<object>());
            translatorLists.Add(new Except<object>());
            translatorLists.Add(new First<object>());
            translatorLists.Add(new GroupBy1<object, object>());
            translatorLists.Add(new GroupBy2<object, object, object>());
            translatorLists.Add(new GroupBy3<object, object, object>());
            translatorLists.Add(new GroupBy4<object, object, object, object>());
            translatorLists.Add(new GroupJoin<object, object, object, object>());
            translatorLists.Add(new Intersect<object>());
            translatorLists.Add(new Join<object, object, object, object>());
            translatorLists.Add(new Last<object>());
            translatorLists.Add(new LoadRelated());
            translatorLists.Add(new LongCount<object>());
            translatorLists.Add(new OrderBy<object, object>());
            translatorLists.Add(new Reverse<object>());
            translatorLists.Add(new Select1<object, object>());
            translatorLists.Add(new Select2<object, object>());
            translatorLists.Add(new SelectMany1<object, object>());
            translatorLists.Add(new SelectMany2<object, object>());
            translatorLists.Add(new SelectMany3<object, object, object>());
            translatorLists.Add(new SelectMany4<object, object, object>());
            translatorLists.Add(new SequenceEqual<object>());
            translatorLists.Add(new Single<object>());
            translatorLists.Add(new Skip<object>());
            translatorLists.Add(new Sum<object>());
            translatorLists.Add(new Take<object>());
            translatorLists.Add(new ThenBy<object, object>());
            translatorLists.Add(new Union<object>());
            translatorLists.Add(new Where<object, object>());
            translatorLists.Add(new CreateQuery<object>());

            foreach (var list in translatorLists)
            {
                foreach (var item in list.GetTranslators())
                {
                    var method = GetMethodKey(item.Key);
                    if (!translators.ContainsKey(method))
                        translators[method] = item.Value;
                }
            }
        }

        public static MethodTranslator GetMethodTranslator(MethodInfo methodInfo)
        {
            var methodKey = GetMethodKey(methodInfo);
            if (translators.ContainsKey(methodKey))
                return translators[methodKey];
            else
                return null;
        }

        private static MethodInfo GetMethodKey(MethodInfo methodInfo)
        {
            return methodInfo.IsGenericMethod ? methodInfo.GetGenericMethodDefinition() : methodInfo;
        }
    }
}
