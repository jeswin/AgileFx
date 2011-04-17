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

namespace AgileFx.ORM.ModificationTypes
{
    public class EntityUpdateFunc<TElement, TContainer>
        where TElement : class, new()
        where TContainer : class, new()
    {
        Action<TElement> updateFunc;
        Action<TElement, TContainer> updateFuncReferencingParent;

        public EntityUpdateFunc(Action<TElement> func)
        {
            updateFunc = func;
        }

        public EntityUpdateFunc(Action<TElement, TContainer> func)
        {
            updateFuncReferencingParent = func;
        }

        public void Run(object container, TElement element)
        {
            if (updateFunc != null)
                   updateFunc(element);
            else if (updateFuncReferencingParent != null)
                updateFuncReferencingParent(element, container as TContainer);
        }
    }
}
