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
    public class IntermediateEntityReference<TEntity, TRelatedEntity, TIntermediateEntity, TRelatedIntermediateEntity, TTableEntity, TRelatedTableEntity>
        : IIntermediateEntityContainer<TIntermediateEntity, TRelatedIntermediateEntity>
        where TEntity : IEntity<TIntermediateEntity>
        where TRelatedEntity : IEntity<TRelatedIntermediateEntity>
        where TIntermediateEntity : IntermediateEntity<TEntity, TTableEntity>
        where TRelatedIntermediateEntity : IntermediateEntity<TRelatedEntity, TRelatedTableEntity>
        where TTableEntity : class, ITableEntity
        where TRelatedTableEntity : class, ITableEntity
    {
        bool nullable = false;
        TIntermediateEntity parent;
        public TIntermediateEntity Parent
        {
            get { return parent; }
        }

        public IntermediateEntity GetParent()
        {
            return parent;
        }

        TRelatedIntermediateEntity val;
        //Type parameters are in the reverse order. This is intentional; since this is a reference from 'that' entity to this one.;
        Func<TRelatedIntermediateEntity, IIntermediateEntityContainer<TRelatedIntermediateEntity, TIntermediateEntity>> relatedIntermediateEntityContainerGetter;
        Func<TTableEntity, TRelatedTableEntity> relatedTableEntityGetter;
        Action<TTableEntity, TRelatedTableEntity> relatedTableEntitySetter;

        public TRelatedEntity Entity
        {
            get
            {
                return Value._entity;
            }
            set
            {
                Value = value._intermediateEntity;
            }
        }

        public TRelatedIntermediateEntity Value
        {
            get
            {
                return val;
            }
            set
            {
                isLoaded = true;
                isModified = true;
                parent._isModified = true;

                //If the new value is the same as the current value, then don't do anything.
                if (val == value)
                    return;

                //If the current value is not null (and the new value is different as checked above), remove the reverse relationship.
                if (val != null)
                    relatedIntermediateEntityContainerGetter(val).RemoveReverseReference(this.parent);

                //If new value is not null, add reverse relationship
                if (value != null)
                    relatedIntermediateEntityContainerGetter(value).AddReverseReference(this.parent);

                //Finally, assign the new value;
                val = value;

                //We should attach reverse references on table entities only if the EntityState is not Materializing.
                //  Else, in backends like Linq-to-Sql, the table-entity goes to 'changed' state;
                //      causing writes to the database.
                if (val != null)
                    relatedTableEntitySetter(parent._tableEntity, val._tableEntity);
                else
                    relatedTableEntitySetter(parent._tableEntity, null);

                //Setting the context
                this.ShareEntityContext(value);
            }
        }

        public IntermediateEntityReference(TIntermediateEntity parent,
            Func<TTableEntity, TRelatedTableEntity> relatedTableEntityGetter,
            Action<TTableEntity, TRelatedTableEntity> relatedTableEntitySetter,
            Func<TRelatedIntermediateEntity, IIntermediateEntityContainer<TRelatedIntermediateEntity, TIntermediateEntity>> relatedIntermediateEntityContainerGetter,
            bool nullable)
        {
            this.parent = parent;
            this.relatedTableEntityGetter = relatedTableEntityGetter;
            this.relatedTableEntitySetter = relatedTableEntitySetter;
            this.relatedIntermediateEntityContainerGetter = relatedIntermediateEntityContainerGetter;
            this.nullable = nullable;
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

            //We cannot overwrite edited fields during materialization.
            if (isLoaded) 
                return;

            isLoaded = true;

            //If new item is not null, add reverse relationship
            if (item != null)
            {
                //If we are unable to set the reverse reference, we should set null as the value.
                //Explanation: Consider objA.B points to objB, there would be a reverse reference from objB -> objA, ie objB.A = objA.
                //  If objB.A now points to objA2 instead of objA, this means that the reference objA.B -> objB is no longer valid.
                if (!relatedIntermediateEntityContainerGetter(item).MaterializationAddReverseReference(this.parent))
                {
                    val = null;
                    relatedTableEntitySetter(parent._tableEntity, null);

                    isModified = true;
                    parent._isModified = true;
                    return;
                }
            }

            //Finally, assign the new item;
            val = item;

            //Setting the context
            this.ShareEntityContext(item);
        }

        public bool MaterializationAddReverseReference(object intermediateEntity)
        {
            var entity = intermediateEntity as TRelatedIntermediateEntity;

            //If there is already a value (possibly changed by the user), return false to indicate failure.
            if (isLoaded && val != entity)
                return false;

            isLoaded = true;
            val = entity;

            return true;
        }

        public void AddReverseReference(TRelatedIntermediateEntity item)
        {
            //Remove reference on existing value.
            if (val != null)
                relatedIntermediateEntityContainerGetter(item).RemoveReverseReference(this.parent);

            isLoaded = true;
            isModified = true;
            parent._isModified = true;

            val = item;
            relatedTableEntitySetter(parent._tableEntity, item._tableEntity);
        }

        public void RemoveReverseReference(TRelatedIntermediateEntity entity)
        {
            val = null;

            isModified = true;
            parent._isModified = true;

            relatedTableEntitySetter(parent._tableEntity, null);
        }

        public void RemoveReferenceOnRelatedEntity()
        {
            if (val != null)
                relatedIntermediateEntityContainerGetter(val).RemoveReverseReference(this.parent);
        }
    }
}
