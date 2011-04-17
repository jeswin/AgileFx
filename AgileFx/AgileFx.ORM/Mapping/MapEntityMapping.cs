/* AgileFx Version 2.0
 * An open-source framework for rapid development of applications and services using Microsoft.net
 * Developed by: AgileHead
 * Website: www.agilefx.org
 * This component is licensed under the terms of the Apache 2.0 License.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AgileFx.ORM.Mapping
{
    public interface IMapEntityMapping : IEntityMapping
    {    
        List<ManyToManyMap> GetManyToManyMaps();
        List<ManyToManyKeyRelationship> GetManyToManyKeyRelationships();
    }

    public class MapEntityMapping<TMapEntity, TMapIntermediateEntity, TMapTableEntity> : EntityMapping<TMapEntity, TMapIntermediateEntity, TMapTableEntity>, IMapEntityMapping
    {
        List<ManyToManyMap> manyToManyMaps = new List<ManyToManyMap>();
        List<ManyToManyKeyRelationship> manyToManyKeyRelationships = new List<ManyToManyKeyRelationship>();

        public void AddManyToManyMap<TEntity, TTableEntity>(Expression<Func<TMapEntity, TEntity>> entitySelector, Expression<Func<TTableEntity, IEnumerable<TMapTableEntity>>> mapTableEntitySelector)
        {
            manyToManyMaps.Add(new ManyToManyMap { EntitySelector = entitySelector, MapTableEntitySelector = mapTableEntitySelector });
        }

        public void AddManyToManyKeyRelationship<TMapForeignKey, TEntity, TEntityPrimaryKey>
            (Expression<Func<TMapEntity, TMapForeignKey>> mapFieldSelector, Expression<Func<TEntity, TEntityPrimaryKey>> entityFieldSelector)
        {
            manyToManyKeyRelationships.Add(new ManyToManyKeyRelationship { MapField = mapFieldSelector, EntityField = entityFieldSelector });
        }

        public List<ManyToManyMap> GetManyToManyMaps()
        {
            return manyToManyMaps;
        }

        public List<ManyToManyKeyRelationship> GetManyToManyKeyRelationships()
        {
            return manyToManyKeyRelationships;
        }
    }

    public class ManyToManyMap
    {
        public LambdaExpression EntitySelector { get; set; }
        public LambdaExpression MapTableEntitySelector { get; set; }
    }

    public class ManyToManyKeyRelationship
    {
        public LambdaExpression MapField { get; set; }
        public LambdaExpression EntityField { get; set; }
    }
}
