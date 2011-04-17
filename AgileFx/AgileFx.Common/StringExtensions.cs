/* AgileFx Version 2.0
 * An open-source framework for rapid development of applications and services using Microsoft.net
 * Developed by: AgileHead
 * Website: www.agilefx.org
 * This component is licensed under the terms of the Apache 2.0 License.
 */

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace AgileFx
{
    public static class StringExtensions
    {
        public static string GetCSV<T>(this IEnumerable<T> list) where T : IConvertible
        {
            return string.Join(",", list.Select(x => x.ToString()).ToArray());
        }

        public static List<T> GetListFromCSV<T>(this string csv) where T : IConvertible
        {
            if (string.IsNullOrEmpty(csv)) return new List<T>();
            return csv.Split(',').Select(x => (T)Convert.ChangeType(x, typeof(T))).ToList();
        }

        public static string Format(this string str, params object[] args)
        {
            return string.Format(str, args);
        }

        public static string Format(this string str, Dictionary<string, object> args)
        {
            var rex = new Regex("{.*?}");
            var groups = new List<string>();
            foreach (Match x in rex.Matches(str))
            {
                var strippedVal = x.Value.Trim('{', '}');
                if (!groups.Contains(strippedVal))
                    groups.Add(strippedVal);
            }
            var values = new List<object>();
            for (int i = 0; i < groups.Count; i++)
            {
                str = str.Replace("{" + groups[i] + "}", "{" + i + "}");
                values.Add(args.ContainsKey(groups[i]) ? args[groups[i]] : string.Empty);
            }
            return string.Format(str, values.ToArray());
            //return string.Format(str, args);
        }

        public static bool Empty(this string x)
        {
            if (x == null || x == "")
                return true;
            else
                return false;
        }
    }
}
