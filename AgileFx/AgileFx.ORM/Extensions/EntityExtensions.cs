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
using System.Collections;
using System.Reflection;
using System.Runtime.Serialization;

using IQToolkit;

using AgileFx.ORM;
using AgileFx.ORM.Utils;
using AgileFx.ORM.Mapping;
using AgileFx.ORM.ObjectComposition;

namespace AgileFx.ORM
{
    public static class ModelEntityExtensions
    {
        //Create Where Clause using Identity Fields & source values
        static Expression<Func<T, bool>> GetIdentityWhereClause<T>(T source)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var whereCondition = new TypeTranslationUtil().GetMapping<IEntityMapping>(typeof(T))
                .GetIdentityFields().OfType<LambdaExpression>().Select(x => ((MemberExpression)x.Body).Member)
                    .Select(member => Expression.Equal(Expression.MakeMemberAccess(parameter, member), Expression.Constant(member.GetValue(source))))
                        .Aggregate((accumulate, equal) => Expression.And(accumulate, equal));
            return Expression.Lambda<Func<T, bool>>(whereCondition, parameter);
        }

        public static T Load<T>(this T source, EntityContext context, params Expression<Func<T, object>>[] includedFields)
            where T : class, IEntity, new()
        {
            var whereClause = GetIdentityWhereClause(source);
            context.CreateQuery<T>().LoadRelated(includedFields).Where(whereClause).ToArray();
            return source;
        }

        public static T LoadInCollection<T, TCollection>(this T source, EntityContext context,
            Expression<Func<T, IEnumerable<TCollection>>> collectionSelector, params Expression<Func<TCollection, object>>[] includedFields)
            where T : class, IModelEntity, new()
        {
            var whereClause = GetIdentityWhereClause(source);
            context.CreateQuery<T>().LoadRelatedInCollection(collectionSelector, includedFields).Where(whereClause).ToArray();
            return source;
        }

        public static bool IsLoaded<T, TField>(this T source, Expression<Func<T, TField>> fieldSelector)
            where T : IEntity
        {
            return source._getIntermediateEntity().IsLoaded(fieldSelector);
        }

        static LambdaExpression makeFieldSelector<T>(PropertyInfo property)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            return Expression.Lambda(Expression.MakeMemberAccess(parameter, property), parameter);
        }

        public static TPOCO CreatePOCO<T, TPOCO>(this T source)
            where T : IModelEntity
            where TPOCO : IPOCO<T>, new()
        {
            Func<bool, object, bool, IPOCOContainer> getReferenceValue = (isEnumerable, propVal, isLoaded) =>
                isEnumerable ? (IPOCOContainer)new POCOCollection(propVal as IEnumerable, isLoaded)
                    : (IPOCOContainer)new POCOReference(propVal, isLoaded);

            TPOCO poco = new TPOCO();
            foreach (var prop in typeof(TPOCO).GetProperties())
            {
                var propOnSource = typeof(T).GetProperty(prop.Name);
                if (typeof(IPOCOContainer).IsAssignableFrom(prop.PropertyType))
                    prop.SetValue(poco, getReferenceValue(TypesUtil.IsNonPrimitiveCollection(propOnSource.PropertyType),
                        propOnSource.GetValue(source), source._getIntermediateEntity().IsLoaded(makeFieldSelector<T>(propOnSource))));
                else
                    prop.SetValue(poco, propOnSource.GetValue(source));
            }
            return poco;
        }

        public static void Serialize<T, TPOCO>(this T source, SerializationInfo info)
            where T : IModelEntity
            where TPOCO : IPOCO<T>, new()
        {
            var poco = CreatePOCO<T, TPOCO>(source);
            foreach (PropertyInfo prop in poco.GetType().GetProperties())
                info.AddValue(prop.Name, prop.GetValue(poco));
        }

        public static bool IsEquivalent(this IEntity source, IEntity target)
        {
            if (source == null || target == null || source.GetType() != target.GetType()) return false;

            var typeTranslationUtil = new TypeTranslationUtil();
            var identityProperties = typeTranslationUtil.GetMapping<IEntityMapping>(source.GetType()).GetIdentityFields().Cast<LambdaExpression>()
                .Select(l => (l.Body as MemberExpression).Member as PropertyInfo).ToList();
            return identityProperties.All(p => p.GetValue(source).Equals(p.GetValue(target)));
        }
    }
}
