using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace AgileFx.ORM.QueryAnalysis.TypeTracking
{
    public static class RuntimeTypes
    {
        public static SimpleType CreateEnumerable(SimpleType itemType)
        {
            var type = typeof(IEnumerable<>).MakeGenericType(itemType.Type);
            var translatedType = typeof(IEnumerable<>).MakeGenericType(itemType.TranslatedType);
            return new SimpleType(type, translatedType, itemType);
        }

        public static SimpleType CreateGrouping(SimpleType key, SimpleType itemType)
        {
            var type = typeof(IGrouping<,>).MakeGenericType(key.Type, itemType.Type);
            var translatedType = typeof(IGrouping<,>).MakeGenericType(key.TranslatedType, itemType.TranslatedType);

            Dictionary<MemberInfo, SimpleType> fields = new Dictionary<MemberInfo,SimpleType>();
            fields.Add(type.GetProperty("Key"), key);

            var ctor = typeof(AgileFx.ORM.Types.Grouping<,>).MakeGenericType(key.Type, itemType.Type).GetConstructor(Type.EmptyTypes);
            return new SimpleType(type, translatedType, ctor, null, fields, itemType);
        }
    }
}
