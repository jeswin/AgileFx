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
using System.Data.Linq;
using System.Reflection;
using System.Linq.Expressions;

using AgileFx.ORM.Reflection;
using AgileFx.ORM.Utils;

namespace AgileFx.ORM.Backends.LinqToSql
{
    public class L2STypeTranslationUtil : TypeTranslationUtil
    {
        public override MethodCallExpression GetTranslatedGenericMethodCall(Expression instance, MethodInfo method, 
            IEnumerable<Type> translatedGenericArgs, IEnumerable<Expression> translatedArgs)
        {
            if (typeof(EntityContext).IsAssignableFrom(method.DeclaringType) && method.Name == "CreateQuery")
            {
                var translatedMethod = MethodFinder.LinqToSqlDataContext.GetTable(translatedGenericArgs.First());
                return Expression.Call(instance, translatedMethod, translatedArgs);
            }
            else
                return base.GetTranslatedGenericMethodCall(instance, method, translatedGenericArgs, translatedArgs);
        }

        public override Type GetTranslatedType(Type t)
        {
            if (t == typeof(EntityContext))
                return typeof(DataContext);
            else
                return base.GetTranslatedType(t);
        }
    }
}
