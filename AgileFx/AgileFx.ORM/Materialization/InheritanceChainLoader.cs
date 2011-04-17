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
using System.Collections;
using System.Reflection;

using IQToolkit;
using AgileFx.ORM.Reflection;
using AgileFx.ORM.QueryAnalysis;
using AgileFx.ORM.Utils;
using AgileFx.ORM.Mapping;
using AgileFx.ORM.QueryAnalysis.TypeTracking;

namespace AgileFx.ORM.Materialization
{
    public class InheritanceChainLoader<T> : PostProjectionLoader<T>
    {
        public InheritanceChainLoader(ProjectedType projectedType, EntityContext entityContext)
            : base(projectedType, entityContext)
        { }

        public override IEnumerable<IUnprojectedBinding> GetBindingsToAnalyze(ProjectedType projectedType)
        {
            return projectedType.UnprojectedBindings.Where(ub => ub is UnprojectedCollectionBinding);
        }

        public override PostProjectionLoadedResult GetResult(IUnprojectedBinding binding, List<object> objects)
        {
            var result = new PostProjectionLoadedResult();
            result.ProjectionBinding = binding;

            var targetType = TypesUtil.GetGenericArgumentForBaseType(binding.TargetBinding.Type, typeof(IEnumerable<>));

            Func<AnonymousType, object> valueGetter = 
                ao => Get_GetProjectedValue_Computation(binding.TargetBinding)(new TableEntityRow(ao));

            var query = GetQuery(objects, valueGetter, targetType);
            query = LoadIncludes(query, targetType, GetIncludes(binding));
            result.Value = MethodFinder.EnumerableMethods.ToList(targetType).Invoke(null, new object[] { query }) as IList;

            return result;
        }
    }
}
