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
using System.Runtime.Serialization.Formatters.Binary;
using AgileFx.ORM.ContextServices;

namespace AgileFx.ORM.Caching
{
    public class Cache
    {
        EntityContext context;
        ObjectGraphSerializer objectGraphSerializer;
        BinaryFormatter formatter;
        ContextBinder contextBinder;

        public Cache(EntityContext context)
        {
            this.context = context;
            this.Store = GetStore();
            this.formatter = new BinaryFormatter();
            this.objectGraphSerializer = GetObjectGraphSerializer();
            this.contextBinder = new ContextBinder(context);
        }

        public Store Store { get; private set; }
        public CacheParams CacheParams { get; private set; }

        public Cache SetCacheParams(CacheParams cacheParams)
        {
            this.CacheParams = cacheParams;
            return this;
        }

        private TResult InvokeImpl<TResult>(string funcId, object instance, Func<TResult> resultMaterializer, CacheParams cacheParams, params object[] parameters)
        {
            var cacheQuery = Store.Query<TResult>(funcId, instance, parameters);

            if (cacheQuery.Found)
                return cacheQuery.SerializedForm.Deserialize(contextBinder, formatter, context);
            else
            {
                var result = resultMaterializer();
                var serializedResult = objectGraphSerializer.Serialize(result);
                Store.Add<TResult>(serializedResult, funcId, instance, cacheParams, parameters);
                return result;
            }
        }

        public TResult Invoke<TResult>
            (Func<EntityContext, TResult> f, object instance)
        {
            return InvokeImpl(f.Method.ToString(), instance, () => f(context), new CacheParams());
        }

        public TResult Invoke<TResult>
            (Func<EntityContext, TResult> f, object instance, CacheParams cacheParams)
        {
            return InvokeImpl(f.Method.ToString(), instance, () => f(context), cacheParams);
        }

        public TResult Invoke<TParam1, TResult>
            (Func<TParam1, EntityContext, TResult> f, object instance, TParam1 param1)
        {
            return InvokeImpl(f.Method.ToString(), instance, () => f(param1, context), new CacheParams(), param1);
        }

        public TResult Invoke<TParam1, TResult>
            (Func<TParam1, EntityContext, TResult> f, object instance, TParam1 param1, CacheParams cacheParams)
        {
            return InvokeImpl(f.Method.ToString(), instance, () => f(param1, context), cacheParams, param1);
        }

        public TResult Invoke<TParam1, TParam2, TResult>
            (Func<TParam1, TParam2, EntityContext, TResult> f, object instance, TParam1 param1, TParam2 param2)
        {
            return InvokeImpl(f.Method.ToString(), instance, () => f(param1, param2, context), new CacheParams(), param1, param2);
        }

        public TResult Invoke<TParam1, TParam2, TResult>
            (Func<TParam1, TParam2, EntityContext, TResult> f, object instance, TParam1 param1, TParam2 param2, CacheParams cacheParams)
        {
            return InvokeImpl(f.Method.ToString(), instance, () => f(param1, param2, context), cacheParams, param1, param2);
        }

        public TResult Invoke<TParam1, TParam2, TParam3, TResult>
            (Func<TParam1, TParam2, TParam3, EntityContext, TResult> f, object instance, TParam1 param1, TParam2 param2, TParam3 param3)
        {
            return InvokeImpl(f.Method.ToString(), instance, () => f(param1, param2, param3, context), new CacheParams(), param1, param2, param3);
        }

        public TResult Invoke<TParam1, TParam2, TParam3, TResult>
            (Func<TParam1, TParam2, TParam3, EntityContext, TResult> f, object instance, TParam1 param1, TParam2 param2, TParam3 param3, CacheParams cacheParams)
        {
            return InvokeImpl(f.Method.ToString(), instance, () => f(param1, param2, param3, context), cacheParams, param1, param2, param3);
        }

