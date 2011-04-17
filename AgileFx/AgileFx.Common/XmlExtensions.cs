/* AgileFx Version 2.0
 * An open-source framework for rapid development of applications and services using Microsoft.net
 * Developed by: AgileHead
 * Website: www.agilefx.org
 * This component is licensed under the terms of the Apache 2.0 License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace AgileFx
{
    public static class XmlExtensions
    {
        public static string ToXml<T>(this T obj) where T : class
        {
            if (null == obj) return null;
            using (StringWriter writer = new StringWriter())
            {
                new XmlSerializer(typeof(T)).Serialize(writer, obj);
                return writer.ToString();
            }
        }

        public static T GetObjectFromXml<T>(this string xml) where T : class
        {
            if (string.IsNullOrEmpty(xml)) return null;
            using (StringReader reader = new StringReader(xml))
            {
                return (new XmlSerializer(typeof(T))).Deserialize(reader) as T;
            }
        }
    }
}
