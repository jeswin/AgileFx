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
using System.Reflection;
using System.Collections.ObjectModel;

using IQToolkit;

using AgileFx.ORM.Utils;
using AgileFx.ORM.Materialization;

namespace AgileFx.ORM
{
    public abstract class EntityQueryProvider : IQueryProvider
    {
        public EntityContext EntityContext { get; set; }

        public EntityQueryProvider(EntityContext entityContext)
        {
            this.EntityContext = entityContext;
        }

        public IEntityQuery<T> CreateQuery<T>()
        {
            return CreateQueryImpl<T>();
        }

        protected abstract IEntityQuery<T> CreateQueryImpl<T>();

        public abstract IQueryable<T> CreateTableEntityQuery<T>()
            where T : class;

        #region IQueryProvider Members

        IQueryable<TElement> IQueryProvider.CreateQuery<TElement>(Expression expression)
        {
            return CreateQueryImpl<TElement>(expression);
        }

        protected abstract IQueryable<TElement> CreateQueryImpl<TElement>(Expression expression);

        IQueryable IQueryProvider.CreateQuery(Expression expression)
        {
            return CreateQueryImpl(expression);
        }

        protected abstract IQueryable CreateQueryImpl(Expression expression);

        T IQueryProvider.Execute<T>(Expression expression)
        {
            return ExecuteImpl<T>(expression);
        }

        public virtual T ExecuteImpl<T>(Expression expression)
        {
            var materializer = EntityContext.GetSingleObjectMaterializer<T>();
            return materializer.GetResult(expression);
        }

        object IQueryProvider.Execute(Expression expression)
        {
            return ExecuteImpl(expression);
        }

        public object ExecuteImpl(Expression expression)
        {
            //?
            throw new NotImplementedException();
        }

        #endregion
        
        //? This method needs verification
        private ConstantExpression GetQueryExpression(MethodCallExpression e)
        {
            if (e.Arguments.First() is ConstantExpression && typeof(IQueryable).IsAssignableFrom(e.Arguments.First().Type))
            {
                return e.Arguments.First() as ConstantExpression;
            }
            return GetQueryExpression(e.Arguments.First() as MethodCallExpression);
        }
    }
}