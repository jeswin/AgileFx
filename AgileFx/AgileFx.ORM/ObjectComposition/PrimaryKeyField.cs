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

namespace AgileFx.ORM.ObjectComposition
{
    public class PrimaryKeyField<TEntity, TIntermediateEntity, TTableEntity, TField>
        : EntityField<TEntity, TIntermediateEntity, TTableEntity, TField>
        where TIntermediateEntity : IntermediateEntity<TEntity, TTableEntity>
        where TEntity : IEntity<TIntermediateEntity>
        where TTableEntity : ITableEntity<TIntermediateEntity>
    {
        public PrimaryKeyField(TIntermediateEntity parent, Func<TTableEntity, TField> tableEntityFieldGetter, Action<TTableEntity, TField> tableEntityFieldSetter)
            : base(parent, tableEntityFieldGetter, tableEntityFieldSetter)
        {
        }
    }

    public class DerivedPrimaryKeyField<TEntity, TIntermediateEntity, TTableEntity, TBaseIntermediateEntity, TField>
        where TIntermediateEntity : IntermediateEntity<TEntity, TTableEntity>
        where TEntity : IEntity<TIntermediateEntity>
        where TTableEntity : ITableEntity<TIntermediateEntity>
        where TBaseIntermediateEntity : IntermediateEntity
    {
        Func<TBaseIntermediateEntity, TField> baseIntermediateEntityFieldGetter;
        Action<TBaseIntermediateEntity, TField> baseIntermediateEntityFieldSetter;
        Action<TTableEntity, TField> tableEntityFieldSetter;

        protected bool isModified = false;
        public bool IsModified
        {
            get { return isModified; }
        }

        protected TIntermediateEntity parent;
        public TIntermediateEntity Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        protected TBaseIntermediateEntity baseIntemediateEntity;
        public TBaseIntermediateEntity BaseIntemediateEntity
        {
            get { return baseIntemediateEntity; }
        }

        public DerivedPrimaryKeyField(TIntermediateEntity parent, TBaseIntermediateEntity baseIntemediateEntity, Func<TBaseIntermediateEntity, TField> baseIntermediateEntityFieldGetter, Action<TBaseIntermediateEntity, TField> baseIntermediateEntityFieldSetter, Action<TTableEntity, TField> tableEntityFieldSetter)
        {
            this.parent = parent;
            this.baseIntemediateEntity = baseIntemediateEntity;
            this.baseIntermediateEntityFieldGetter = baseIntermediateEntityFieldGetter;
            this.baseIntermediateEntityFieldSetter = baseIntermediateEntityFieldSetter;
            this.tableEntityFieldSetter = tableEntityFieldSetter;
        }

        public TField Value
        {
            get
            {
                return baseIntermediateEntityFieldGetter(baseIntemediateEntity);
            }
            set
            {
                tableEntityFieldSetter(parent._tableEntity, value);
                baseIntermediateEntityFieldSetter(baseIntemediateEntity, value);
            }
        }
    }
}
