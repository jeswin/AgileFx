using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgileFx.ORM.Utils;
using System.Linq.Expressions;
using AgileFx.ORM.QueryAnalysis.MethodTranslation;
using AgileFx.ORM.Reflection;
using IQToolkit;
using AgileFx.ORM.Mapping;
using System.Reflection;
using System.Collections.ObjectModel;
using AgileFx.ORM.QueryAnalysis.TypeTracking;
using System.Collections;
using AgileFx.ORM.ObjectComposition;

namespace AgileFx.ORM.QueryAnalysis
{
    public class QueryTranslationVisitor : IQToolkit.ExpressionVisitor
    {
        public QueryAnalysisContext AnalysisContext { get; private set; } 

        public TypeTranslationUtil TypeTranslationUtil
        {
            get { return AnalysisContext.TypeTranslationUtil; }
        }

        public QueryTranslationVisitor(QueryAnalysisContext context)
        {
            this.AnalysisContext = context;
        }

        public virtual Expression Translate(Expression expression)
        {
            return this.Visit(expression);
        }
        
        public QueryTranslationResult GetTranslatedResults(Expression query)
        {
            return new QueryTranslationResult(this.Translate(query), AnalysisContext);
        }


        protected override Expression VisitMethodCall(MethodCallExpression mce)
        {
            AnalysisContext.CallStack.Push(mce);

            if (mce.Method.IsGenericMethod)
            {
                var translator = TranslatorFinder.GetMethodTranslator(mce.Method);
                if (translator != null)
                {
                    Expression translatedFirstArg;
                    IEnumerable<Expression> otherArgs;

                    //If the method is a static method (Most common case, of Extension Methods)
                    if (mce.Object == null)
                    {
                        translatedFirstArg = base.Visit(mce.Arguments.First());
                        otherArgs = mce.Arguments.Slice(1);
                    }
                    else
                    {
                        translatedFirstArg = base.Visit(mce.Object);
                        otherArgs = mce.Arguments;
                    }

                    //Ask the translator if the source needs to be modified.
                    if (translator.ModifyTranslatedSource(mce, AnalysisContext) && NeedsProjectionModification(mce))
                        translatedFirstArg = ModifyProjection(translatedFirstArg);

                    var result = translator.Translate(mce, translatedFirstArg, otherArgs, AnalysisContext);

                    //Ask the translator if the result needs to be modified.
                    if (translator.ModifyTranslatedResult(mce, AnalysisContext) && NeedsProjectionModification(mce))
                        result = ModifyProjection(result);

                    AnalysisContext.CallStack.Pop();
                    return result;
                }
            }

            //There are two conditions which reach here.
            //  1. A non-generic method.
            //  2. A method which we do not handle. (ie No translator defined.)
            //
            //Examples of 1 - Non-generic method
            //  Linq to Sql supports Contains in this form:
            //      from user in context.Users 
            //      where someList.Contains(user.Id)
            //      select user
            //  In this case, someList.Contains is just a regular Contains defined on List<int>
            AnalysisContext.CallStack.Pop();
            var newMce = Expression.Call(base.Visit(mce.Object), mce.Method, base.VisitExpressionList(mce.Arguments));
            AnalysisContext.QueryableType = GetQueryableType(mce.Type, newMce.Type);
            return newMce;
        }


        protected override Expression VisitConstant(ConstantExpression c)
        {
            Expression result = null;
            
            //If the constant is an IEntityQuery, we need to pass the TableEntityQueryExpression back
            if (c.Value is IEntityQuery)
            {
                this.AnalysisContext.RootEntityQuery = c.Value as IEntityQuery;
                result = (c.Value as IEntityQuery).TableEntityQueryExpression;
            }
            else if (c.Value != null && typeof(IEntity).IsAssignableFrom(c.Type))
            {
                result = GetTableEntityExpression(c);
            }
            else
            {
                result = base.VisitConstant(c);
            }
            AnalysisContext.QueryableType = GetQueryableType(c.Type, result.Type);

            //If there are no method calls and !IgnoreModification, call the Modifier
            if (AnalysisContext.ModifyProjection && AnalysisContext.CallStack.Count == 0)
                result = ModifyProjection(result);

            return result;
        }

