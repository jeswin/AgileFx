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
    public abstract class MemoizationEntry
    {
        public object Instance { get; set; }
        public string FuncId { get; set; }
        public object[] Params { get; set; }
        public CacheParams CacheParams { get; set; }
        public DateTime TimeAdded { get; set; }
    }

    public class MemoizationEntry<TResult> : MemoizationEntry
    {
        public SerializedForm<TResult> Result { get; set; }
    }
}
