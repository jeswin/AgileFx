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
using System.Collections;

using AgileFx.ORM.Utils;

namespace AgileFx.ORM.Mapping
{
    public interface IModelEntityMapping : IEntityMapping
    {
        IDictionary GetRelationships();
        List<ManyToManyRelationship> GetManyToManyRelationships();
    }

    public interface IDerivedModelEntityMapping : IModelEntityMapping
    {
        string BaseClassProperty { get; }
        Type BaseTableEntityType { get; }
    }

    public class ModelEntityMapping<TEntity, TIntermediateEntity, TTableEntity> : EntityMapping<TEntity, TIntermediateEntity, TTableEntity>, IModelEntityMapping
    {
        Dictionary<LambdaExpression, LambdaExpression> relationships = new Dictionary<LambdaExpression, LambdaExpression>();
        List<ManyToManyRelationship> manyToManyRelationships = new List<ManyToManyRelationship>();


        public void AddRelationship<TField>(Expression<Func<TEntity, TField>> relationship,
            Expression<Func<TField, TEntity>> reverseRelationship)
        {
            relationships.Add(relationship, reverseRelationship);
        }

        public void AddRelationship<TField>(Expression<Func<TEntity, ICollection<TField>>> relationship,
            Expression<Func<TField, TEntity>> reverseRelationship)
        {
            relationships.Add(relationship, reverseRelationship);
        }

        public void AddRelationship<TField>(Expression<Func<TEntity, ICollection<TField>>> relationship,
            Expression<Func<TField, ICollection<TEntity>>> reverseRelationship)
        {
            relationships.Add(relationship, reverseRelationship);
        }

        public void AddRelationship<TField>(Expression<Func<TEntity, TField>> relationship,
            Expression<Func<TField, ICollection<TEntity>>> reverseRelationship)
        {
            relationships.Add(relationship, reverseRelationship);
        }

        public void AddManyToManyRelationship<TRelatedEntity, TMapEntity>(Expression<Func<TEntity, IEnumerable<TRelatedEntity>>> relatedEntitySelector,
            Expression<Func<TMapEntity, TEntity>> selfSelectorFromMap, Expression<Func<TMapEntity, TRelatedEntity>> relatedEntitySelectorFromMap)
        {
            manyToManyRelationships.Add(new ManyToManyRelationship<TMapEntity> { RelatedEntitySelector = relatedEntitySelector, SelfSelectorFromMap = selfSelectorFromMap, RelatedEntitySelectorFromMap = relatedEntitySelectorFromMap });
        }

        public IDictionary GetRelationships()
        {
            return relationships;
        }

        public List<ManyToManyRelationship> GetManyToManyRelationships()
        {
            return manyToManyRelationships;
        }
    }

    public class ModelEntityMapping<TDerivedEntity, TDerivedIntermediateEntity, TDerivedTableEntity, TBaseTableEntity>
        : ModelEntityMapping<TDerivedEntity, TDerivedIntermediateEntity, TDerivedTableEntity>, IDerivedModelEntityMapping
    {
        public Expression<Func<TDerivedTableEntity, TBaseTableEntity>> BaseClassField { get; set; }

        public ModelEntityMapping(Expression<Func<TDerivedTableEntity, TBaseTableEntity>> baseClassField)
        {
            BaseClassField = baseClassField;
        }

        public string BaseClassProperty
        {
            get 
            {
                return ExpressionUtil.GetPropertyName(BaseClassField);
            }
        }

        public Type BaseTableEntityType
        {
            get
            {
                return typeof(TBaseTableEntity);
            }
        }
    }

    public abstract class ManyToManyRelationship
    {
        public LambdaExpression RelatedEntitySelector { get; set; }
        public LambdaExpression SelfSelectorFromMap { get; set; }
        public LambdaExpression RelatedEntitySelectorFromMap { get; set; }

        public abstract Type MapType { get; }
    }

    public class ManyToManyRelationship<TMapEntity> : ManyToManyRelationship
    {
        public override Type MapType { get { return typeof(TMapEntity); } }
    }
}