        private SimpleType GetQueryableType(Type originalType, Type translatedType)
        {
            if (!TypesUtil.IsNonPrimitiveCollection(originalType))
            {
                return new SimpleType(originalType, translatedType);
            }
            else
            {
                var itemType = TypesUtil.GetGenericArgumentForBaseType(originalType, typeof(IEnumerable<>));
                var translatedItemType = TypesUtil.GetGenericArgumentForBaseType(translatedType, typeof(IEnumerable<>));
                return RuntimeTypes.CreateEnumerable(new SimpleType(itemType, translatedItemType));
            }
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            if (!AnalysisContext.ParameterDictionary.ContainsKey(p))
                throw new NotSupportedException("Parameter not found the list of translated parameters.");

            var item = AnalysisContext.ParameterDictionary[p];
            AnalysisContext.QueryableType = item.Type;
            return item.ReplacementExpression;
        }

        //m.Expression is null in the case of a static member
        protected override Expression VisitMemberAccess(MemberExpression m)
        {
            Expression translatedM = null;
            //Handles Member referencing constant
            if (IsReferencingConstantValue(m))
            {
                translatedM = (typeof(IEntity).IsAssignableFrom(m.Type)) ? GetTableEntityExpression(m) : (Expression)m;
                AnalysisContext.QueryableType = GetQueryableType(m.Type, translatedM.Type);
            }
            //Handles static members
            else if (m.Expression == null)
            {
                translatedM = base.VisitMemberAccess(m) as MemberExpression;
                AnalysisContext.QueryableType = SimpleType.GetMember(AnalysisContext.QueryableType, m, translatedM as MemberExpression,
                    AnalysisContext.TypeTranslationUtil);
            }
            else
            {
                var exp = base.Visit(m.Expression);
                translatedM = TypeTranslationUtil.GetTranslatedMemberExpression(m.Expression.Type, AnalysisContext.QueryableType.GetMemberName(m.Member), exp);
                AnalysisContext.QueryableType = SimpleType.GetMember(AnalysisContext.QueryableType, m, translatedM as MemberExpression,
                    AnalysisContext.TypeTranslationUtil);
            }

            return translatedM;
        }

        private Expression GetTableEntityExpression(Expression expression)
        {
            var intermediateSelector = Expression.Call(expression, typeof(IEntity).GetMethod("_getIntermediateEntity"));
            return Expression.Call(intermediateSelector, typeof(IntermediateEntity).GetMethod("_getTableEntity"));
        }

        protected override Expression VisitLambda(LambdaExpression lambda)
        {
            var parameters = lambda.Parameters.Select(exp => VisitParameter(exp) as ParameterExpression).ToArray();
            return Expression.Lambda(base.Visit(lambda.Body), parameters);
        }

        protected override NewExpression VisitNew(NewExpression nex)
        {
            //Compiler generated Anonymous Types
            if (nex.Constructor.DeclaringType.IsGenericType)
            {
                var args = new List<Expression>();
                var argQueryTypes = new List<SimpleType>();
                foreach (var arg in nex.Arguments)
                {
                    var result = VisitWithNewVisitor(arg, AnalysisContext, false);
                    args.Add(result.TranslatedExpression);
                    argQueryTypes.Add(result.QueryableType);
                }
                
                var argTypes = args.Select(e => e.Type).ToArray();
                var newType = nex.Constructor.DeclaringType.GetGenericTypeDefinition().MakeGenericType(argTypes);

                var ctor = newType.GetConstructor(argTypes);
                if (ctor != null)
                {
                    AnalysisContext.QueryableType = new SimpleType(nex.Type, newType, nex.Constructor, argQueryTypes, null);
                    return Expression.New(ctor, args, newType.GetProperties());
                }
                //If we are here, our generic attempt did not work. (This also means this isn't a compiler generated anonymous type.)
            }
            //Translatable types
            else if (TypeTranslationUtil.GetTranslatedType(nex.Constructor.DeclaringType) != null)
            {
                var translatedType = TypeTranslationUtil.GetTranslatedType(nex.Constructor.DeclaringType);

                var ctorArgs = new List<ExpressionTranslationResult>();
                foreach (var arg in nex.Arguments)
                    ctorArgs.Add(VisitWithNewVisitor(arg, AnalysisContext, false));

                var argTypes = ctorArgs.Select(result => result.QueryableType.Type).ToArray();
                var ctor2 = translatedType.GetConstructor(argTypes);

                AnalysisContext.QueryableType = new SimpleType(nex.Type, translatedType, ctor2,
                    ctorArgs.Select(result => result.QueryableType), null);

                return Expression.New(ctor2, ctorArgs.Select(x => x.TranslatedExpression));
            }

            throw new NotSupportedException(string.Format("Expression involving type '{0}' is not supported.", nex.Type));
        }

