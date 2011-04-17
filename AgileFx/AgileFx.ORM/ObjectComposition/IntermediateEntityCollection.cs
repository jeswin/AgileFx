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
    public class IntermediateEntityCollection<TEntity, TRelatedEntity, TIntermediateEntity, TRelatedIntermediateEntity, TTableEntity, TRelatedTableEntity>
            : List<TRelatedIntermediateEntity>, ICollection<TRelatedIntermediateEntity>, IIntermediateEntityContainer<TIntermediateEntity, TRelatedIntermediateEntity>
        where TEntity : IEntity<TIntermediateEntity>
        where TRelatedEntity : IEntity<TRelatedIntermediateEntity>
        where TIntermediateEntity : IntermediateEntity<TEntity, TTableEntity>
        where TRelatedIntermediateEntity : IntermediateEntity<TRelatedEntity, TRelatedTableEntity>
        where TTableEntity : class, ITableEntity
        where TRelatedTableEntity : class, ITableEntity
    {
        TIntermediateEntity parent;
        public TIntermediateEntity Parent
        {
            get { return parent; }
        }

        public IntermediateEntity GetParent()
        {
            return parent;
        }

        public ICollection<TRelatedEntity> EntityCollection { get; set; }

        //Type parameters are in the reverse order. This is intentional; since this is a reference from 'that' entity to this one.
        Func<TRelatedIntermediateEntity, IIntermediateEntityContainer<TRelatedIntermediateEntity, TIntermediateEntity>> relatedIntermediateEntityContainerGetter;

        public IntermediateEntityCollection(TIntermediateEntity parent,
            Func<TRelatedIntermediateEntity, IIntermediateEntityContainer<TRelatedIntermediateEntity, TIntermediateEntity>> relatedIntermediateEntityContainerGetter)
        {
            this.parent = parent;
            this.relatedIntermediateEntityContainerGetter = relatedIntermediateEntityContainerGetter;
            this.EntityCollection = new EntityCollection<TRelatedEntity, TRelatedIntermediateEntity>(this);
        }

        #region ICollection<TEntity> Members

        void ICollection<TRelatedIntermediateEntity>.Add(TRelatedIntermediateEntity item)
        {
            if (item != null && !Contains(item))
            {
                base.Add(item);
                this.isLoaded = true;

                relatedIntermediateEntityContainerGetter(item).AddReverseReference(this.parent);

                isModified = true;
                parent._isModified = true;

                //Setting the context
                this.ShareEntityContext(item);
            }
        }

        void ICollection<TRelatedIntermediateEntity>.Clear()
        {
            foreach (var item in this.ToArray())
            {
                relatedIntermediateEntityContainerGetter(item).RemoveReverseReference(this.parent);
            }

            isModified = true;
            parent._isModified = true;

            base.Clear();
        }

        public new void Remove(TRelatedIntermediateEntity item)
        {
            if (this.Contains(item))
            {
                isModified = true;
                parent._isModified = true;

                base.Remove(item);
                relatedIntermediateEntityContainerGetter(item).RemoveReverseReference(this.parent);
            }
        }

        bool ICollection<TRelatedIntermediateEntity>.Contains(TRelatedIntermediateEntity item)
        {
            return base.Contains(item);
        }

        void ICollection<TRelatedIntermediateEntity>.CopyTo(TRelatedIntermediateEntity[] array, int arrayIndex)
        {
            base.CopyTo(array, arrayIndex);
        }

        int ICollection<TRelatedIntermediateEntity>.Count
        {
            get { return base.Count; }
        }

        bool ICollection<TRelatedIntermediateEntity>.IsReadOnly
        {
            get { return false; }
        }

        bool ICollection<TRelatedIntermediateEntity>.Remove(TRelatedIntermediateEntity item)
        {
            if (this.Contains(item))
            {
                isModified = true;
                parent._isModified = true;

                base.Remove(item);
                relatedIntermediateEntityContainerGetter(item).RemoveReverseReference(this.parent);
                return true;
            }
            else
                return false;
        }

        #endregion

        #region IEnumerable<TEntity> Members

        IEnumerator<TRelatedIntermediateEntity> IEnumerable<TRelatedIntermediateEntity>.GetEnumerator()
        {
            return base.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return base.GetEnumerator();
        }

        #endregion

        public void Add(object item)
        {
            ((ICollection<TRelatedIntermediateEntity>)this).Add(item as TRelatedIntermediateEntity);
        }

        bool isModified = false;
        public bool IsModified
        {
            get { return isModified; }
        }

        bool isLoaded = false;
        public bool IsLoaded
        {
            get
            {
                return isLoaded;
            }
        }

        public void MaterializationAddReference(object intermediateEntity)
        {
            var item = intermediateEntity as TRelatedIntermediateEntity;
            
            if (item != null && !Contains(item))
            {
                this.isLoaded = true;
                if (relatedIntermediateEntityContainerGetter(item).MaterializationAddReverseReference(this.parent))
                {
                    base.Add(item);

                    //Setting the context
                    this.ShareEntityContext(item);
                }
            }
        }

        public bool MaterializationAddReverseReference(object intermediateEntity)
        {
            var item = intermediateEntity as TRelatedIntermediateEntity;
            if (item != null && !Contains(item))
            {
                base.Add(item);
                isLoaded = true;
            }
            //We always succeed with collections (As in the case of IntermediateEntityReference types)
            return true;
        }

        public void AddReverseReference(TRelatedIntermediateEntity entity)
        {
            if (entity != null && !Contains(entity))
            {
                isModified = true;
                parent._isModified = true;

                base.Add(entity);
                isLoaded = true;
            }
        }

        public void RemoveReverseReference(TRelatedIntermediateEntity entity)
        {
            if (this.Contains(entity))
            {
                isModified = true;
                parent._isModified = true;

                base.Remove(entity);
            }
        }

        public void RemoveReferenceOnRelatedEntity()
        {
            foreach (var entity in this.ToArray())
            {
                if (entity != null)
                    relatedIntermediateEntityContainerGetter(entity).RemoveReverseReference(this.parent);
            }
        }
    }
}