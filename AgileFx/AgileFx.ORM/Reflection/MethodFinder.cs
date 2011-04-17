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
using System.Reflection;
using AgileFx.ORM.ObjectComposition;
using System.Linq.Expressions;

namespace AgileFx.ORM.Reflection
{
    public static partial class MethodFinder
    {
        public class EntityContextMethods
        {
            public static MethodInfo CreateQuery(Type genericArg)
            {
                return typeof(EntityContext).GetMethod("CreateQuery").MakeGenericMethod(genericArg);
            }
        }

        public class QueryExtensionsMethods
        {
            public static MethodInfo WhereMatchesConditions(Type genericArg)
            {
                return typeof(QueryExtensions).GetMethod("WhereMatchesConditions").MakeGenericMethod(genericArg);
            }

            public static MethodInfo LoadRelated(Type genericArg)
            {
                Func<IQueryable<object>, Expression<Func<object, object>>[], IQueryable<object>> f = QueryExtensions.LoadRelated;
                return f.Method.GetGenericMethodDefinition().MakeGenericMethod(genericArg);
            }
        }

        public class CollectionIncludesLoaderMethods<T>
        {
            public static MethodInfo GetIncludeExpression(Type genericArg)
            {
                return typeof(Materialization.CollectionIncludesLoader<T>)
                    .GetMethod("GetIncludeExpression", BindingFlags.NonPublic | BindingFlags.Instance).MakeGenericMethod(genericArg);
            }
        }

        public class PostProjectionLoaderMethods<T>
        {
            public static MethodInfo GetIncludeExpression(Type genericArg)
            {
                return typeof(Materialization.PostProjectionLoader<T>)
                    .GetMethod("GetIncludeExpression", BindingFlags.NonPublic | BindingFlags.Instance).MakeGenericMethod(genericArg);
            }
        }

        public class EnumerableMethods
        {
            public static MethodInfo ToList(Type genericArg)
            {
                return typeof(System.Linq.Enumerable).GetMethod("ToList").MakeGenericMethod(genericArg);
            }
        }

        public class ObjectMethods
        {
            public static MethodInfo Equals()
            {
                return typeof(object).GetMethod("Equals", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
            }
        }

        public class ModelEntityExtensionsMethods
        {
            public static MethodInfo IsLoaded(params Type[] genericArgs)
            {
                return typeof(ModelEntityExtensions).GetMethod("IsLoaded").MakeGenericMethod(genericArgs);
            }
        }

        public class ObjectTrackerMethods
        {
            public static MethodInfo GetObjectByKey(Type genericArg)
            {
                return typeof(ContextServices.ObjectTracker).GetMethod("GetObjectByKey").MakeGenericMethod(genericArg);
            }
        }
    }
}
