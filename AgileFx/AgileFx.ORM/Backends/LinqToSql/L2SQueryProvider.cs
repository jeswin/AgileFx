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
using System.Data.Linq;
using IQToolkit;

using AgileFx.ORM.Reflection;

namespace AgileFx.ORM.Backends.LinqToSql
{
    public class L2SQueryProvider : EntityQueryProvider
    {
        public L2SQueryProvider(EntityContext entityContext)
            : base(entityContext)
        { }

        protected override IEntityQuery<T> CreateQueryImpl<T>()
        {
            var createQuery = MethodFinder.L2SQueryProvider
                .CreateTableEntityQuery(this.GetType(), EntityContext._InternalServices.TypeTranslationUtil.GetTranslatedType(typeof(T)));
            var tableEntityQuery = createQuery.Invoke(this, null) as IQueryable;

            return new L2SQuery<T>(this)
            {
                TableEntityQueryExpression = (tableEntityQuery as IQueryable).Expression,
                TableEntityQuery = tableEntityQuery
            };
        }

        public override IQueryable<TTableEntity> CreateTableEntityQuery<TTableEntity>()
        {
            var tableEntityQuery = (EntityContext._InternalServices.TableEntityContext as DataContextWrapper).CreateQuery<TTableEntity>();                
            return tableEntityQuery;
        }

        protected override IQueryable<TElement> CreateQueryImpl<TElement>(Expression expression)
        {
            return new L2SQuery<TElement>(this, expression);
        }

        protected override IQueryable CreateQueryImpl(Expression expression)
        {
            Type elementType = TypeHelper.GetElementType(expression.Type);
            return Activator.CreateInstance(typeof(L2SQuery<>)
                .MakeGenericType(elementType), new object[] { this, expression }) as IQueryable;
        }
    }
}
