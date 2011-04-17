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
    public interface ITableEntity
    {
        IntermediateEntity _getIntermediateEntity();
        void _setIntermediateEntity(IntermediateEntity intermdiateEntity);
    }

    public interface ITableEntity<TIntermediateEntity> : ITableEntity
        where TIntermediateEntity : IntermediateEntity
    {
        TIntermediateEntity _intermediateEntity { get; set; }
    }
}
