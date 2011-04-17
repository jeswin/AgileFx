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
using AgileFx.ORM.QueryAnalysis;
using AgileFx.ORM.Caching;
using AgileFx.ORM.Utils;
using AgileFx.ORM.QueryAnalysis.TypeTracking;

namespace AgileFx.ORM.Materialization
{
    public class SingleObjectMaterializer<T> : Materializer<T>
    {
        public SingleObjectMaterializer(EntityContext entityContext)
            : base(entityContext)
        { }

        public virtual T GetResult(Expression expression)
        {
            var visitor = new QueryTranslationVisitor(new QueryAnalysisContext(entityContext._InternalServices.TypeTranslationUtil) { ModifyProjection = true });
            var translationResults = visitor.GetTranslatedResults(expression);

            var tableEntityQuery = translationResults.AnalysisContext.RootEntityQuery.TableEntityQuery;
            var tableEntity =  tableEntityQuery.Provider.Execute(translationResults.TranslatedExpression);

            if (TypesUtil.IsPrimitiveDataType(typeof(T)))
                return (T)tableEntity;
            else
                return MakeResult(tableEntity, translationResults.AnalysisContext.QueryableType);
        }

        public virtual T MakeResult(object tableEntity, SimpleType queryableType)
        {
            T result;
            if (tableEntity != null && tableEntity is AnonymousType)
            {
                var projectedType = (ProjectedType)queryableType;            
                var entity = (T)Materialize(new[] { tableEntity as AnonymousType }, projectedType).First();
                new CollectionIncludesLoader<T>(projectedType, entityContext).LoadUnprojectedIncludes(new[] { entity });
                result = entity;
            }
            else
            {
                //Shouldn't get here though... 
                result = (T)tableEntity;
            }

            return result;
        }
    }
}