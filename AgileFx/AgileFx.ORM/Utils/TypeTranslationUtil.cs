/* AgileFx Version 2.0
 * An open-source framework for rapid development of applications and services using Microsoft.net
 * Developed by: AgileHead
 * Website: www.agilefx.org
 * This component is licensed under the terms of the Apache 2.0 License.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

using IQToolkit;
using AgileFx.ORM.Mapping;
using AgileFx.ORM.Materialization;
using AgileFx.ORM.ObjectComposition;

namespace AgileFx.ORM.Utils
{
    public class ClassMetadata
    {
        public object ClassMapping { get; set; }
        public ITypeFactory TypeFactory { get; set; }
    }

    public static class ClassMetadataCache
    {
        static object accessLock = new object();
        static Dictionary<Type, ClassMetadata> cache = new Dictionary<Type, ClassMetadata>();

        public static ClassMetadata GetCacheEntry(Type type)
        {
            lock (accessLock)
            {
                return cache.ContainsKey(type) ? cache[type] : null;
            }
        }

        public static void Add(Type type, ClassMetadata entry)
        {
            lock (accessLock)
            {
                cache[type] = entry;
            }
        }
    }

    public class TypeTranslationUtil
    {
        public T GetMapping<T>(Type entityType) where T : class
        {
            return GetMetadata(entityType).ClassMapping as T;
        }

        public ITypeFactory GetTypeFactory(Type entityType)
        {
            return GetMetadata(entityType).TypeFactory;
        }

        private ClassMetadata GetMetadata(Type entityType)
        {
            var classMetadata = ClassMetadataCache.GetCacheEntry(entityType);
            return classMetadata != null ? classMetadata : LoadMetadataFromType(entityType);
        }

        private ClassMetadata LoadMetadataFromType(Type entityType)
        {
            var classMappingAttr = entityType.GetCustomAttributes(typeof(ClassMappingAttribute), false).SingleOrDefault();
            var typeFactoryAttr = entityType.GetCustomAttributes(typeof(TypeFactoryAttribute), false).SingleOrDefault();

            var classMetadata = new ClassMetadata();

            if (classMappingAttr != null)
            {
                classMetadata.ClassMapping = Activator.CreateInstance((classMappingAttr as ClassMappingAttribute).Value);

                if (typeFactoryAttr != null)
                    classMetadata.TypeFactory = Activator.CreateInstance((typeFactoryAttr as TypeFactoryAttribute).FactoryType) as ITypeFactory;

                ClassMetadataCache.Add(entityType, classMetadata);
            }

            return classMetadata;
        }

        public virtual Type GetTranslatedType(Type t)
        {
            if (TypesUtil.IsCompilerGeneratedAnonymousType(t))
            {
                var typeArgs = t.GetGenericArguments().Select(x => GetTranslatedType(x)).ToArray();
                return t.GetGenericTypeDefinition().MakeGenericType(typeArgs);
            }
            else
            {
                if (HasCorrespondingTableEntityType(t))
                    return GetTableEntityType(t);
                else
                    return null;
            }
        }

        public virtual MethodCallExpression GetTranslatedGenericMethodCall(Expression instance, MethodInfo method,
            IEnumerable<Type> translatedGenericArgs, IEnumerable<Expression> translatedArgs)
        {
            var mInfo = method.GetGenericMethodDefinition().MakeGenericMethod(translatedGenericArgs.ToArray());
            return Expression.Call(instance, mInfo, translatedArgs);
        }

        public Type GetAnonymousType(IEnumerable<Type> types)
        {
            var _types = types.ToList();
            var genericType = AnonymousType.GetGenericType(_types.Count);
            return genericType.MakeGenericType(_types.ToArray());
        }

        private Type GetAnonymousTranslatedType(Type type)
        {
            //If type "TypeA" has ctor(type1 t1) & ctor(type1 t1, type2 t2) && public fields type1 f1, type2 f2, type3 f3
            //  then our "AnonymousType" be of type AnonymousType<type1, type1, type2, type1, type2, type3>
            //  ie, types of all constructors (included separately) plus those of the public fields.
            List<Type> anonyTypeGenericParams = new List<Type>();

            foreach (var ctor in type.GetConstructors())
                anonyTypeGenericParams.AddRange(ctor.GetParameters().Select(p => GetTranslatedType(p.ParameterType)));

            foreach (var prop in type.GetProperties())
                anonyTypeGenericParams.Add(GetTranslatedType(prop.PropertyType));

            var genericType = AnonymousType.GetGenericType(anonyTypeGenericParams.Count);
            return genericType.MakeGenericType(anonyTypeGenericParams.ToArray());
        }

        public bool IsEntity(Type type)
        {
            return GetMapping<IEntityMapping>(type) != null;
        }

        public bool IsEntityCollection(Type t)
        {
            //?Todo: Also check if the generic type of the collection is IEntity
            if (t.IsGenericType && (typeof(ICollection<>).IsAssignableFrom(t.GetGenericTypeDefinition())))
            {
                if (IsEntity(TypesUtil.GetGenericArgumentForBaseType(t, typeof(ICollection<>))))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        protected virtual bool HasCorrespondingTableEntityType(Type type)
        {
            if (TypesUtil.IsPrimitiveDataType(type))
                return true;

            return IsEntity(type);
        }

        private Type GetTableEntityType(Type entityType)
        {
            var classMapping = GetMapping<IEntityMapping>(entityType);
            return classMapping != null ? classMapping.TableEntityType : entityType;
        }

        public MemberExpression GetTranslatedMemberExpression(Type entityType, string memberName, Expression innerExpression)
        {
            return GetManyToManyMapExpression(entityType, memberName, innerExpression) ??
                       GetMemberExpression(entityType, memberName, innerExpression);
        }

        private MemberExpression GetManyToManyMapExpression(Type entityType, string memberName, Expression innerExpression)
        {
            var classMapping = GetMapping<IModelEntityMapping>(entityType);
            if (classMapping == null) return null;

            //Getting the many-to-many maps for the declaringType
            var member = classMapping.GetManyToManyRelationships()
                .Where(m => ((MemberExpression)m.RelatedEntitySelector.Body).Member.Name == memberName)
                .Select(m => (GetCollectionSelector(m).Body as MemberExpression).Member).FirstOrDefault();

            if (member != null) return GetMemberExpression(entityType, member.Name, innerExpression);

            //Need to check if the map is from base type
            if (classMapping is IDerivedModelEntityMapping)
            {
                var baseEfProperty = innerExpression.Type.GetProperty((GetMapping<IModelEntityMapping>(entityType) as IDerivedModelEntityMapping).BaseClassProperty);
                return GetManyToManyMapExpression(entityType.BaseType, memberName, Expression.MakeMemberAccess(innerExpression, baseEfProperty));
            }
            return null;
        }

        public MemberExpression GetMemberExpression(Type entityType, string memberName, Expression innerExpression)
        {
            var memInfo = innerExpression.Type.GetMember(memberName).SingleOrDefault();
            if (null != memInfo) return Expression.MakeMemberAccess(innerExpression, memInfo);
            else
            {
                var baseTableEntityProp = innerExpression.Type.GetProperty((GetMapping<IModelEntityMapping>(entityType) as IDerivedModelEntityMapping).BaseClassProperty);
                return GetMemberExpression(entityType.BaseType, memberName, Expression.MakeMemberAccess(innerExpression, baseTableEntityProp));
            }
        }

        public LambdaExpression GetCollectionSelector(ManyToManyRelationship relationship)
        {
            var memInfo = ((MemberExpression)relationship.SelfSelectorFromMap.Body).Member;
            return GetMapping<IMapEntityMapping>(memInfo.DeclaringType).GetManyToManyMaps()
                .Where(m => ((MemberExpression)m.EntitySelector.Body).Member.Name == memInfo.Name)
                .Select(m => m.MapTableEntitySelector).FirstOrDefault();
        }

        public virtual IEnumerable<PropertyInfo> GetIdentityProperties(Type instanceType)
        {
            var keyProperties = GetMapping<IEntityMapping>(instanceType)
                .GetIdentityFields().OfType<LambdaExpression>()
                .Select(exp => ExpressionUtil.GetProperty(exp, instanceType)).ToList();

            return keyProperties;
        }

        public Func<object, IIntermediateEntityContainer> MakeBackingContainerGetter(Type entityType, MemberInfo property)
        {
            if (entityType.GetProperty(property.Name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public) != null)
            {
                var intermediateEntityType = GetMapping<IEntityMapping>(entityType).IntermediateEntityType;
                var member = TypesUtil.GetIntermediateContainer(intermediateEntityType, property.Name);
                return tableEntity => member.GetValue(tableEntity) as IIntermediateEntityContainer;
            }
            var derivedMapping = GetMapping<IDerivedModelEntityMapping>(entityType);
            if (derivedMapping != null)
            {
                var baseProperty = derivedMapping.IntermediateEntityType.GetProperty("_base");
                var baseTypeMemberGetter = MakeBackingContainerGetter(entityType.BaseType, property);
                return tableEntity => baseTypeMemberGetter(baseProperty.GetValue(tableEntity));
            }
            else
            {
                throw new InvalidOperationException("Member does not exist in the inheritance chain.");
            }
        }
    }
}
