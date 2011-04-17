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
using System.Collections;

namespace AgileFx.ORM.Parallelization
{
    public class ParallelScope
    {
        public object executionLock = new object();

        public EntityContext EntityContext { get; private set; }
        List<ParallelWorkItem> workItems = new List<ParallelWorkItem>();

        public ParallelScope(EntityContext entityContext)
        {
            this.EntityContext = entityContext;
        }

        public void Invoke()
        {
            lock (executionLock)
            {
                var activeThreads = new List<Thread>();
                foreach (var item in workItems)
                {
                    var threadEntry = new ThreadStart(item.ThreadEntry);
                    var thread = new Thread(threadEntry);
                    thread.Start();
                    activeThreads.Add(thread);
                }
                activeThreads.ForEach(t => t.Join());

                foreach (var item in workItems)
                {
                    item.InvokeCallBack();
                }

                workItems.Clear();
            }
        }

        public void AddWorkItem<TResult>(Func<TResult> method, Action<TResult> callback)
        {
            lock (executionLock)
            {
                workItems.Add(new ParallelWorkItem<TResult>(method, callback, this));
            }
        }
    }
}