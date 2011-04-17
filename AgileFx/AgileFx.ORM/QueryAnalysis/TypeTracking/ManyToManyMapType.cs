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
    public class ManyToManyMapType : SimpleType
    {
        public Type MappedItemType { get; protected set; }
        public ManyToManyRelationship Relationship { get; protected set; }
        public MemberInfo Mapping { get; protected set; }

        public ManyToManyMapType(Type originalType, 
                                Type translatedType, 
                                Type mappedItemType,
                                ManyToManyRelationship relationship,
                                MemberInfo mapping)
                : base(originalType, translatedType)
        {
            this.MappedItemType = mappedItemType;
            this.Relationship = relationship;
            this.Mapping = mapping;
        }

        //Gets a parameter which can represent this type in the Translated Expression.
        //Since this is a ManyToMany Map, the member corresponding to the original query is the reference on the map.
        //eg: from user in context.Users select user.Languages ; where user <-> languages is a many-to-many relationship.
        //  Translation will convert it to user.UserLanguageMaps (which is IEnumerable<UserLanguageMap>)
        //  But when we need the actual object, refer to map.Language.
        public override Expression GetTranslatedParameterOrMember(ParameterExpression param)
        {
            return Expression.MakeMemberAccess(param, Mapping);
        }

        public override SimpleType GetOriginalQueryableType()
        {
            var memberExp = GetTranslatedParameterOrMember(GetParameter("x"));
            var mappedItem = new SimpleType(MappedItemType, memberExp.Type);
            mappedItem.Includes.AddRange(Includes);
            return mappedItem;
        }
    }
}