        public TResult Invoke<TParam1, TParam2, TParam3, TParam4, TResult>
            (Func<TParam1, TParam2, TParam3, TParam4, EntityContext, TResult> f, object instance, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4)
        {
            return InvokeImpl(f.Method.ToString(), instance, () => f(param1, param2, param3, param4, context), new CacheParams(), param1, param2, param3, param4);
        }

        public TResult Invoke<TParam1, TParam2, TParam3, TParam4, TResult>
            (Func<TParam1, TParam2, TParam3, TParam4, EntityContext, TResult> f, object instance, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, CacheParams cacheParams)
        {
            return InvokeImpl(f.Method.ToString(), instance, () => f(param1, param2, param3, param4, context), cacheParams, param1, param2, param3, param4);
        }

        public TResult Invoke<TParam1, TParam2, TParam3, TParam4, TParam5, TResult>
            (Func<TParam1, TParam2, TParam3, TParam4, TParam5, EntityContext, TResult> f, object instance, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5)
        {
            return InvokeImpl(f.Method.ToString(), instance, () => f(param1, param2, param3, param4, param5, context), new CacheParams(), param1, param2, param3, param4, param5);
        }

        public TResult Invoke<TParam1, TParam2, TParam3, TParam4, TParam5, TResult>
            (Func<TParam1, TParam2, TParam3, TParam4, TParam5, EntityContext, TResult> f, object instance, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, CacheParams cacheParams)
        {
            return InvokeImpl(f.Method.ToString(), instance, () => f(param1, param2, param3, param4, param5, context), cacheParams, param1, param2, param3, param4, param5);
        }

        public TResult Invoke<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TResult>
            (Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, EntityContext, TResult> f, object instance, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6)
        {
            return InvokeImpl(f.Method.ToString(), instance, () => f(param1, param2, param3, param4, param5, param6, context), new CacheParams(), param1, param2, param3, param4, param5, param6);
        }

        public TResult Invoke<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TResult>
            (Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, EntityContext, TResult> f, object instance, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, CacheParams cacheParams)
        {
            return InvokeImpl(f.Method.ToString(), instance, () => f(param1, param2, param3, param4, param5, param6, context), cacheParams, param1, param2, param3, param4, param5, param6);
        }

        public TResult Invoke<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TResult>
            (Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, EntityContext, TResult> f, object instance, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7)
        {
            return InvokeImpl(f.Method.ToString(), instance, () => f(param1, param2, param3, param4, param5, param6, param7, context), new CacheParams(), param1, param2, param3, param4, param5, param6, param7);
        }

        public TResult Invoke<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TResult>
            (Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, EntityContext, TResult> f, object instance, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, CacheParams cacheParams)
        {
            return InvokeImpl(f.Method.ToString(), instance, () => f(param1, param2, param3, param4, param5, param6, param7, context), cacheParams, param1, param2, param3, param4, param5, param6, param7);
        }

        public TResult Invoke<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TResult>
            (Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, EntityContext, TResult> f, object instance, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8)
        {
            return InvokeImpl(f.Method.ToString(), instance, () => f(param1, param2, param3, param4, param5, param6, param7, param8, context), new CacheParams(), param1, param2, param3, param4, param5, param6, param7, param8);
        }

        public TResult Invoke<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TResult>
            (Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, EntityContext, TResult> f, object instance, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8, CacheParams cacheParams)
        {
            return InvokeImpl(f.Method.ToString(), instance, () => f(param1, param2, param3, param4, param5, param6, param7, param8, context), cacheParams, param1, param2, param3, param4, param5, param6, param7, param8);
        }

        public void InvalidateItemWithKey(string key)
        {
            Store.InvalidateItemWithKey(key);
        }

        public void InvalidateItemWithKeyFragment(string[] keyFragments)
        {
            Store.InvalidateItemWithKeyFragments(keyFragments);
        }

        public virtual Store GetStore()
        {
            return new InMemoryStore.InMemoryStore();
        }

        public virtual ObjectGraphSerializer GetObjectGraphSerializer()
        {
            return new ObjectGraphSerializer(formatter);
        }
    }
}
