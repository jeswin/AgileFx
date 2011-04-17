using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using AgileFx.ORM.Utils;
using AgileFx.ORM.Mapping;
using System.Collections;
using System.Reflection;
using AgileFx.ORM.QueryAnalysis.TypeTracking;

namespace AgileFx.ORM.QueryAnalysis
{
    public class ProjectionModification
    {
        public SimpleType QueryableType { get; set; }
        public Expression ModifiedExpression { get; set; }
    }

    public class ProjectionModifier
    {
        SimpleType queryableType;
        Expression currentExpression;
        TypeTranslationUtil typeTranslationUtil;

        public ProjectionModifier(Expression currentExpression, SimpleType queryableType, TypeTranslationUtil typeTranslationUtil)
        {
            this.queryableType = queryableType;
            this.currentExpression = currentExpression;
            this.typeTranslationUtil = typeTranslationUtil;
        }

        public ProjectionModification GetModifiedProjection()
        {
            //There will be no includes or inheritance bindings on primitive types (like int, long, guid etc)
            //We can just return the current expression, and QueryableType.
            if (TypesUtil.IsPrimitiveDataType(currentExpression.Type))
                return new ProjectionModification { ModifiedExpression = currentExpression, QueryableType = queryableType };

            var parameter = Expression.Parameter(queryableType.NonPrimitiveEnumerableItemType.TranslatedType, "p");

            var queryBindings = GetQueryBindings(queryableType.NonPrimitiveEnumerableItemType, parameter);
            var includeBindings = GetIncludeBindings(queryBindings);

            var requestedBindings = new List<IRequestedBinding>();
            queryBindings.ForEach(b => requestedBindings.Add(b));
            includeBindings.ForEach(b => requestedBindings.Add(b));

            var inheritanceBindings = GetInheritanceBindings(requestedBindings);
            var unprojectedBindings = GetUnprojectedBindings(requestedBindings);

            var relatedBindings = new List<IRelatedBinding>();
            //Add all related bindings to the context.
            //We don't need QueryBindings since they are just a wrapped around QueryableTypes
            includeBindings.Where(inc => !(inc is UnprojectedIncludeInCollectionBinding)).ToList()
                .ForEach(inc => relatedBindings.Add(inc));
            inheritanceBindings.ForEach(b => relatedBindings.Add(b));

            //All set. Create the new Select Expression.
            return CreateMethodCallExpression(parameter, relatedBindings, unprojectedBindings);
        }

        private ProjectionModification CreateMethodCallExpression(ParameterExpression parameter, List<IRelatedBinding> relatedBindings, List<IUnprojectedBinding> unprojectedBindings)
        {
            //We will create an AnonymousType(this type is part of AgileFx), to hold the new projection.
            var typeArgs = new List<Type>();
            typeArgs.Add(queryableType.NonPrimitiveEnumerableItemType.TranslatedType);
            relatedBindings.ForEach(binding => typeArgs.Add(binding.TranslatedType));
            var anonType = typeTranslationUtil.GetAnonymousType(typeArgs.ToArray());

            var ctor = anonType.GetConstructor(Type.EmptyTypes);
            var newExpression = Expression.New(ctor);

            //Fields in the Projected Type
            var fieldsInProjectedType = new Dictionary<MemberInfo, SimpleType>();
            //The first field is the original type. This goes in as Field0.
            fieldsInProjectedType.Add(anonType.GetProperties()[0], queryableType.NonPrimitiveEnumerableItemType);

            var memberBindings = new List<MemberBinding>();
            //The first field is the original type. This goes in as Field0.
            memberBindings.Add(Expression.Bind(anonType.GetProperties()[0], parameter));
            
            int propCounter = 1;
            var anonTypeProperties = anonType.GetProperties();
            foreach (var binding in relatedBindings)
            {
                var propInfo = anonTypeProperties[propCounter];
                memberBindings.Add(Expression.Bind(propInfo, binding.TranslatedExpression));
                fieldsInProjectedType.Add(propInfo, null);
                propCounter++;
            }

            var memberInit = Expression.MemberInit(newExpression, memberBindings);
            var lambda = Expression.Lambda(memberInit, parameter);


            var projectedType = new ProjectedType(queryableType.NonPrimitiveEnumerableItemType.Type, anonType, ctor, null, fieldsInProjectedType,
                                                        relatedBindings, unprojectedBindings);
            
            Func<IQueryable<object>, Expression<Func<object, object>>, IQueryable<object>> f = Queryable.Select;
            var selectMethod = f.Method.GetGenericMethodDefinition().MakeGenericMethod(queryableType.NonPrimitiveEnumerableItemType.TranslatedType, anonType);            
            var mce = Expression.Call(null, selectMethod, currentExpression, lambda);

            return new ProjectionModification { ModifiedExpression = mce, QueryableType = RuntimeTypes.CreateEnumerable(projectedType) };
        }

