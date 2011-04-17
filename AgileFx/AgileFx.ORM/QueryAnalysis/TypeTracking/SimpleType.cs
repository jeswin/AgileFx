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
    //This is the simplest type, which in a query is represented by Parameter or Member access.
    //eg: from user in context.Users select user
    //  user is represented as a SimpleType.
    //eg2: from user in context.Users select user.FirstName
    //  This gives a SimpleType of Type "string".
    public class SimpleType
    {
        public Type Type { get; protected set; }

        public Type TranslatedType { get; protected set; }

        //The list of includes defined on this type.
        //eg: (from user in context.Users select user).LoadRelated(user => user.Account)
        //  user.Account is added as an Include.
        public virtual List<IncludeDirective> Includes { get; protected set; }

        //These two are optional. You will not have them for parameter or member-access expressions.
        public ConstructorInfo Constructor { get; protected set; }
        public List<SimpleType> ConstructorArgs { get; protected set; }

        //Fields are optional. You will not have them for parameter or member-access expressions.
        public virtual Dictionary<MemberInfo, SimpleType> Fields { get; protected set; }

        //If this type is enumerable, this will hold the item type.
        public SimpleType NonPrimitiveEnumerableItemType { get; private set; }

        //This should only be used when using primitive types, like int, string, long etc.
        public SimpleType(Type primitiveType)
            : this(primitiveType, primitiveType)
        {
        }

        public SimpleType(Type originalType, Type translatedType)
        {
            this.Type = originalType;
            this.TranslatedType = translatedType;
            this.Includes = new List<IncludeDirective>();
            this.ConstructorArgs = new List<SimpleType>();
            this.Fields = new Dictionary<MemberInfo, SimpleType>();
        }

        public SimpleType(Type originalType, Type translatedType, SimpleType enumerableItemType)
            : this(originalType, translatedType)
        {
            this.NonPrimitiveEnumerableItemType = enumerableItemType;
        }

        public SimpleType(Type originalType, Type translatedType, SimpleType enumerableItemType, IEnumerable<KeyValuePair<MemberInfo, SimpleType>> fields)
            : this(originalType, translatedType, enumerableItemType)
        {
            //Null is an allowed value, if there are no fields to add.
            if (fields != null)
                foreach (var field in fields)
                    this.Fields.Add(field.Key, field.Value);
        }

        public SimpleType(Type originalType, Type translatedType, ConstructorInfo constructor,
            IEnumerable<SimpleType> constructorArgs, IEnumerable<KeyValuePair<MemberInfo, SimpleType>> fields)
            : this(originalType, translatedType)
        {
            this.Constructor = constructor;

            this.ConstructorArgs = new List<SimpleType>();

            //Null is an allowed value, if the constructor is empty.
            if (constructorArgs != null)
                this.ConstructorArgs.AddRange(constructorArgs);

            this.Fields = new Dictionary<MemberInfo, SimpleType>();

            //Null is an allowed value, if there are no fields to add.
            if (fields != null)
                foreach (var field in fields)
                    this.Fields.Add(field.Key, field.Value);
        }

        public SimpleType(Type originalType, Type translatedType, ConstructorInfo constructor, IEnumerable<SimpleType> constructorArgs,
            IEnumerable<KeyValuePair<MemberInfo, SimpleType>> fields, SimpleType enumerableItemType)
            : this(originalType, translatedType, constructor, constructorArgs, fields)
        {
            this.NonPrimitiveEnumerableItemType = enumerableItemType;
        }

        protected ParameterExpression parameter = null;

        //Gets a parameter which can represent this type in the Translated Expression.
        //In the case of SimpleType this is just a Parameter of type 'TranslatedType'
        public ParameterExpression GetParameter(string parameterName)
        {
            return Expression.Parameter(TranslatedType, parameterName);
        }

        //Gets a parameter which can represent this type in the Translated Expression.
        //In the case of SimpleType this is just a Parameter of type 'TranslatedType'
        public virtual Expression GetTranslatedParameterOrMember(ParameterExpression param)
        {
            return param;
        }

        //SimpleType is an unmodified type. Hence should return 'this'.
        //Note: This value may only change in ProjectedType, where it becomes the
        //  QueryableType associated with Field0.
        public virtual SimpleType GetOriginalQueryableType()
        {
            return this;
        }

        public virtual string GetMemberName(MemberInfo memInfo)
        {
            return memInfo.Name;
        }

        public Dictionary<MemberInfo, SimpleType> GetAllMembers()
        {
            if (TypesUtil.IsCompilerGeneratedAnonymousType(Type))
            {
                var properties = Type.GetProperties();
                var dic = new Dictionary<MemberInfo, SimpleType>();
                for (int i = 0; i < properties.Length; i++)
                    dic.Add(properties[i], ConstructorArgs[i]);
                return dic;
            }
            else
            {
                return Fields;
            }
        }

        protected SimpleType GetMemberType(MemberExpression memberExp)
        {
            var allMembers = GetAllMembers();
            if (allMembers.ContainsKey(memberExp.Member))
                return allMembers[memberExp.Member];
            else
                return null;
        }

        protected virtual List<IncludeDirective> GetMemberIncludes(MemberExpression memberExp)
        {
            var newIncludes = new List<IncludeDirective>();
            var memberIsNotCollection = !TypesUtil.IsNonPrimitiveCollection(memberExp.Type);
            Includes.ForEach(inc =>
            {
                var includeExpression = inc.GetSelector().Body as MemberExpression;
                if (memberIsNotCollection)
                {
                    var innermostMember = ExpressionUtil.GetInnermostMemberExpression(includeExpression);
                    if (innermostMember.Member == memberExp.Member && includeExpression.Expression is MemberExpression)
                    {
                        var slicedExpression = ExpressionUtil.ReplaceInnermostMemberExpressionWithParameter(includeExpression) as MemberExpression;

                        if (inc is IIncludeInCollectionDirective)
                            newIncludes.Add(IncludeDirectiveUtil.GetIncludeInCollectionDirective(slicedExpression,
                                (inc as IIncludeInCollectionDirective).GetFieldSelector()));
                        else
                            newIncludes.Add(IncludeDirectiveUtil.GetIncludeDirective(slicedExpression));
                    }
                }
                else
                {
                    if (inc is IIncludeInCollectionDirective && ExpressionUtil.IsEquivalent(inc.GetSelector().Body, memberExp))
                    {
                        newIncludes.Add(IncludeDirectiveUtil.GetIncludeDirective
                            ((inc as IIncludeInCollectionDirective).GetFieldSelector()));
                    }
                }
            });
            return newIncludes;
        }

        public static SimpleType GetMember(SimpleType source, MemberExpression memberExp, MemberExpression translatedMemberExp, TypeTranslationUtil typeTranslationUtil)
        {
            SimpleType result;

            if (source != null)
            {
                result = source.GetMemberType(memberExp);
                if (result != null) return result;
            }

            var includes = (source != null) ? source.GetMemberIncludes(memberExp) : new List<IncludeDirective>();
            var memberIsCollection = TypesUtil.IsNonPrimitiveCollection(memberExp.Type);

            var classMapping = typeTranslationUtil.GetMapping<IModelEntityMapping>(memberExp.Member.DeclaringType);
            if (memberIsCollection && classMapping != null)
            {
                //Getting the many-to-many maps for the declaringType
                var manyToManyRelationship = classMapping.GetManyToManyRelationships()
                    .FirstOrDefault(m => ((MemberExpression)m.RelatedEntitySelector.Body).Member.Name == memberExp.Member.Name);
                if (manyToManyRelationship != null)
                {
                    var otherEndSelector = manyToManyRelationship.RelatedEntitySelectorFromMap.Body as MemberExpression;
                    var param = Expression.Parameter(typeTranslationUtil.GetTranslatedType(manyToManyRelationship.MapType), "x");
                    var translatedOtherEndSelector = typeTranslationUtil.GetMemberExpression
                        (manyToManyRelationship.MapType, otherEndSelector.Member.Name, param);
                    var map = new ManyToManyMapType(manyToManyRelationship.MapType, param.Type,
                        TypesUtil.GetGenericArgumentForBaseType(memberExp.Type, typeof(ICollection<>)), 
                            manyToManyRelationship, translatedOtherEndSelector.Member)  { Includes = includes };
                    return new SimpleType(memberExp.Type, translatedMemberExp.Type) { NonPrimitiveEnumerableItemType = map };
                }
            }

            result = new SimpleType(memberExp.Type, translatedMemberExp.Type);
            if (memberIsCollection)
            {
                var itemType = TypesUtil.GetGenericArgumentForBaseType(memberExp.Type, typeof(IEnumerable<>));
                var translatedItemType = TypesUtil.GetGenericArgumentForBaseType(translatedMemberExp.Type, typeof(IEnumerable<>));
                var innerType = new SimpleType(itemType, translatedItemType) { Includes = includes };
                result.NonPrimitiveEnumerableItemType = innerType;
            }
            else
            {
                result.Includes = includes;
            }
            return result;
        }

        public virtual Func<object, object> GetTranslatedMemberValue(MemberInfo memInfo, TypeTranslationUtil typeTranslationUtil)
        {
            return GetTranslatedMemberValue(this.Type, this.TranslatedType, memInfo.Name, typeTranslationUtil);
        }

        public Func<object, object> GetTranslatedMemberValue(Type entityType, Type tableEntityType, string memberName
            , TypeTranslationUtil typeTranslationUtil)
        {
            var member = tableEntityType.GetProperty(memberName);
            if (member != null)
                return entity =>
                {
                    var result = member.GetValue(entity);
                    return result;
                };
            else
            {
                var baseTableEntityProp = tableEntityType.GetProperty((typeTranslationUtil.GetMapping<IModelEntityMapping>(entityType) as IDerivedModelEntityMapping).BaseClassProperty);
                var basePropertyGetter = GetTranslatedMemberValue(entityType.BaseType, baseTableEntityProp.PropertyType, memberName, typeTranslationUtil);
                return entity =>
                {
                    var result = baseTableEntityProp.GetValue(basePropertyGetter(entity));
                    return result;
                };
            }
        }
    }
}
