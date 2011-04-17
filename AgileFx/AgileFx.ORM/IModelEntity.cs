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
using System.Runtime.Serialization;
using AgileFx.ORM.ObjectComposition;

namespace AgileFx.ORM
{
    public interface IModelEntity : IEntity, ISerializable
    {
    }

    public interface IModelEntity<TIntermediateEntity> : IEntity<TIntermediateEntity>, IModelEntity
        where TIntermediateEntity : IntermediateEntity
    {
    }
}
