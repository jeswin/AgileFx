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

namespace AgileFx.ORM.ObjectComposition
{
    public interface IIntermediateManyToManyCollection
    {
    }

    public interface IIntermediateManyToManyCollection<TIntermediateEntity, TMapIntermediateEntity> : ICollection<TIntermediateEntity>, IIntermediateManyToManyCollection
    {
    }

    public class IntermediateManyToManyCollection<TEntity, TRelatedEntity, TMapEntity, TIntermediateEntity, TRelatedIntermediateEntity, TMapIntermediateEntity, TTableEntity, TRelatedTableEntity, TMapTableEntity>
            : IIntermediateManyToManyCollection<TRelatedIntermediateEntity, TMapIntermediateEntity>, IIntermediateEntityContainer<TIntermediateEntity, TMapIntermediateEntity>
        where TEntity : IEntity<TIntermediateEntity> 
        where TRelatedEntity : IEntity<TRelatedIntermediateEntity>
        where TMapEntity : class, IEntity<TMapIntermediateEntity>
        where TIntermediateEntity : IntermediateEntity<TEntity, TTableEntity>
        where TRelatedIntermediateEntity : IntermediateEntity<TRelatedEntity, TRelatedTableEntity>
        where TMapIntermediateEntity : IntermediateEntity<TMapEntity, TMapTableEntity>, new()
        where TTableEntity : ITableEntity
        where TRelatedTableEntity : ITableEntity
        where TMapTableEntity : ITableEntity
    {
        List<TMapIntermediateEntity> maps = new List<TMapIntermediateEntity>();
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

        Func<TMapIntermediateEntity, TIntermediateEntity> sourceEntityGetter;
        Func<TMapIntermediateEntity, TRelatedIntermediateEntity> relatedEntityGetter;
        Action<TMapIntermediateEntity, TIntermediateEntity> sourceEntitySetter;
        Action<TMapIntermediateEntity, TRelatedIntermediateEntity> relatedEntitySetter;
        Func<TRelatedIntermediateEntity, IIntermediateEntityContainer<TRelatedIntermediateEntity, TMapIntermediateEntity>> relatedIntermediateEntityContainerGetter;

        public IntermediateManyToManyCollection(TIntermediateEntity parent,
            Func<TMapIntermediateEntity, TIntermediateEntity> sourceEntityGetter,
            Func<TMapIntermediateEntity, TRelatedIntermediateEntity> relatedEntityGetter,
            Action<TMapIntermediateEntity, TIntermediateEntity> sourceEntitySetter,
            Action<TMapIntermediateEntity, TRelatedIntermediateEntity> relatedEntitySetter,
            Func<TRelatedIntermediateEntity, IIntermediateEntityContainer<TRelatedIntermediateEntity, TMapIntermediateEntity>> relatedIntermediateEntityContainerGetter)
        {
            this.parent = parent;
            this.sourceEntityGetter = sourceEntityGetter;
            this.relatedEntityGetter = relatedEntityGetter;
            this.sourceEntitySetter = sourceEntitySetter;
            this.relatedEntitySetter = relatedEntitySetter;
            this.relatedIntermediateEntityContainerGetter = relatedIntermediateEntityContainerGetter;

            this.EntityCollection = new ManyToManyCollection<TRelatedEntity, TMapEntity, TRelatedIntermediateEntity, TMapIntermediateEntity>(this);
        }

        #region ICollection<TIntermediateEntity> Members

        void ICollection<TRelatedIntermediateEntity>.Add(TRelatedIntermediateEntity item)
        {
            if (item != null && !this.Contains(item))
            {
                var map = new TMapIntermediateEntity();

                sourceEntitySetter(map, parent);
                relatedEntitySetter(map, item);
            	isLoaded = true;

                isModified = true;
                parent._isModified = true;

                //Setting the context
                this.ShareEntityContext(item);

                maps.Add(map);
                relatedIntermediateEntityContainerGetter(item).AddReverseReference(map);
            }
        }

        void ICollection<TRelatedIntermediateEntity>.Clear()
        {
            foreach (var map in maps)
                relatedIntermediateEntityContainerGetter(relatedEntityGetter(map)).RemoveReverseReference(map);

            isModified = true;
            parent._isModified = true;

            //The map entities have to be deleted explicitly
            maps.ForEach(m => m.EntityContext.DeleteObject(m._entity));
            maps.Clear();
        }

        public void Remove(TRelatedIntermediateEntity item)
        {
            ((ICollection<TRelatedIntermediateEntity>)this).Remove(item);
        }

        bool ICollection<TRelatedIntermediateEntity>.Contains(TRelatedIntermediateEntity item)
        {
            return maps.Any(m => relatedEntityGetter(m) == item);
        }

        void ICollection<TRelatedIntermediateEntity>.CopyTo(TRelatedIntermediateEntity[] array, int arrayIndex)
        {
            int i = 0;
            foreach (var item in this)
            {
                array[i + arrayIndex] = item;
                i++;
            }   
        }

        int ICollection<TRelatedIntermediateEntity>.Count
        {
            get { return maps.Count; }
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

                var map = maps.Find(m => relatedEntityGetter(m) == item);

                //The map entity has to be deleted explicitly
                map.EntityContext.DeleteObject(map._entity);
                maps.Remove(map);
                relatedIntermediateEntityContainerGetter(item).RemoveReverseReference(map);

                return true;
            }
            else
                return false;
        }

        #endregion

        #region IEnumerable<TIntermediateEntity> Members

        IEnumerator<TRelatedIntermediateEntity> IEnumerable<TRelatedIntermediateEntity>.GetEnumerator()
        {
            return maps.Select(m => relatedEntityGetter(m)).GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return maps.Select(m => relatedEntityGetter(m)).GetEnumerator();
        }

        #endregion

        public IntermediateManyToManyCollection<TEntity, TRelatedEntity, TMapEntity, TIntermediateEntity, TRelatedIntermediateEntity, TMapIntermediateEntity, TTableEntity, TRelatedTableEntity, TMapTableEntity> GetValue()
        {
            return this;
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

        public void MaterializationAddReference(object objMap)
        {
            var map = objMap as TMapIntermediateEntity;
            isLoaded = true;

            if (!maps.Contains(map))
            {
                maps.Add(map);
                relatedIntermediateEntityContainerGetter(relatedEntityGetter(map)).MaterializationAddReverseReference(map);
            }
        }

        public bool MaterializationAddReverseReference(object objMap)
        {
            var map = objMap as TMapIntermediateEntity;

            if (!maps.Contains(map))
            {
                maps.Add(map);
            }

            //We always succeed with collections (As in the case of IntermediateEntityReference types)
            return true;
        }

        public void AddReverseReference(TMapIntermediateEntity map)
        {
            if (!maps.Contains(map))
            {
                isModified = true;
                parent._isModified = true;

                maps.Add(map);
                isLoaded = true;
            }
        }

        public void RemoveReverseReference(TMapIntermediateEntity map)
        {
            if (maps.Contains(map))
            {
                isModified = true;
                parent._isModified = true;

                maps.Remove(map);
            }
        }

        public void RemoveReferenceOnRelatedEntity()
        {
            foreach (var map in maps.ToArray())
            {
                relatedIntermediateEntityContainerGetter(relatedEntityGetter(map)).RemoveReverseReference(map);
                this.Parent.EntityContext.DeleteObject(map._entity);
            }
        }
    }
}
