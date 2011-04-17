using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using AgileFx.ORM.Utils;
using System.Collections;
using AgileFx.ORM.Mapping;
using AgileFx.ORM.Types;
using IQToolkit;

namespace AgileFx.ORM.QueryAnalysis.TypeTracking
{
    public class NonTranslatableType : SimpleType
    {
        public Dictionary<MemberInfo, MemberInfo> MemberMap { get; protected set; }

        public NonTranslatableType(Type originalType, Type translatedType, ConstructorInfo constructor, 
            IEnumerable<KeyValuePair<MemberInfo, SimpleType>> fields)
            : base(originalType, translatedType, constructor, null, fields)
        {
            MemberMap = new Dictionary<MemberInfo, MemberInfo>();
        }

        public override string GetMemberName(MemberInfo memInfo)
        {
            return MemberMap[memInfo].Name;
        }

        public override Func<object, object> GetTranslatedMemberValue(MemberInfo memInfo, TypeTranslationUtil typeTranslationUtil)
        {
            return base.GetTranslatedMemberValue(MemberMap[memInfo], typeTranslationUtil);
        }
    }
}
