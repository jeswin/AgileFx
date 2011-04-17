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

using AgileFx.ORM.Serialization;
using AgileFx.ORM.Utils;
using AgileFx.ORM.ContextServices;

namespace AgileFx.ORM.Parallelization
{
    public abstract class ParallelWorkItem
    {
        protected ParallelScope scope;
        public abstract void ThreadEntry();
        public abstract void InvokeCallBack();

        public ParallelWorkItem(ParallelScope scope)
        {
            this.scope = scope;
        }
    }
    public class ParallelWorkItem<TResult> : ParallelWorkItem
    {
        public Func<TResult> Method { get; set; }
        public Action<TResult> Callback { get; set; }
        public TResult results { get; set; }

        public ParallelWorkItem(Func<TResult> method, Action<TResult> callback, ParallelScope scope)
            : base(scope)
        {
            this.Method = method;
            this.Callback = callback;
        }

        public override void ThreadEntry()
        {
            EntityContext.InternalServices.AssertOverridable();

            EntityContext.InternalServices.ParentContext = scope.EntityContext;
            EntityContext.InternalServices.OverridingContext = scope.EntityContext.CreateNew();

            try
            {
                results = Method.Invoke();
            }
            finally
            {
                EntityContext.InternalServices.OverridingContext = null;
                EntityContext.InternalServices.ParentContext = null;
            }
        }

        public override void InvokeCallBack()
        {
            var contextBinder = new ContextBinder(scope.EntityContext);
            //Merge the results back into the parent context.
            var equivalentObjects = (TResult)contextBinder.SwitchContext(results);
            Callback(equivalentObjects);
        }

        private bool NeedsContextSwitch(object o)
        {
            return o is IEntity;
        }
    }
}