        private List<QueryBinding> GetQueryBindings(SimpleType qType, Expression inner)
        {
            var result = new List<QueryBinding>();

            //Create a binding which wraps the queryable type.
            var queryBinding = new QueryBinding { Value = qType, TranslatedExpression = inner };
            result.Add(queryBinding);

            //If this is a complex type, we need to recursively go through the fields.
            //eg of complex type is: new { U = user, A = user.Account }
            //  We are trying to traverse U and A in the example above.
            var members = qType.GetAllMembers();
            if (members.Count > 0)
                foreach (var kvp in members)
                    result.AddRange(GetQueryBindings(kvp.Value, typeTranslationUtil.GetTranslatedMemberExpression(qType.Type, qType.GetMemberName(kvp.Key), inner)));

            return result;
        }

        private List<MemberExpression> GetMembers(MemberExpression memberExp)
        {
            var members = new List<MemberExpression>();
            members.Add(memberExp);
            while (memberExp.Expression is MemberExpression)
            {
                memberExp = memberExp.Expression as MemberExpression;
                members.Add(memberExp);
            }
            members.Reverse();
            return members;
        }

        private List<IncludeBinding> GetIncludeBindings(IEnumerable<QueryBinding> queryBindings)
        {
            var bindings = new List<IncludeBinding>();

            Func<MemberExpression, IncludeBinding> getExistingBinding =
                m => bindings.Find(id => ExpressionUtil.IsEquivalent(id.IncludeDirective.GetSelector().Body, m));

            Func<MemberExpression, IncludeDirective> getIncludeDirective = m => 
            {
                var member = m.GetDepth() == 1 ? m :
                    Expression.MakeMemberAccess(Expression.Parameter(m.Expression.Type, m.Expression.Type.Name.Substring(0, 1).ToLower()), m.Member);
                return IncludeDirectiveUtil.GetIncludeDirective(member);
            };

            foreach (var qb in queryBindings)
            {
                //Allowing not included collections to be used for collection includes
                foreach (var include in qb.Value.Includes)//.Where(inc => !(inc is IIncludeInCollectionDirective)))
                {
                    IRequestedBinding targetBinding = qb;
                    foreach (MemberExpression member in GetMembers(include.GetSelector().Body as MemberExpression))
                    {
                        var existingBinding = getExistingBinding(member);
                        if (existingBinding != null)
                        {
                            targetBinding = existingBinding;
                        }
                        else
                        {
                            var includeDirective = getIncludeDirective(member);
                            var currentMemberName = (targetBinding is QueryBinding) 
                                ? (targetBinding as QueryBinding).Value.GetMemberName(member.Member) : member.Member.Name;
                            var binding = new IncludeBinding()
                            {
                                IncludeDirective = includeDirective,
                                TargetBinding = targetBinding,
                                TranslatedExpression = typeTranslationUtil.GetTranslatedMemberExpression
                                    (member.Expression.Type, currentMemberName, targetBinding.TranslatedExpression)
                            };
                            bindings.Add(binding);
                            targetBinding = binding;
                        }
                    }
                }
                foreach (var collInclude in qb.Value.Includes.Where(inc => inc is IIncludeInCollectionDirective))
                {
                    var collectionSelector = collInclude.GetSelector().Body as MemberExpression;

                    var target = getExistingBinding(collectionSelector);
                    if (target != null)
                    {
                        bindings.Add(new UnprojectedIncludeInCollectionBinding
                        {
                            IncludeDirective = collInclude,
                            TargetBinding = target
                        });
                    }
                }
            }
            return bindings;
        }

