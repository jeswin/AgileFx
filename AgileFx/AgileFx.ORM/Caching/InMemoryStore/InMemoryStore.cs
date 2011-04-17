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

namespace AgileFx.ORM.Caching.InMemoryStore
{
    public class InMemoryStore : Store
    {
        const long CACHE_CLEANUP_INTERVAL = 30000;

        static List<Action> timerCallbacks = new List<Action>();
        static List<Action<string>> invalidationWithKey_Callbacks = new List<Action<string>>();
        static List<Action<string[]>> invalidationWithKeyFragment_Callbacks = new List<Action<string[]>>();

        static List<Dictionary<string, List<MemoizationEntry>>> cacheLists = new List<Dictionary<string,List<MemoizationEntry>>>();
        static InMemoryStore()
        {
            var timer = new System.Threading.Timer(new System.Threading.TimerCallback(CacheCleanup), null, CACHE_CLEANUP_INTERVAL, CACHE_CLEANUP_INTERVAL);
        }

        public override CacheQueryResult<TResult> Query<TResult>(string f, object entity, params object[] parameters)
        {
            return InMemoryCacheSet<TResult>.Query(f, entity, parameters);
        }

        public override void Add<TResult>(SerializedForm<TResult> result, string f, object entity, CacheParams cacheParams, params object[] parameters)
        {
            InMemoryCacheSet<TResult>.Add(f, entity, result, cacheParams, parameters);
        }

        public static void RegisterForCallbacks(Action onTimer, Action<string> onInvalidateWithKey, Action<string[]> onInvalidateWithKeyFragment)
        {
            timerCallbacks.Add(onTimer);
            invalidationWithKey_Callbacks.Add(onInvalidateWithKey);
            invalidationWithKeyFragment_Callbacks.Add(onInvalidateWithKeyFragment);
        }

        public static void CacheCleanup(object state)
        {
            foreach (var action in timerCallbacks)
                action();
        }

        public override void InvalidateItemWithKey(string key)
        {
            foreach (var action in invalidationWithKey_Callbacks)
                action(key);
        }

        public override void InvalidateItemWithKeyFragments(string[] keyFragments)
        {
            foreach (var action in invalidationWithKeyFragment_Callbacks)
                action(keyFragments);
        }
    }
}