using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using AgileFx.ORM.QueryAnalysis.TypeTracking;

namespace AgileFx.ORM.QueryAnalysis.MethodTranslation
{
    public class Concat<TSource> : MethodTranslatorList
    {
        public Concat()
        {
            Func<IQueryable<TSource>, IEnumerable<TSource>, IQueryable<TSource>> f = Queryable.Concat;
            Func<IEnumerable<TSource>, IEnumerable<TSource>, IEnumerable<TSource>> f2 = Enumerable.Concat;
            var translator = new ConcatTranslator();
            AddTranslator(f.Method, translator);
            AddTranslator(f2.Method, translator);
        }

        public class ConcatTranslator : MethodTranslator
        {
            public override Expression Translate(MethodCallExpression mce, Expression translatedSource1, IEnumerable<Expression> otherArgs, QueryAnalysisContext context)
            {
                var translatedSource2 = QueryTranslationVisitor.VisitWithNewVisitor(otherArgs.Single(), context, context.ModifyProjection).TranslatedExpression;

                if (translatedSource1.Type != translatedSource2.Type)
                    throw new Exception("Concatenated queries must be of the same shape.");
                
                var newArgs = new Expression[]
                {
                    translatedSource1,
                    translatedSource2
                };

                var translatedMethod = mce.Method.GetGenericMethodDefinition().MakeGenericMethod(context.QueryableType.NonPrimitiveEnumerableItemType.TranslatedType);    
                return Expression.Call(mce.Object, translatedMethod, newArgs);
            }

            public override bool ModifyTranslatedSource(MethodCallExpression mce, QueryAnalysisContext context)
            {
                //The first arg should be modified, since Concat does not allow the shape to change later.
                return true;
            }

            public override bool ModifyTranslatedResult(MethodCallExpression mce, QueryAnalysisContext context)
            {
                return false;
            }
        }
    }
}
