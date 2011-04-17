using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using AgileFx.ORM.QueryAnalysis.TypeTracking;
using AgileFx.ORM.Utils;

namespace AgileFx.ORM.QueryAnalysis.MethodTranslation
{
    public class Contains<TSource> : MethodTranslatorList
    {
        public Contains()
        {
            Func<IQueryable<TSource>, TSource, bool> f = Queryable.Contains;
            Func<IEnumerable<TSource>, TSource, bool> f2 = Enumerable.Contains;
            var translator = new ContainsTranslator();
            AddTranslator(f.Method, translator);
            AddTranslator(f2.Method, translator);
        }

        public class ContainsTranslator : MethodTranslator
        {
            public override Expression Translate(MethodCallExpression mce, Expression translatedSource1, IEnumerable<Expression> otherArgs, QueryAnalysisContext context)
            {
                if (context.QueryableType.NonPrimitiveEnumerableItemType is ProjectedType)
                    throw new NotSupportedException("Contains cannot be called after projection.");

                var translatedSource2 = QueryTranslationVisitor.VisitWithNewVisitor(otherArgs.Single(), context, false).TranslatedExpression;
                var newArgs = new Expression[]
                {
                    translatedSource1,
                    translatedSource2
                };

                //If NonPrimitiveEnumerableItemType is not null (case 1), this means we have to change the method's generic args.
                //Otherwise, re-use the same (case 2).
                //The second case occurs when we have a List<int> (for example), which has NonPrimitiveEnumerableItemType = null.
                var translatedMethod = context.QueryableType.NonPrimitiveEnumerableItemType != null ?
                    mce.Method.GetGenericMethodDefinition().MakeGenericMethod(context.QueryableType.NonPrimitiveEnumerableItemType.TranslatedType)
                    : mce.Method;

                context.QueryableType = new SimpleType(typeof(bool), typeof(bool));
                return Expression.Call(mce.Object, translatedMethod, newArgs);
            }

            public override bool ModifyTranslatedSource(MethodCallExpression mce, QueryAnalysisContext context)
            {
                return false;
            }

            public override bool ModifyTranslatedResult(MethodCallExpression mce, QueryAnalysisContext context)
            {
                return context.IsOuterExpression;
            }
        }
    }
}