        protected override Expression VisitMemberInit(MemberInitExpression init)
        {
            //User Create Types are handled here
            if (AnalysisContext.TypeTranslationUtil.GetTranslatedType(init.NewExpression.Type) == null)
            {
                return VisitNonTranslatedMemberInit(init);
            }
            else
            {
                //Entity types are handled here
                var fields = new Dictionary<MemberInfo, SimpleType>();
                var visitedBindings = new List<MemberBinding>();
                foreach (var binding in init.Bindings)
                {
                    visitedBindings.Add(VisitBinding(binding));
                    fields.Add(binding.Member, AnalysisContext.QueryableType);
                }

                var nex = VisitNew(init.NewExpression);
                foreach (var kvp in fields)
                    AnalysisContext.QueryableType.Fields.Add(kvp.Key, kvp.Value);
                return Expression.MemberInit(nex, visitedBindings);
            }
        }

        protected Expression VisitNonTranslatedMemberInit(MemberInitExpression init)
        {
            if (init.NewExpression.Arguments.Count > 0)
                throw new NotSupportedException("Only parameterless constructor is supported.");

            var memberAssignments = new List<KeyValuePair<MemberInfo, Expression>>();
            var memberQueryTypes = new Dictionary<MemberInfo, SimpleType>();            
            //Currently only considering member assignment bindings
            foreach(var binding in init.Bindings)
            {
                switch(binding.BindingType)
                {
                    case MemberBindingType.Assignment:
                        var result = VisitWithNewVisitor((binding as MemberAssignment).Expression, AnalysisContext, false);
                        memberAssignments.Add(new KeyValuePair<MemberInfo, Expression>(binding.Member, result.TranslatedExpression));
                        memberQueryTypes.Add(binding.Member, result.QueryableType);
                        break;
                    default:
                        throw new NotSupportedException("Only member assignment is supported.");
                }
            }

            var typeArgs = memberAssignments.Select(kvp => kvp.Value.Type).ToList();
            var newType = AnalysisContext.TypeTranslationUtil.GetAnonymousType(typeArgs);
            var nex = Expression.New(newType.GetConstructor(Type.EmptyTypes));

            var nonTranslatableType = new NonTranslatableType(init.NewExpression.Type, newType, 
                init.NewExpression.Constructor, memberQueryTypes);

            var ctr = 0;
            memberAssignments.ForEach(kvp => nonTranslatableType.MemberMap.Add(kvp.Key, newType.GetProperty("Field" + (ctr++))));
            var bindings = memberAssignments.Select(kvp => Expression.Bind(nonTranslatableType.MemberMap[kvp.Key], kvp.Value) as MemberBinding).ToList();

            AnalysisContext.QueryableType = nonTranslatableType;
            return Expression.MemberInit(nex, bindings);
        }

        protected override MemberAssignment VisitMemberAssignment(MemberAssignment assignment)
        {
            var translatedType = TypeTranslationUtil.GetTranslatedType(assignment.Member.DeclaringType);
            var memInfo = translatedType.GetMember(assignment.Member.Name).SingleOrDefault();
            return Expression.Bind(memInfo, base.Visit(assignment.Expression));
        }

        protected override Expression VisitUnary(UnaryExpression u)
        {
            if (u.NodeType == ExpressionType.Convert && TypeTranslationUtil.IsEntity(u.Type))
            {
                var operand = base.Visit(u.Operand);
                if (u.Type == u.Operand.Type)
                    return operand;
                else
                    //Changing Cast/Conversion to base class of entities to MemberExpression 
                    //as there is no inheritance at table level
                    return TypeTranslationUtil.GetMemberExpression(u.Operand.Type, u.Type.Name, operand);
            }
            return base.VisitUnary(u);
        }

        protected override Expression VisitBinary(BinaryExpression b)
        {
            Expression left = VisitWithNewVisitor(b.Left, AnalysisContext, false).TranslatedExpression;
            Expression right = VisitWithNewVisitor(b.Right, AnalysisContext, false).TranslatedExpression;
            LambdaExpression conversion = VisitWithNewVisitor(b.Conversion, AnalysisContext, false).TranslatedExpression as LambdaExpression;
            MethodInfo method = b.Method;

            if ((b.NodeType == ExpressionType.Equal || b.NodeType == ExpressionType.NotEqual)
                && (TypeTranslationUtil.IsEntity(b.Left.Type) || TypeTranslationUtil.IsEntity(b.Right.Type)))
            {
                method = MethodFinder.ObjectMethods.Equals();
            }
            return Expression.MakeBinary(b.NodeType, left, right, b.IsLiftedToNull, method, conversion);
        }

        public static LambdaExpression GetLambda(Expression lambdaArg, bool isQueryable)
        {
            if (isQueryable)
                return (lambdaArg as UnaryExpression).Operand as LambdaExpression;
            else
                return lambdaArg as LambdaExpression;
        }

