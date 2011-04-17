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
using System.Collections;
using System.Linq.Expressions;
using IQToolkit;
using System.Runtime.CompilerServices;
using System.Reflection;
using AgileFx.ORM.Caching;
using AgileFx.ORM.QueryAnalysis;
using AgileFx.ORM.Utils;
using AgileFx.ORM.QueryAnalysis.TypeTracking;

namespace AgileFx.ORM.Materialization
{
    public class ResultListMaterializer<T> : Materializer<T>
    {
        public ResultListMaterializer(EntityContext entityContext)
            : base(entityContext)
        { }

        public virtual IEnumerable<T> GetResultList(Expression expression)
        {
            var visitor = new QueryTranslationVisitor(new QueryAnalysisContext(entityContext._InternalServices.TypeTranslationUtil) { ModifyProjection = true });
            var translationResults = visitor.GetTranslatedResults(expression);

            var tableEntityQuery = translationResults.AnalysisContext.RootEntityQuery.TableEntityQuery
                .Provider.CreateQuery(translationResults.TranslatedExpression);

            if (TypesUtil.IsPrimitiveDataType(typeof(T)))
                return tableEntityQuery as IEnumerable<T>;
            else
            {
                var tableEntities = new List<object>();
                foreach (object o in tableEntityQuery)
                    tableEntities.Add(o);

                return MakeResultList(tableEntities, translationResults.AnalysisContext.QueryableType.NonPrimitiveEnumerableItemType);
            }
        }

        public virtual IEnumerable<T> MakeResultList(List<object> tableEntities, SimpleType queryableType)
        {
            var projectedType = (ProjectedType)queryableType;
            var projections = Materialize(tableEntities.Select(x => x as AnonymousType), projectedType).Cast<T>();
            new CollectionIncludesLoader<T>(projectedType, entityContext).LoadUnprojectedIncludes(projections);

            return projections;
        }
    }
}