﻿/* AgileFx Version 2.0
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
using System.Reflection;
using AgileFx.ORM.Utils;

namespace AgileFx.ORM.Mapping
{
    public interface IEntityMapping
    {
        IEnumerable GetIdentityFields();
        IEnumerable<IdentityFieldInfo> GetIdentityFieldInfo();
        Type EntityType { get; }
        Type IntermediateEntityType { get; }
        Type TableEntityType { get; }
    }

    public class IdentityFieldInfo
    {
        public LambdaExpression FieldSelector { get; private set; }
        public bool AutoGenerated { get; private set; }

        public IdentityFieldInfo(LambdaExpression fieldSelector, bool autoGenerated)
        {
            this.FieldSelector = fieldSelector;
            this.AutoGenerated = autoGenerated;
        }
    }

    public class EntityMapping<TEntity, TIntermediateEntity, TTableEntity> : IEntityMapping
    {
        private Type entityType = typeof(TEntity);
        public Type EntityType { get { return entityType; } }

        private Type tableEntityType = typeof(TTableEntity);
        public Type TableEntityType { get { return tableEntityType; } }

        private Type intermediateEntityType = typeof(TIntermediateEntity);
        public Type IntermediateEntityType { get { return intermediateEntityType; } }

        List<IdentityFieldInfo> identityFields = new List<IdentityFieldInfo>();
        
        public IEnumerable GetIdentityFields()
        {
            return identityFields.Select(i => i.FieldSelector);
        }

        public IEnumerable<IdentityFieldInfo> GetIdentityFieldInfo()
        {
            return identityFields;
        }

        public void AddIdentityField<TField>(Expression<Func<TEntity, TField>> fieldRef)
        {
            identityFields.Add(new IdentityFieldInfo(fieldRef, true));
        }

        public void AddIdentityField<TField>(Expression<Func<TEntity, TField>> fieldRef, bool autoGenerated)
        {
            identityFields.Add(new IdentityFieldInfo(fieldRef, autoGenerated));
        }
    }
}
