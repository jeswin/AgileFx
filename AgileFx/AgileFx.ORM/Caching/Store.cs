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

using AgileFx.ORM.Serialization;

namespace AgileFx.ORM.Caching
{
    public abstract class Store
    {
        public abstract CacheQueryResult<TResult> Query<TResult>(string f, object entity, params object[] parameters);

        public abstract void Add<TResult>(SerializedForm<TResult> result, string f, object entity, CacheParams cacheParams, params object[] parameters);

        public abstract void InvalidateItemWithKey(string key);

        public abstract void InvalidateItemWithKeyFragments(string[] keyFragments);
    }
}
