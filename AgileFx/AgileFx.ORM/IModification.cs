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

namespace AgileFx.ORM
{
    public interface IModification
    {
        IFieldReference GetFieldReference();
        object _applyChanges(object container, EntityContext context);
        ModificationType Type { get; }
    }

    public abstract class IModification<TElement> : IModification
        where TElement : class, new()
    {
        #region IModification Members

        public abstract IFieldReference GetFieldReference();
        public abstract object _applyChanges(object container, EntityContext context);
        #endregion

        public abstract ModificationType Type { get; }

        List<IModification> entityFieldModifications = new List<IModification>();
        public List<IModification> EntityFieldModifications
        {
            get { return entityFieldModifications; }
            set { entityFieldModifications = value; }
        }

        protected Func<IModification<TElement>, object, TElement> ApplyChangesOverride { get; set; }

        public virtual TElement GetElement(object container, EntityContext context)
        {
            return GetFieldReference().GetElement(container) as TElement;
        }

        //This is written with the sole purpose of avoiding a manual typecast.
        //The actual code is in _applyChanges.
        internal TElement ApplyChanges(object container, EntityContext context)
        {
            return _applyChanges(container, context) as TElement;
        }

        protected void ApplyInnerModifications(object element, EntityContext context)
        {            
            foreach (IModification innerMod in EntityFieldModifications)
            {
                if (innerMod != null)
                {
                    innerMod._applyChanges(element, context);
                }
            }
        }

        public IModification<TField> PopFieldUpdate<TField>(Expression<Func<TElement, TField>> selector)
            where TField : class, new()
        {
            var selUpdate = GetFieldUpdate(selector);
            entityFieldModifications = entityFieldModifications.Where(upd => !upd.Equals(selUpdate)).ToList();
            return selUpdate;
        }

        //_applyChangesOverride takes a modification of the field, and an instance of its parent(container, viz TElement).
        public void OverrideFieldUpdate<TField>(Expression<Func<TElement, TField>> selector,
            Func<IModification<TField>, object, TField> _applyChangesOverride)
                    where TField : class, new()
        {
            var selUpdate = GetFieldUpdate(selector);
            if (selUpdate != null)
                selUpdate.ApplyChangesOverride = _applyChangesOverride;
        }

        public IModification<TField> GetFieldUpdate<TField>(Expression<Func<TElement, TField>> selector)
            where TField : class, new()
        {
            string selectorProp = ExpressionUtil.GetPropertyName(selector);

            foreach (IModification upd in entityFieldModifications)
            {
                if (selectorProp == upd.GetFieldReference().GetFieldName())
                    return upd as IModification<TField>;
            }
            return null;
        }
    }
}
