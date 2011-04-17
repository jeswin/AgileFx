/* AgileFx Version 2.0
 * An open-source framework for rapid development of applications and services using Microsoft.net
 * Developed by: AgileHead
 * Website: www.agilefx.org
 * This component is licensed under the terms of the Apache 2.0 License.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AgileFx.ORM.Utils
{
    public static class TypesUtil
    {
        public static Type GetGenericArgumentForBaseType(Type finalType, Type specificBaseType)
        {
            //The objective is to get T, where FinalType<X,Y,Z> : BaseType<T>
            //  Note that is more commonly of the form: FinalType<T> : BaseType<T>
            
            //? TODO: This method should except when FinalType<X,Y,Z> : BaseType<T1>, BaseType<T2>
            //  since we wouldn't know whether we should return T1 or T2.

            if (!specificBaseType.IsGenericType)
                throw new ArgumentException("The matchedType parameter should be a generic type.");

            var typeTree = GetInheritanceAndImplementationsTree(finalType);

            Func<Type, bool> isMatching = i => i.IsGenericType && i.GetGenericTypeDefinition() == specificBaseType;

            var matchingBaseType = typeTree.SingleOrDefault(i => isMatching(i));
            if (matchingBaseType != null)
                //There should be only one MatchingBaseType, else Except.
                return matchingBaseType.GetGenericArguments().SingleOrDefault();
            else
                //finalType does not inherit or implement specificBaseType at any level.
                return null;
        }

        public static bool GenericTypeIsAssignableFrom(Type derivedType, Type baseType)
        {
            if (derivedType.IsGenericType && derivedType.IsInterface && derivedType.GetGenericTypeDefinition() == baseType)
                return true;
            else
                return derivedType.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == baseType); 
        }

        public static bool IsNonPrimitiveCollection(Type type)
        {
            return !IsPrimitiveDataType(type) && IsEnumerable(type);
        }

        private static IEnumerable<Type> GetInheritanceAndImplementationsTree(Type type)
        {
            var baseTypesAndInterfaces = new List<Type>();
            baseTypesAndInterfaces.AddRange(type.GetInterfaces());

            var addBaseTypes = F.Y<Type>(f => _currentType =>
            {
                baseTypesAndInterfaces.Add(_currentType);
                if (_currentType.BaseType != null)
                    f(_currentType.BaseType);
            });

            //Add the base classes
            addBaseTypes(type);

            return baseTypesAndInterfaces;
        }

        //To be used internally only
        private static bool IsEnumerable(Type type)
        {
            return GetGenericArgumentForBaseType(type, typeof(IEnumerable<>)) != null;
        }

        public static bool IsCompilerGeneratedAnonymousType(Type t)
        {
            CompilerGeneratedAttribute attrib = (CompilerGeneratedAttribute)Attribute.GetCustomAttribute(
            t, typeof(CompilerGeneratedAttribute));

            if (attrib != null && t.Name.Contains("AnonymousType"))
                return true;
            else
                return false;
        }

        //All values types, strings, and Nullable value types, or even List of the previously listed types
        public static bool IsPrimitiveDataType(Type t)
        {
            return  //Value types
                    typeof(ValueType).IsAssignableFrom(t)

                    //strings
                    || t == typeof(string)

                    //Nullable<value-type>
                    || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>)
                        && IsPrimitiveDataType(t.GetGenericArguments().Single()))

                    || (t.IsGenericType
                        && IsEnumerable(t)
                        && IsPrimitiveDataType(GetGenericArgumentForBaseType(t, typeof(IEnumerable<>))));

        }

        public static Type GetMemberType(MemberInfo memberInfo)
        {
            return (memberInfo is PropertyInfo) ? ((PropertyInfo)memberInfo).PropertyType
                : ((memberInfo is FieldInfo) ? ((FieldInfo)memberInfo).FieldType : null);
        }

        public static MemberInfo GetIntermediateContainer(Type intermediateEntityType, string memberName)
        {
            Func<string, string> toCamelCase = str => 
                !string.IsNullOrEmpty(str) ? str.Substring(0, 1).ToLower() + str.Substring(1) : str;
        
            return intermediateEntityType.GetMember("_" + toCamelCase(memberName)).Single();
        }
    }
}
