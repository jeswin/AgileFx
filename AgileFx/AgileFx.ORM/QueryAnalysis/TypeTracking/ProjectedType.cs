using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using AgileFx.ORM.Utils;
using System.Collections;
using AgileFx.ORM.Mapping;
using AgileFx.ORM.Types;
using IQToolkit;

namespace AgileFx.ORM.QueryAnalysis.TypeTracking
{
    public class ProjectedType : SimpleType
    {
        const string PROJECTED_FIRST_FIELD = "Field0";

        public virtual List<IRelatedBinding> RelatedBindings { get; protected set; }
        public virtual List<IUnprojectedBinding> UnprojectedBindings { get; protected set; }

        public ProjectedType(Type originalType, 
                            Type translatedType, 
                            ConstructorInfo constructor,
                            IEnumerable<SimpleType> constructorArgs, 
                            Dictionary<MemberInfo, 
                            SimpleType> fields,
                            IEnumerable<IRelatedBinding> relatedBindings,
                            IEnumerable<IUnprojectedBinding> unprojectedBindings)
                : base(originalType, translatedType, constructor, constructorArgs, fields)
        {
            this.RelatedBindings = new List<IRelatedBinding>();
            this.RelatedBindings.AddRange(relatedBindings);

            this.UnprojectedBindings = new List<IUnprojectedBinding>();
            this.UnprojectedBindings.AddRange(unprojectedBindings);
        }

        //Gets a parameter which can represent this type in the Translated Expression.
        //Since we have modified the projection, the value will be stored in Field0.
        public override Expression GetTranslatedParameterOrMember(ParameterExpression param)
        {
            return Expression.MakeMemberAccess(param, TranslatedType.GetMember(PROJECTED_FIRST_FIELD).Single());
        }

        //Return the QueryableType associated with the original expression, which we have stored in Field0.
        public override SimpleType GetOriginalQueryableType()
        {
            return Fields.First(kvp => kvp.Key.Name == PROJECTED_FIRST_FIELD).Value;
        }
    }
}
