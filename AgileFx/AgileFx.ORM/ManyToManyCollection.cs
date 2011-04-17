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
using AgileFx.ORM.ObjectComposition;

namespace AgileFx.ORM
{
    public class ManyToManyCollection<TEntity, TMapEntity, TIntermediateEntity, TMapIntermediateEntity> : ICollection<TEntity>
        where TEntity : IEntity<TIntermediateEntity>
        where TIntermediateEntity : IntermediateEntity<TEntity>
        where TMapEntity : class, IEntity<TMapIntermediateEntity>
        where TMapIntermediateEntity : IntermediateEntity<TMapEntity>
    {
        IIntermediateManyToManyCollection<TIntermediateEntity, TMapIntermediateEntity> intermediateCollection;
        public ManyToManyCollection(IIntermediateManyToManyCollection<TIntermediateEntity, TMapIntermediateEntity> intermediateCollection)
        {
            this.intermediateCollection = intermediateCollection;
        }

        #region ICollection<TRelatedEntity> Members

        public void Add(TEntity item)
        {
            intermediateCollection.Add(item._intermediateEntity);
        }

        public void Clear()
        {
            intermediateCollection.Clear();
        }

        public bool Contains(TEntity item)
        {
            return intermediateCollection.Contains(item._intermediateEntity);
        }

        public void CopyTo(TEntity[] array, int arrayIndex)
        {
            int i = 0;
            foreach (var item in this)
            {
                array[i + arrayIndex] = item;
                i++;
            }   
        }

        public int Count
        {
            get { return intermediateCollection.Count; }
        }

        public bool IsReadOnly
        {
            get { return ((ICollection<TIntermediateEntity>)intermediateCollection).IsReadOnly; }
        }

        public bool Remove(TEntity item)
        {
            return ((ICollection<TIntermediateEntity>)intermediateCollection).Remove(item._intermediateEntity);
        }

        #endregion

        #region IEnumerable<TRelatedEntity> Members

        public IEnumerator<TEntity> GetEnumerator()
        {
            foreach (var item in intermediateCollection)
                yield return item._entity;
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            foreach (var item in intermediateCollection)
                yield return item._entity;
        }

        #endregion
    }
}
