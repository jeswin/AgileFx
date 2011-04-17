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
using System.Linq.Expressions;
using System.Reflection;

namespace AgileFx.ORM.Utils
{
    public static class MethodsUtil
    {
        public static bool MethodIsLoadRelated(MethodCallExpression m)
        {
            return m.Method.Name == "LoadRelated";
        }

        public static bool MethodIsSelect(MethodCallExpression m)
        {
            return m.Method.IsGenericMethod
                    && m.Method.Name == "Select"
                    && typeof(IQueryable).IsAssignableFrom(m.Method.GetParameters()[0].ParameterType)
                    && typeof(IQueryable).IsAssignableFrom(m.Method.ReturnType);
        }

        public static bool MethodIsSelectMany(MethodCallExpression m)
        {
            return m.Method.IsGenericMethod
                    && m.Method.Name == "SelectMany"
                    && typeof(IQueryable).IsAssignableFrom(m.Method.GetParameters()[0].ParameterType)
                    && typeof(IQueryable).IsAssignableFrom(m.Method.ReturnType);
        }

        //? FIXME
        public static bool MethodIsJoin(MethodCallExpression m)
        {
            return m.Method.Name == "Join";
        }

        //? FIXME
        public static bool MethodIsUnion(MethodCallExpression m)
        {
            return m.Method.Name == "Union";
        }

        public static MethodInfo GetSelect<TEntity, TResult>()
        {
            Expression<Func<TEntity, TResult>> selector = x => default(TResult);
            Expression<Func<IQueryable<TEntity>, IQueryable<TResult>>> selectLamba = q => q.Select(selector);
            return (selectLamba.Body as MethodCallExpression).Method;
        }

        public static MethodInfo GetSelect(Type TEntity, Type TResult)
        {
            var methodInfo = typeof(MethodsUtil).GetMethods().Where(m => m.Name == "GetSelect" && m.IsGenericMethod).First();
            return methodInfo.MakeGenericMethod(new Type[] { TEntity, TResult }).Invoke(null, null) as MethodInfo;
        }

        public static MethodInfo GetSelectMany<TElement, TCollection, TResult>()
        {
            Expression<Func<TElement, IEnumerable<TCollection>>> collSelector = x => default(IEnumerable<TCollection>);
            Expression<Func<TElement, TCollection, TResult>> selector = (x, y) => default(TResult);
            Expression<Func<IQueryable<TElement>, IQueryable<TResult>>> selectLamba = q => q.SelectMany(collSelector, selector);
            return (selectLamba.Body as MethodCallExpression).Method;
        }
    }
}
