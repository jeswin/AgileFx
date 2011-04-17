/* AgileFx Version 2.0
 * An open-source framework for rapid development of applications and services using Microsoft.net
 * Developed by: AgileHead
 * Website: www.agilefx.org
 * This component is licensed under the terms of the Apache 2.0 License.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AgileFx.ORM
{
    public abstract class IncludeDirective
    {
        public abstract bool IsDefinedOnCollection();
        public abstract LambdaExpression GetSelector();
    }

    public class IncludeDirective<T> : IncludeDirective
    {
        public Expression<Func<T, object>> Selector { get; set; }

        public IncludeDirective(Expression<Func<T, object>> selector)
        {
            this.Selector = selector;
        }

        public override LambdaExpression GetSelector()
        {
            return Selector;
        }

        public override bool IsDefinedOnCollection()
        {
            return false;
        }
    }

    public interface IIncludeInCollectionDirective
    {
        LambdaExpression GetFieldSelector();
    }

    public class IncludeInCollectionDirective<T, TField> : IncludeDirective, IIncludeInCollectionDirective
    {
        public Expression<Func<T, IEnumerable<TField>>> CollectionSelector { get; set; }
        public Expression<Func<TField, object>> FieldSelector { get; set; }

        public IncludeInCollectionDirective(Expression<Func<T, IEnumerable<TField>>> collectionSelector, Expression<Func<TField, object>> fieldSelector)
        {
            this.CollectionSelector = collectionSelector;
            this.FieldSelector = fieldSelector;
        }

        public override LambdaExpression GetSelector()
        {
            return CollectionSelector;
        }

        public override bool IsDefinedOnCollection()
        {
            return true;
        }

        public LambdaExpression GetFieldSelector()
        {
            return FieldSelector;
        }
    }
}
