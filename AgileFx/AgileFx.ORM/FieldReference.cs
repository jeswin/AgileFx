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
    public interface IFieldReference
    {
        object GetElement(object container);
        string GetFieldName();
        FieldSelectorType SelectorType { get; }
    }

    public enum FieldSelectorType
    {
        FieldSelector,
        CollectionSelector
    }

    public class FieldReference<TContainer, TElement> : IFieldReference
        where TContainer : class, new()
        where TElement : class, new()
    {        
        Expression<Func<TContainer, TElement>> FieldSelector { get; set; }
        Expression<Func<TContainer, IEnumerable<TElement>>> CollectionSelector { get; set; }

        public FieldReference(Expression<Func<TContainer, TElement>> fieldSelector)
        {
            this.FieldSelector = fieldSelector;
        }

        public FieldReference(Expression<Func<TContainer, IEnumerable<TElement>>> collectionSelector)
        {
            this.CollectionSelector = collectionSelector;
        }

        public void SetValue(object container, object val)
        {
            if (container != null)
            {
                if (FieldSelector != null)
                {
                    var entProp = ExpressionUtil.GetProperty(FieldSelector, container.GetType());
                    if (null != entProp) entProp.SetValue(container, val, null);
                }
                else if (CollectionSelector != null)
                {
                    var coll = ExpressionUtil.GetPropertyValue(CollectionSelector, container) as ICollection<TElement>;
                    coll.Add(val as TElement);
                }
            }
        }

        public object RemoveValue(object container)
        {
            var removee = ExpressionUtil.GetPropertyValue(FieldSelector, container) as TElement;
            var entProp = ExpressionUtil.GetProperty(FieldSelector, container.GetType());
            entProp.SetValue(container, null, null);
            return removee;
        }

        public object RemoveValueFromCollection(object container, Func<TElement, bool> predicate)
        {
            var coll = ExpressionUtil.GetPropertyValue(CollectionSelector, container) as ICollection<TElement>;
            var removee = coll.First(predicate);
            coll.Remove(removee);
            return coll;
        }

        public object GetElement(object container)
        {
            return ExpressionUtil.GetPropertyValue(FieldSelector, container) as TElement;
        }

        public string GetFieldName()
        {
            if (FieldSelector != null)
                return ExpressionUtil.GetPropertyName(FieldSelector);
            else if (CollectionSelector != null)
                return ExpressionUtil.GetPropertyName(CollectionSelector);

            return null;
        }

        public FieldSelectorType SelectorType
        {
            get
            {
                if (FieldSelector != null)
                    return FieldSelectorType.FieldSelector;
                else
                    return FieldSelectorType.CollectionSelector;
            }
        }
    }
}