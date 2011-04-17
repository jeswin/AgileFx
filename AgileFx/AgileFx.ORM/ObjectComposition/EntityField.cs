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
    public interface IEntityField
    {
        void MaterializeSet(object item);
    }

    public class EntityField<TEntity, TIntermediateEntity, TTableEntity, TField> : IEntityField
        where TIntermediateEntity : IntermediateEntity<TEntity, TTableEntity>
        where TEntity : IEntity<TIntermediateEntity>
        where TTableEntity : ITableEntity<TIntermediateEntity>
    {
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

        protected Func<TTableEntity, TField> tableEntityFieldGetter;
        protected Action<TTableEntity, TField> tableEntityFieldSetter;

        public EntityField(TIntermediateEntity parent, Func<TTableEntity, TField> tableEntityFieldGetter, Action<TTableEntity, TField> tableEntityFieldSetter)
        {
            this.parent = parent;
            this.tableEntityFieldGetter = tableEntityFieldGetter;
            this.tableEntityFieldSetter = tableEntityFieldSetter;
        }

        public TField Value 
        {
            get
            {
                return tableEntityFieldGetter(parent._tableEntity);
            }
            set
            {
                parent._isModified = true;

                isModified = true;
                
                tableEntityFieldSetter(parent._tableEntity, value);
            }
        }

        public void MaterializeSet(object item)
        {
            tableEntityFieldSetter(parent._tableEntity, (TField)item);
        }
    }
}