        private List<IRelatedBinding> GetInheritanceBindings(IEnumerable<IRequestedBinding> requestedBindings)
        {
            var result = new List<IRelatedBinding>();

            foreach (var target in requestedBindings.Where(b => !TypesUtil.IsNonPrimitiveCollection(b.Type)))
            {
                //Not a collection. Add Inheritance Bindings if there are any.
                var inheritanceExpressions = GetInheritanceExpressions(target.Type, target.TranslatedExpression);
                foreach (var expression in inheritanceExpressions)
                {
                    var binding = new InheritanceBinding
                    {
                        TargetBinding = target,
                        TranslatedExpression = expression
                    };

                    result.Add(binding);
                }
            }

            return result;
        }

        private List<IUnprojectedBinding> GetUnprojectedBindings(IEnumerable<IRequestedBinding> requestedBindings)
        {
            var result = new List<IUnprojectedBinding>();

            foreach (var target in requestedBindings.Where(b => TypesUtil.IsNonPrimitiveCollection(b.Type)))
            {
                if (target is UnprojectedIncludeInCollectionBinding)
                {
                    result.Add(target as IUnprojectedBinding);
                }
                //If the target is a collection, and the type of collection is inherited, we will do its loading
                //  via "Post-Projection Loader". Which reloads the inheritance chain by issuing a new query.
                //We will not handle this case here, instead just mark this for post projection loading.
                else if (TypesUtil.IsNonPrimitiveCollection(target.Type))
                {
                    if (IsManyToManyBinding(target))
                    {
                        var unprojected = new UnprojectedManyToManyBinding();
                        unprojected.TargetBinding = target;
                        result.Add(unprojected);
                    }
                    else
                    {
                        if (IsDerivedCollection(target.Type))
                        {
                            var unprojected = new UnprojectedCollectionBinding();
                            unprojected.TargetBinding = target;
                            result.Add(unprojected);
                        }
                    }
                }
            }

            return result;
        }

        private bool IsDerivedCollection(Type CollectionType)
        {
            var itemType = TypesUtil.GetGenericArgumentForBaseType(CollectionType, typeof(IEnumerable<>));

            //if entityType is a non-entity, it will not have any mapping.
            return (typeTranslationUtil.GetMapping<IModelEntityMapping>(itemType) is IDerivedModelEntityMapping);
        }

        private bool IsManyToManyBinding(IRequestedBinding target)
        {
            if (target is IncludeBinding)
            {
                var include = target as IncludeBinding;
                var memberExpression = include.IncludeDirective.GetSelector().Body as MemberExpression;
                return IsManyToMany(memberExpression.Member);
            }
            else if (target is QueryBinding)
            {
                var queryType = (target as QueryBinding).Value;
                return (queryType.NonPrimitiveEnumerableItemType != null && queryType.NonPrimitiveEnumerableItemType is ManyToManyMapType);
            }
            else
                return false;
        }

        private bool IsManyToMany(MemberInfo memberInfo)
        {
            var classMapping = typeTranslationUtil.GetMapping<IModelEntityMapping>(memberInfo.DeclaringType);

            //Getting the many-to-many maps for the declaringType
            return (classMapping != null && classMapping.GetManyToManyRelationships()
                .Any(m => ((MemberExpression)m.RelatedEntitySelector.Body).Member.Name == memberInfo.Name));
        }

        //Returns a list of properties in the table entity inheritance chain.
        //eg: Lawyer : Professional : User will return ["Professional", "User"]
        private Stack<Expression> GetInheritanceExpressions(Type entityType, Expression baseExpression)
        {
            var mapping = typeTranslationUtil.GetMapping<IModelEntityMapping>(entityType);

            //if entityType is a non-entity, it will not have any mapping.            
            if (mapping is IDerivedModelEntityMapping)
            {
                var derivedMapping = mapping as IDerivedModelEntityMapping;
                var derivedExpression = Expression.Property(baseExpression, derivedMapping.BaseClassProperty);
                var inheritances = GetInheritanceExpressions(entityType.BaseType, derivedExpression);
                inheritances.Push(derivedExpression);
                return inheritances;
            }
            else //mapping is just IModelEntityMapping
            {
                return new Stack<Expression>();
            }
        }
    }
}