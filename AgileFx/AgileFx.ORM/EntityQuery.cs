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
using System.Linq.Expressions;
using IQToolkit;
using System.Runtime.CompilerServices;
using System.Reflection;
using AgileFx.ORM.Materialization;

namespace AgileFx.ORM
{
    public interface IEntityQuery
    {
        Expression TableEntityQueryExpression { get; set; }
        IQueryable TableEntityQuery { get; set; }
    }

    public interface IEntityQuery<T> : IQueryable<T>, IOrderedQueryable<T>, IEnumerable<T>, IOrderedQueryable, 
        IQueryable, IEnumerable, IEntityQuery
    {
        
    }

    public abstract class EntityQuery<T> : IEntityQuery<T>
    {
        Expression expression;
        EntityQueryProvider provider;

        public Expression TableEntityQueryExpression { get; set; }
        public IQueryable TableEntityQuery { get; set; }

        public EntityQuery(EntityQueryProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("Provider");
            }
            this.provider = provider;
            this.expression = Expression.Constant(this);
        }

        public EntityQuery(EntityQueryProvider provider, Expression expression)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("Provider");
            }
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }
            if (!typeof(IQueryable<T>).IsAssignableFrom(expression.Type))
            {
                throw new ArgumentOutOfRangeException("expression");
            }
            this.provider = provider;
            this.expression = expression;
        }

        public Expression Expression
        {
            get { return this.expression; }
        }

        public Type ElementType
        {
            get { return typeof(T); }
        }

        public IQueryProvider Provider
        {
            get { return this.provider; }
        }

        public virtual IEnumerator<T> GetEnumerator()
        {
            var materializer = provider.EntityContext.GetResultListMaterializer<T>();

            var results = materializer.GetResultList(expression);

            foreach (var entity in results)
                yield return entity;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            var materializer = provider.EntityContext.GetResultListMaterializer<T>();

            var results = materializer.GetResultList(expression);

            foreach (var entity in results)
                yield return entity;
        }

        public override string ToString()
        {
            if (this.expression.NodeType == ExpressionType.Constant &&
                ((ConstantExpression)this.expression).Value == this)
            {
                return "EntityQuery(" + typeof(T) + ")";
            }
            else
            {
                return this.expression.ToString();
            }
        }
    }
}