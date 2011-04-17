using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using AgileFx.ORM.QueryAnalysis.TypeTracking;

namespace AgileFx.ORM.QueryAnalysis.MethodTranslation
{
    public class CreateQuery<TSource> : MethodTranslatorList
    {
        public CreateQuery()
        {
            var translator = new CreateQueryTranslator();
            AddTranslator(typeof(EntityContext).GetMethod("CreateQuery"), translator);            
        }

        public class CreateQueryTranslator : MethodTranslator
        {
            public override Expression Translate(MethodCallExpression mce, Expression translatedFirstArg, IEnumerable<Expression> otherArgs, QueryAnalysisContext context)
            {
                //entityContext.CreateQuery<TSource>. We should turn this into tableContext.GetTable<TranslatedSource>
                var genericArg = mce.Method.GetGenericArguments().First();
                var translatedGenericArg = context.TypeTranslationUtil.GetTranslatedType(genericArg);

                context.QueryableType = RuntimeTypes.CreateEnumerable(new SimpleType(genericArg, translatedGenericArg));

                //We cannot use the same mce.Method like we usually do, since the method itself should change (not just generic args)
                //  EntityContext.CreateQuery should be converted to LinqContext.GetTable.
                return context.TypeTranslationUtil.GetTranslatedGenericMethodCall(translatedFirstArg, mce.Method,
                    new Type[] { translatedGenericArg }, new Expression[] { });
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
