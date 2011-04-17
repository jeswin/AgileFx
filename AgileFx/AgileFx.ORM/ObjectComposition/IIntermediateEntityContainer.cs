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

namespace AgileFx.ORM.ObjectComposition
{
    public interface IIntermediateEntityContainer
    {
        bool IsLoaded { get; }
        
        IntermediateEntity GetParent();
        
        void MaterializationAddReference(object intermediateEntity);

        //The return value indicates if the reverse reference was successfully set.
        //  For example, this could return false when the EntityReference(single) being set is already changed by the user.
        //  This does not happen in the case of collections, where we just add items to the collection.
        bool MaterializationAddReverseReference(object intermediateEntity);
        
        void RemoveReferenceOnRelatedEntity();
    }

    public interface IIntermediateEntityContainer<TParent> : IIntermediateEntityContainer
        where TParent : IntermediateEntity
    {
        TParent Parent { get; }
    }

    public interface IIntermediateEntityContainer<TIntermediateParent, TIntermediateItem> : IIntermediateEntityContainer<TIntermediateParent>
        where TIntermediateParent : IntermediateEntity
        where TIntermediateItem : IntermediateEntity
    {
        void AddReverseReference(TIntermediateItem item);
        void RemoveReverseReference(TIntermediateItem item);
    }

    public static class IIntermediateEntityContainerExtensions
    {
        public static void SetEntityContextOnParent(this IIntermediateEntityContainer container, EntityContext context)
        {
            context.AttachContext(container.GetParent());
        }

        public static void ShareEntityContext(this IIntermediateEntityContainer container, IntermediateEntity entity)
        {
            //You can't share context with a null, right?
            if (entity != null)
            {
                var parent = container.GetParent();

                if (entity.EntityContext != null && parent.EntityContext == null)
                {
                    entity.EntityContext.AttachContext(parent);
                }
                else if (parent.EntityContext != null && entity.EntityContext == null)
                {
                    parent.EntityContext.AttachContext(entity);
                }
            }
        }
    }
}