        public static Expression FormatLambda(LambdaExpression lambda, bool isQueryable)
        {
            if (isQueryable)
                return Expression.Quote(lambda);
            else
                return lambda;
        }

        public static ExpressionTranslationResult TranslateLambda(LambdaExpression lambda, SimpleType[] parameterTypes, QueryAnalysisContext context)
        {
            if (parameterTypes.Length != lambda.Parameters.Count)
                throw new Exception("The number of parameter types must match the number of parameters in the lambda.");

            var newContext = new QueryAnalysisContext(context.TypeTranslationUtil, context.ParameterDictionary);

            //translatedParams are the new parameters that will be used in the lambda.
            var translatedParams = new List<ParameterExpression>();

            for (int i = 0; i < lambda.Parameters.Count; i++)
            {
                var newParam = parameterTypes[i].GetParameter(lambda.Parameters[i].Name);
                var paramReplacement = new ParameterReplacement
                {
                    //replacementExpression will be a MemberExpression if the projection is modified.
                    //  since the original parameter has become Field0.
                    ReplacementExpression = parameterTypes[i].GetTranslatedParameterOrMember(newParam),
                    Type = parameterTypes[i].GetOriginalQueryableType()
                };
                newContext.ParameterDictionary.Add(lambda.Parameters[i], paramReplacement);

                translatedParams.Add(newParam);
            }

            var lambdaBody = new QueryTranslationVisitor(newContext).Translate(lambda.Body);
            var result = Expression.Lambda(lambdaBody, translatedParams.ToArray());

            return new ExpressionTranslationResult(result, newContext.QueryableType);
        }

        public static ExpressionTranslationResult TranslateLambda(Expression lambdaArg, SimpleType[] parameterTypes, QueryAnalysisContext context, bool isQueryable)
        {
            var lambda = GetLambda(lambdaArg, isQueryable);
            var translation = TranslateLambda(lambda, parameterTypes, context);
            return new ExpressionTranslationResult
                (FormatLambda(translation.TranslatedExpression as LambdaExpression, isQueryable), translation.QueryableType);
        }

        public static ExpressionTranslationResult VisitWithNewVisitor(Expression expression, QueryAnalysisContext outerContext, bool modifyProjection)
        {
            var newContext = new QueryAnalysisContext(outerContext.TypeTranslationUtil) { ModifyProjection = modifyProjection };
            foreach (var kvp in outerContext.ParameterDictionary) newContext.ParameterDictionary.Add(kvp.Key, kvp.Value);
            var result = new QueryTranslationVisitor(newContext).Translate(expression);
            return new ExpressionTranslationResult(result, newContext.QueryableType);
        }

        //Is the expression referencing a constant value?
        //(a => a.id == someValue)
        private bool IsReferencingConstantValue(MemberExpression m)
        {
            if (m.Expression is ConstantExpression) return true;
            else if (m.Expression is MemberExpression) return IsReferencingConstantValue(m.Expression as MemberExpression);
            else return false;
        }


        private bool NeedsProjectionModification(Expression currentExpression)
        {
            //Get the T, when the query returns T or IQueryable<T>
            var genericTypeOfResult = typeof(IQueryable).IsAssignableFrom(currentExpression.Type) ?
                TypesUtil.GetGenericArgumentForBaseType(currentExpression.Type, typeof(IQueryable<>)) : currentExpression.Type;

            //If this is the outermost method, and it is a Non-Primitive Type, we need to modify the Projection
            //  so that we can add implicit includes (like Inhertiance) and user-requested Includes (Prefetch Paths)
            //There are two cases:
            //  1) If IQueryable<NonPrimitiveType>, add a Select around translated mce (eg: Where, Select etc)
            //  2) If just NonPrimitiveType, add a Select around translatedFirstArg (eg: First, Single etc)

            return  AnalysisContext.ModifyProjection                             //Don't modify if context.ModifyProjection is false
                    && !(AnalysisContext.QueryableType.NonPrimitiveEnumerableItemType is ProjectedType)            //Don't modify if already modified, that is - context.QueryableType is ProjectedType
                    && !TypesUtil.IsPrimitiveDataType(genericTypeOfResult);         //Don't modify if Primitive Type.
        }

        protected Expression ModifyProjection(Expression expression)
        {
            var projectionModifier = new ProjectionModifier(expression, AnalysisContext.QueryableType, AnalysisContext.TypeTranslationUtil);
            var result = projectionModifier.GetModifiedProjection();
            AnalysisContext.QueryableType = result.QueryableType;
            return result.ModifiedExpression;
        }
    }
}