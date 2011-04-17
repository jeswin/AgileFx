using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using AgileFx.ORM.Utils;
using AgileFx.ORM.QueryAnalysis.TypeTracking;

namespace AgileFx.ORM.QueryAnalysis
{
    //A binding specified by the programmer in the query.
    //  due to MCEs like Select() and LoadRelated()
    public interface IRequestedBinding
    {
        Type Type { get; set; }
        Expression TranslatedExpression { get; set; }
    }

    //QueryBindings are the fields in the final projection due to the query methods themselves 
    //  (like Select, SelectMany, GroupBy etc)
    public class QueryBinding : IRequestedBinding
    {
        public SimpleType Value { get; set; }
        public Type Type
        {
            get { return Value.Type; }
            set { throw new Exception("Type cannot be set. It should return the Type of the Value property."); }
        }
        public Expression TranslatedExpression { get; set; }
        public Type TranslatedType { get { return TranslatedExpression.Type; } }
    }

    public interface IRelatedBinding
    {
        Expression TranslatedExpression { get; set; }
        Type TranslatedType { get; }
        IRequestedBinding TargetBinding { get; set; }
    }

    public static class IRelatedBindingExtensions
    {
        public static bool MatchesTarget(this IRelatedBinding relatedBinding, IRequestedBinding target)
        {
            return relatedBinding.TargetBinding == target;
        }

        public static bool MatchesTarget(this IRelatedBinding relatedBinding, SimpleType trackedType)
        {
            return relatedBinding.TargetBinding is QueryBinding
                && (relatedBinding.TargetBinding as QueryBinding).Value == trackedType;
        }
    }

    public interface IUnprojectedBinding
    {
        IRequestedBinding TargetBinding { get; set; }
    }

    //These are due to LoadRelated and LoadRelatedInCollection calls.
    public class IncludeBinding : IRelatedBinding, IRequestedBinding
    {
        public IRequestedBinding TargetBinding { get; set; }
        public IncludeDirective IncludeDirective { get; set; }
        public Type Type
        {
            get { return IncludeDirective.GetSelector().Body.Type; }
            set { throw new NotSupportedException(); }
        }
        public Type TranslatedType { get { return TranslatedExpression.Type; } }
        public Expression TranslatedExpression { get; set; }
    }

    //These are the base classes for derived types used in the query.
    public class InheritanceBinding : IRelatedBinding
    {
        public IRequestedBinding TargetBinding { get; set; }
        public Type TranslatedType { get { return TranslatedExpression.Type; } }
        public Expression TranslatedExpression { get; set; }
    }

    public class UnprojectedCollectionBinding : IUnprojectedBinding
    {
        public IRequestedBinding TargetBinding { get; set; }
    }

    public class UnprojectedIncludeInCollectionBinding : IncludeBinding, IUnprojectedBinding
    {
    }

    public class UnprojectedManyToManyBinding : IUnprojectedBinding
    {
        public IRequestedBinding TargetBinding { get; set; }
    }
}