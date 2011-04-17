/* AgileFx Version 2.0
 * An open-source framework for rapid development of applications and services using Microsoft.net
 * Developed by: AgileHead
 * Website: www.agilefx.org
 * This component is licensed under the terms of the Apache 2.0 License.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Collections;

namespace AgileFx.ORM.Utils
{
    public static class ExpressionUtil
    {
        public static string GetPropertyName(LambdaExpression expression)
        {
            if (null == expression) return null;

            //if (expression.Body
            string rhs = expression.Body.ToString();
            int dot = rhs.IndexOf('.');
            var result = (dot != -1) ? rhs.Substring(dot + 1) : null;
            return result;
        }

        public static PropertyInfo GetProperty(LambdaExpression expression, Type type)
        {
            var propName = GetPropertyName(expression);
            return propName.Empty() ? null : type.GetProperty(propName);
        }

        //Gets the property value refered to by the Expression
        public static object GetPropertyValue(LambdaExpression expression, object container)
        {
            var prop = GetProperty(expression, container.GetType());
            return (null == prop) ? container : prop.GetValue(container, null);
        }

        public static bool IsMatching(Expression exp1, Expression exp2)
        {
            if (exp1.Type != exp2.Type) return false;
            if (exp1 is MemberExpression)
            {
                var mem1 = exp1 as MemberExpression;
                var mem2 = exp2 as MemberExpression;
                return (mem1.Member.Name == mem2.Member.Name) && IsMatching(mem1.Expression, mem2.Expression);
            }
            else if (exp1 is ParameterExpression)
                return exp1 == exp2;
            else
                return false;
        }

        public static int GetDepth(this MemberExpression expression)
        {
            int counter = 0;

            while (expression.Expression is MemberExpression)
            {
                expression = expression.Expression as MemberExpression;
                counter++;
            }

            return counter + 1;
        }

        /// <summary>
        /// Finds whether exp1 is a part of the expression exp2
        /// </summary>
        /// <param name="exp1"></param>
        /// <param name="exp2"></param>
        /// <returns></returns>
        public static bool IsPartOf(this MemberExpression exp1, MemberExpression exp2)
        {
            Func<MemberExpression, int, MemberExpression> SliceToDepth = (exp, depth) =>
            {
                var currentDepth = exp.GetDepth();
                for (int i = currentDepth; i > depth; i--) exp = exp.Expression as MemberExpression;
                return exp;
            };

            var depth1 = exp1.GetDepth();
            var depth2 = exp2.GetDepth();
            if (depth1 >= depth2) return false;
            var partOfExp2 = SliceToDepth(exp2, depth1);

            return IsMatching(exp1, partOfExp2);
        }

        public static bool IsEquivalent(Expression exp1, Expression exp2)
        {
            if (exp1.Type != exp2.Type) return false;
            if (exp1 is LambdaExpression && exp2 is LambdaExpression)
                return IsEquivalent((exp1 as LambdaExpression).Body, (exp2 as LambdaExpression).Body);
            if (exp1 is MemberExpression)
            {
                var mem1 = exp1 as MemberExpression;
                var mem2 = exp2 as MemberExpression;
                return (mem1.Member.Name == mem2.Member.Name) && IsEquivalent(mem1.Expression, mem2.Expression);
            }
            else if (exp1 is ParameterExpression)
                return exp1.Type == exp2.Type;
            else
                return false;
        }

        public static Expression ReplaceInnermostMemberExpressionWithParameter(MemberExpression member)
        {
            if (member.Expression is MemberExpression)
                return Expression.MakeMemberAccess(ReplaceInnermostMemberExpressionWithParameter(member.Expression as MemberExpression), member.Member);
            else
                return Expression.Parameter(member.Type, "p");
        }

        public static Expression ReplaceInnermostMemberExpression(MemberExpression member, Expression replacement)
        {
            if (member.Expression is MemberExpression)
                return Expression.MakeMemberAccess(ReplaceInnermostMemberExpression(member.Expression as MemberExpression, replacement), member.Member);
            else
                return replacement;
        }

        public static MemberExpression GetInnermostMemberExpression(MemberExpression member)
        {
            if (member.Expression is MemberExpression)
                return GetInnermostMemberExpression(member.Expression as MemberExpression);
            else
                return member;
        }

        public static ParameterExpression GetParameterExpression(MemberExpression member)
        {
            return (member.Expression is MemberExpression) ? 
                GetParameterExpression(member.Expression as MemberExpression) : member.Expression as ParameterExpression;
        }
    }
}
