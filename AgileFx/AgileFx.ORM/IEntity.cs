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
using System.Linq.Expressions;
using AgileFx.ORM.ObjectComposition;

namespace AgileFx.ORM
{
    public interface IEntity
    {
        IntermediateEntity _getIntermediateEntity();
    }

    public interface IEntity<TIntermediateEntity> : IEntity
        where TIntermediateEntity : IntermediateEntity
    {
        TIntermediateEntity _intermediateEntity { get; }
        void _setIntermediateEntity(TIntermediateEntity intermediateEntity);
    }

    public static class IEntityExtensions
    {
        //Compare if two entities are the same.
        public static bool _equals<TIntermediateEntity>(this IEntity<TIntermediateEntity> source, object target)
            where TIntermediateEntity : IntermediateEntity
        {
            if (source == null && target == null) return true;
            else if (source == null || target == null) return false;

            return ((IEntity<TIntermediateEntity>)source)._intermediateEntity == ((IEntity<TIntermediateEntity>)target)._intermediateEntity;
        }
    }
}
