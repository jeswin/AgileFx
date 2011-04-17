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
using System.Reflection;
using System.Text;

namespace AgileFx.ORM
{
    public static class QueryExtensions
    {
        private static Expression<Func<TElement, bool>> GetWhereInExpression<TElement, TValue>(Expression<Func<TElement, TValue>> propertySelector, IEnumerable<TValue> values)
        {
            ParameterExpression p = propertySelector.Parameters.Single();
            if (!values.Any())
                return e => false;

            var equals = values.Select(value => (Expression)Expression.Equal(propertySelector.Body, Expression.Constant(value, typeof(TValue)))).ToList();

            //Fix for Tail Call Optimization issue
            var accumulators = new List<Expression>();
            var startIndex = 0;
            var maxNumber = 100;
            while (startIndex < equals.Count)
            {
                var buffer = equals.Skip(startIndex).Take(maxNumber);
                accumulators.Add(buffer.Aggregate<Expression>((accumulate, equal) => Expression.Or(accumulate, equal)));
                startIndex += buffer.Count();
            }
            var body = accumulators.Aggregate<Expression>((accumulate, equal) => Expression.Or(accumulate, equal));

            return Expression.Lambda<Func<TElement, bool>>(body, p);
        }

        /// <summary> 
        /// Return the element that the specified property's value is contained in the specifiec values 
        /// </summary> 
        /// <typeparam name="TElement">The type of the element.</typeparam> 
        /// <typeparam name="TValue">The type of the values.</typeparam> 
        /// <param name="source">The source.</param> 
        /// <param name="propertySelector">The property to be tested.</param> 
        /// <param name="values">The accepted values of the property.</param> 
        /// <returns>The accepted elements.</returns> 
        public static IQueryable<TElement> WhereIn<TElement, TValue>(this IQueryable<TElement> source, Expression<Func<TElement, TValue>> propertySelector, params TValue[] values)
        {
            return source.Where(GetWhereInExpression(propertySelector, values));
        }

        /// <summary> 
        /// Return the element that the specified property's value is contained in the specifiec values 
        /// </summary> 
        /// <typeparam name="TElement">The type of the element.</typeparam> 
        /// <typeparam name="TValue">The type of the values.</typeparam> 
        /// <param name="source">The source.</param> 
        /// <param name="propertySelector">The property to be tested.</param> 
        /// <param name="values">The accepted values of the property.</param> 
        /// <returns>The accepted elements.</returns> 
        public static IQueryable<TElement> WhereIn<TElement, TValue>(this IQueryable<TElement> source, Expression<Func<TElement, TValue>> propertySelector, IEnumerable<TValue> values)
        {
            return source.Where(GetWhereInExpression(propertySelector, values));
        }

        public static IQueryable<TElement> WhereMatchesConditions<TElement>(this IQueryable<TElement> source, IEnumerable<List<Condition>> conditionValues)
        {
            if (!conditionValues.Any())
                return source.Where(e => false);

            var parameters = conditionValues.First().First().Expression.Parameters;

            var body = conditionValues.Select(cvl => cvl.Select(cv =>
                (Expression)Expression.Equal(cv.Expression.Body, Expression.Constant(cv.Value, cv.Expression.Body.Type)))
                    .Aggregate((accumulate, x) => Expression.And(accumulate, x)))
                .Aggregate((accumulate, x) => Expression.Or(accumulate, x));

            var whereIn = Expression.Lambda<Func<TElement, bool>>(body, parameters);
            return source.Where(whereIn);
        }

        public static PartialResultset<T> ToPartialResultSet<T>(this IOrderedQueryable<T> query, int startNum, int maxResults)
        {
            return new PartialResultset<T>(query.Skip(startNum - 1).Take(maxResults), startNum, query.Count(), maxResults);
        }

        public static IQueryable<T> LoadRelated<T>(this IQueryable<T> query, params Expression<Func<T, object>>[] includedFields)
            where T : class
        {
            var directives = new List<IncludeDirective>();
            foreach(var inc in includedFields)
                directives.Add(new IncludeDirective<T>(inc));
            return LoadRelated(query, Expression.Constant(directives.ToArray()));
        }

        public static IQueryable<T> LoadRelatedInCollection<T, TCollection>(this IQueryable<T> query, 
                Expression<Func<T, IEnumerable<TCollection>>> collectionSelector,
                params Expression<Func<TCollection, object>>[] includedFields)
            where T : class
        {
            var directives = new List<IncludeDirective>();
            foreach (var inc in includedFields)
                directives.Add(new IncludeInCollectionDirective<T, TCollection>(collectionSelector, inc));
            return LoadRelated(query, Expression.Constant(directives.ToArray()));
        }

        public static IQueryable<T> LoadRelated<T>(this IQueryable<T> query, Expression includes)
        {
            return query.Provider.CreateQuery<T>(Expression.Call(null,
                ((MethodInfo)MethodBase.GetCurrentMethod()).MakeGenericMethod(new Type[] { typeof(T) }), new Expression[] { query.Expression, Expression.Constant(includes) }));
        }
    }

    public class Condition
    {
        public Condition(LambdaExpression exp, object value)
        {
            this.Expression = exp;
            this.Value = value;
        }

        public LambdaExpression Expression { get; set; }
        public object Value { get; set; }
    }
}
