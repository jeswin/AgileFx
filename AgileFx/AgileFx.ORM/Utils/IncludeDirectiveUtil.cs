using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace AgileFx.ORM.Utils
{
    public static class IncludeDirectiveUtil
    {
        public static IncludeDirective GetIncludeInCollectionDirective(MemberExpression collectionMemberExp, LambdaExpression collectionFieldSelector)
        {
            var parameter = ExpressionUtil.GetParameterExpression(collectionMemberExp);
            var collectionItemType = TypesUtil.GetGenericArgumentForBaseType(collectionMemberExp.Type, typeof(ICollection<>));
            var delegateType = typeof(Func<,>).MakeGenericType(parameter.Type, typeof(IEnumerable<>).MakeGenericType(collectionItemType));
            var collectionSelector = Expression.Lambda(delegateType, collectionMemberExp, parameter);

            var includeType = typeof(IncludeInCollectionDirective<,>).MakeGenericType(parameter.Type, collectionItemType);
            return Activator.CreateInstance(includeType, collectionSelector, collectionFieldSelector) as IncludeDirective;
        }

        public static IncludeDirective GetIncludeDirective(MemberExpression fieldMemberExp)
        {
            var parameter = ExpressionUtil.GetParameterExpression(fieldMemberExp);
            var delegateType = typeof(Func<,>).MakeGenericType(parameter.Type, typeof(object));
            var fieldSelector = Expression.Lambda(delegateType, fieldMemberExp, parameter);

            return GetIncludeDirective(fieldSelector);
        }

        public static IncludeDirective GetIncludeDirective(LambdaExpression fieldSelector)
        {
            var parameterType = fieldSelector.Parameters.Single().Type;
            var includeType = typeof(IncludeDirective<>).MakeGenericType(parameterType);
            return Activator.CreateInstance(includeType, fieldSelector) as IncludeDirective;
        }
    }
}
