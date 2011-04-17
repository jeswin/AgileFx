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
using L2S=System.Data.Linq;

namespace AgileFx.ORM.Reflection
{
    public partial class MethodFinder
    {
        public class IEntity
        {
            public static MethodInfo _setIntermediateEntity(Type entityType)
            {
                return entityType.GetMethod("_setIntermediateEntity");
            }
        }

        public class L2SQueryProvider
        {
            public static MethodInfo CreateTableEntityQuery(Type providerType, Type genericArg)
            {
                return providerType.GetMethod("CreateTableEntityQuery").MakeGenericMethod(genericArg);
            }
        }

        public class LinqToSqlDataContext
        {
            public static MethodInfo GetTable(Type genericArg)
            {
                return typeof(L2S.DataContext).GetMethods().Where(m => m.Name == "GetTable" && m.IsGenericMethod)
                    .Single().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { genericArg });
            }
        }

        public class LinqToSqlCompiledQuery
        {
            public static MethodInfo Compile(Type[] genericArgs)
            {   
                return typeof(L2S.CompiledQuery).GetMethods().Where(m => m.Name == "Compile" && m.IsGenericMethod
                    && m.GetGenericMethodDefinition().GetGenericArguments().Count() == genericArgs.Length)
                        .Single().GetGenericMethodDefinition().MakeGenericMethod(genericArgs);
            }
        }
    }
}
