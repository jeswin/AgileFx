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
using System.Threading;
using AgileFx.ORM.Serialization;

namespace AgileFx.ORM.Caching.InMemoryStore
{
    public class InMemoryCacheSet<TResult>
    {
        static ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();

        //This contains the cached results. A result-list is stored against each method, 
        //  with each value in the list being a set of parameters, and the return-value.
        //  When a cache query comes in, it is matched against the method (name), and the full list of parameters.
        //  If they all match, the corresponding return-value is returned.
        static Dictionary<string, List<MemoizationEntry<TResult>>> MemoizationTable 
            = new Dictionary<string, List<MemoizationEntry<TResult>>>();

        static InMemoryCacheSet()
        {
            InMemoryStore.RegisterForCallbacks(DiscardTimedoutEntries, InvalidateWitKey, InvalidateWithKeyFragments);
        }

        public static void Add(string funcId, object entity, SerializedForm<TResult> result, CacheParams cacheParams, params object[] parameters)
        {
            rwLock.EnterWriteLock();
            try
            {
                if (!MemoizationTable.ContainsKey(funcId))
                    MemoizationTable.Add(funcId, new List<MemoizationEntry<TResult>>());

                MemoizationTable[funcId].Add(new MemoizationEntry<TResult>
                {
                    FuncId = funcId,
                    Instance = entity,
                    Result = result,
                    Params = parameters,
                    CacheParams = cacheParams,
                    TimeAdded = DateTime.Now
                });
            }
            finally
            {
                rwLock.ExitWriteLock();
            }
        }

        public static CacheQueryResult<TResult> Query(string funcId, object entity, params object[] parameters)
        {
            rwLock.EnterReadLock();
            try
            {
                if (MemoizationTable.ContainsKey(funcId))
                {
                    var cachedResults = MemoizationTable[funcId];

                    foreach (MemoizationEntry<TResult> result in cachedResults)
                    {
                        if (result.Instance == entity)
                        {
                            var parametersMatched = true;
                            var paramArray = parameters.ToArray();
                            for (int i = 0; i < paramArray.Length; i++)
                            {
                                if (!paramArray[i].Equals(result.Params[i]))
                                {
                                    parametersMatched = false;
                                    break;
                                }
                            }

                            if (parametersMatched)
                            {
                                return new CacheQueryResult<TResult>
                                {
                                    Found = true,
                                    SerializedForm = result.Result
                                };
                            }
                        }
                    }
                }
                return new CacheQueryResult<TResult> { Found = false };
            }
            finally
            {
                rwLock.ExitReadLock();
            }
        }

        public static void DiscardTimedoutEntries()
        {
            rwLock.EnterWriteLock();
            try
            {
                var cleanUpStartTime = DateTime.Now;

                var entriesToRemove = new List<string>();
                foreach (var item in MemoizationTable)
                {
                    var methodsToRemove = new List<MemoizationEntry>();
                    foreach (var methodData in item.Value)
                    {
                        if ((cleanUpStartTime - methodData.TimeAdded) > methodData.CacheParams.Timeout)
                            methodsToRemove.Add(methodData);
                    }
                    item.Value.RemoveAll(m => methodsToRemove.Contains(m));

                    if (item.Value.Count == 0)
                        entriesToRemove.Add(item.Key);
                }
                entriesToRemove.ForEach(e => MemoizationTable.Remove(e));
            }
            finally
            {
                rwLock.ExitWriteLock();
            }
        }

        public static void InvalidateWitKey(string key)
        {
            rwLock.EnterWriteLock();
            try
            {
                foreach (var item in MemoizationTable)
                {
                    var methodsToRemove = new List<MemoizationEntry>();
                    foreach (var methodData in item.Value)
                    {
                        if (methodData.CacheParams.ItemKey == key)
                            methodsToRemove.Add(methodData);
                    }
                    item.Value.RemoveAll(m => methodsToRemove.Contains(m));
                }
            }
            finally
            {
                rwLock.ExitWriteLock();
            }
        }


        public static void InvalidateWithKeyFragments(string[] keyFragments)
        {
            var fragments = keyFragments.ToList();
            rwLock.EnterWriteLock();
            try
            {
                foreach (var item in MemoizationTable)
                {
                    var methodsToRemove = new List<MemoizationEntry>();
                    foreach (var methodData in item.Value)
                    {                       
                        if (fragments.All(fragment => methodData.CacheParams.ItemKey.Contains(fragment)))
                            methodsToRemove.Add(methodData);
                    }
                    item.Value.RemoveAll(m => methodsToRemove.Contains(m));
                }
            }
            finally
            {
                rwLock.ExitWriteLock();
            }
        }
    }
}
