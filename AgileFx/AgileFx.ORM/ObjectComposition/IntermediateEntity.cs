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
using AgileFx.ORM.Utils;
using System.Collections;

namespace AgileFx.ORM.ObjectComposition
{
    public abstract class IntermediateEntity
    {
        public abstract ITableEntity _getTableEntity();
        public abstract bool IsLoaded(LambdaExpression fieldSelector);
        public bool _isModified { get; set; }

        private EntityContext entityContext;
        public EntityContext EntityContext
        {
            get { return entityContext; }
            set
            {
                if (this.entityContext == null)
                {
                    this.entityContext = value;
                    value._InternalServices.ObjectTracker.Add(this);
                    SetEntityContext(value);
                }
            }
        }

        public virtual void SetEntityContext(EntityContext context)
        {
            foreach (var prop in this.GetType().GetProperties()
                .Where(p => typeof(IIntermediateEntityContainer).IsAssignableFrom(p.PropertyType)))
            {
                var propVal = prop.GetValue(this, null) as IIntermediateEntityContainer;
                if (propVal != null)
                    propVal.SetEntityContextOnParent(context);
            }
        }

        private List<IncludeDirective> includeDirectives = new List<IncludeDirective>();
        public List<IncludeDirective> IncludeDirectives { get { return includeDirectives; } }
    }

    public abstract class IntermediateEntity<TEntity> : IntermediateEntity
    where TEntity : IEntity
    {
        protected TEntity __entity;
        public TEntity _entity 
        {
            get { return __entity; }
            set { __entity = value; }
        }
    }

    public abstract class IntermediateEntity<TEntity, TTableEntity> : IntermediateEntity<TEntity>
        where TEntity : IEntity
        where TTableEntity : ITableEntity
    {
        protected TTableEntity __tableEntity;
        public TTableEntity _tableEntity { get { return __tableEntity; } }

        public override ITableEntity _getTableEntity()
        {
            return _tableEntity;
        }
    }

    public abstract class DerivedIntermediateEntity<TEntity, TTableEntity, TBaseIntermediateEntity> : IntermediateEntity<TEntity, TTableEntity>
        where TEntity : IEntity
        where TTableEntity : ITableEntity
        where TBaseIntermediateEntity : IntermediateEntity
    {
        protected TBaseIntermediateEntity __base;
        public TBaseIntermediateEntity _base { get { return __base; } }	

        public override void SetEntityContext(EntityContext context)
        {
            base.SetEntityContext(context);
            if (__base.EntityContext != null)
                __base.EntityContext = context;
        }
    }
}
