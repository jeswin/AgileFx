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
using System.Reflection;

using AgileFx.ORM.Caching;
using AgileFx.ORM.Materialization;
using AgileFx.ORM.Utils;
using AgileFx.ORM.Mapping;
using AgileFx.ORM.QueryCompilation;
using AgileFx.ORM.ObjectComposition;
using AgileFx.ORM.Parallelization;
using AgileFx.ORM.ContextServices;

namespace AgileFx.ORM
{   
    public abstract class EntityContext
    {
        public class InternalServices
        {
            [ThreadStatic]
            public static EntityContext ParentContext;
            [ThreadStatic]
            public static EntityContext OverridingContext;

            EntityContext context;
            
            EntityQueryProvider queryProvider;
            public EntityQueryProvider QueryProvider
            {
                get { return GetEffectiveContext()._InternalServices.queryProvider; }
                set { queryProvider = value; } 
            }

            ITableEntityContext tableEntityContext;
            public ITableEntityContext TableEntityContext
            {
                get { return GetEffectiveContext()._InternalServices.tableEntityContext; }
                set { tableEntityContext = value; }
            }

            //TypeTranslationUtil is not tied to any Context. We don't need to call GetEffectiveContext()
            TypeTranslationUtil typeTranslationUtil;
            public TypeTranslationUtil TypeTranslationUtil
            {
                get { return typeTranslationUtil; }
                set { typeTranslationUtil = value; }
            }

            ObjectTracker objectTracker;
            public ObjectTracker ObjectTracker
            {
                get { return GetEffectiveContext()._InternalServices.objectTracker; }
                set { objectTracker = value; }
            }

            Cache cache;
            public Cache Cache
            {
                get { return GetEffectiveContext()._InternalServices.cache; }
                set { cache = value; }
            }

            ParallelScope parallelScope;
            public ParallelScope ParallelScope
            {
                get { return GetEffectiveContext()._InternalServices.parallelScope; }
                set { parallelScope = value; }
            }

            public InternalServices(EntityContext context)
            {
                this.context = context;
            }

            public EntityContext GetEffectiveContext()
            {
                if (OverridingContext != null)
                    return OverridingContext;
                else
                    return context;
            }

            public static void AssertOverridable()
            {
                if (OverridingContext != null || ParentContext != null)
                    throw new Exception("An overriding context can be set only on a new thread.");
            }
        }

        public InternalServices _InternalServices { get; private set; }
        public string ConnectionString { get; set; }

        public EntityContext(string connectionString)
        {
            this.ConnectionString = connectionString;
            Init();
        }

        private void Init()
        {
            _InternalServices = new InternalServices(this);
            _InternalServices.QueryProvider = GetQueryProvider();
            _InternalServices.ObjectTracker = new ObjectTracker(this);
            _InternalServices.TypeTranslationUtil = GetTypeTranslationUtil();
            _InternalServices.TableEntityContext = GetTableEntityContext();
            _InternalServices.ParallelScope = GetParallelScope();
            _InternalServices.Cache = GetCacheImplementation();
        }

        # region API (Public) Methods

        public T Create<T>(IModification<T> mod) 
            where T : class, IEntity, new()
        {
            var t = mod.ApplyChanges(null, _InternalServices.GetEffectiveContext());
            AddObject(t);
            return t;
        }

        public TElement ApplyChanges<TElement>(object obj, IModification<TElement> mod)
            where TElement : class, IEntity, new()
        {
            var element = mod.ApplyChanges(obj, _InternalServices.GetEffectiveContext());
            if (mod.Type == ModificationType.Add)
                AddObject(element);
            return element;
        }

        public virtual void AddObject(IEntity entity)
        {
            var tableEntity = entity._getIntermediateEntity()._getTableEntity();
            _InternalServices.TableEntityContext.AddObject(tableEntity);

            entity._getIntermediateEntity().EntityContext = _InternalServices.GetEffectiveContext();
        }

        public virtual void DeleteObject(IEntity entity)
        {
            if (entity._getIntermediateEntity().EntityContext != null)
            {
                var tableEntity = entity._getIntermediateEntity()._getTableEntity();
                _InternalServices.TableEntityContext.DeleteObject(tableEntity);
            }

            var entityType = entity.GetType();
            var classMapping = _InternalServices.TypeTranslationUtil.GetMapping<IEntityMapping>(entityType);
                
            var intermediateEntity = entity._getIntermediateEntity();
            var relatedContainers = classMapping.IntermediateEntityType.GetFields()
                .Where(f => typeof(IIntermediateEntityContainer).IsAssignableFrom(f.FieldType))
                .Select(f => f.GetValue(intermediateEntity) as IIntermediateEntityContainer);

            foreach( var container in relatedContainers)
            {
                if (container.IsLoaded)
                    container.RemoveReferenceOnRelatedEntity();
            }

            if (classMapping is IDerivedModelEntityMapping)
            {
                var derivedClassMapping = classMapping as IDerivedModelEntityMapping;
                var baseClassProp = entityType.GetProperty(derivedClassMapping.BaseClassProperty);
                var baseClass = baseClassProp.GetValue(entity, null);
                DeleteObject(baseClass as IEntity);
            }
        }

        public virtual void SaveChanges()
        {
            _InternalServices.TableEntityContext.SaveChanges();
        }

        public virtual void AttachObject(IEntity entity)
        {
            var tableEntity = entity._getIntermediateEntity()._getTableEntity();
            _InternalServices.TableEntityContext.AttachObject(tableEntity);

            entity._getIntermediateEntity().EntityContext = _InternalServices.GetEffectiveContext();
        }

        public virtual void AttachObject(IEntity entity, IEntity original)
        {
            var tableEntity = entity._getIntermediateEntity()._getTableEntity();
            var originalTableEntity = (original as IEntity)._getIntermediateEntity()._getTableEntity();
            _InternalServices.TableEntityContext.AttachObject(tableEntity, originalTableEntity);

            entity._getIntermediateEntity().EntityContext = _InternalServices.GetEffectiveContext();
        }

        public virtual void AttachObject(IEntity entity, bool asModified)
        {
            var tableEntity = entity._getIntermediateEntity()._getTableEntity();
            _InternalServices.TableEntityContext.AttachObject(tableEntity, asModified);

            entity._getIntermediateEntity().EntityContext = _InternalServices.GetEffectiveContext();
        }

        public virtual IEntityQuery<T> CreateQuery<T>()
        {
            return _InternalServices.QueryProvider.CreateQuery<T>();
        }

        public virtual void AttachContext(IntermediateEntity obj)
        {
            if (obj.EntityContext == null)
                obj.EntityContext = _InternalServices.GetEffectiveContext();
        }

        public virtual ResultListMaterializer<T> GetResultListMaterializer<T>()
        {
            return new ResultListMaterializer<T>(_InternalServices.GetEffectiveContext());
        }

        public virtual SingleObjectMaterializer<T> GetSingleObjectMaterializer<T>()
        {
            return new SingleObjectMaterializer<T>(_InternalServices.GetEffectiveContext());
        }

        public abstract QueryCompiler GetQueryCompiler();
        
        public abstract EntityContext CreateNew();

        #endregion

        # region internal methods

        protected abstract ITableEntityContext GetTableEntityContext();
        protected abstract EntityQueryProvider GetQueryProvider();
        
        protected virtual ParallelScope GetParallelScope()
        {
            return new ParallelScope(_InternalServices.GetEffectiveContext());
        }

        protected virtual Cache GetCacheImplementation()
        {
            return new Cache(_InternalServices.GetEffectiveContext());
        }

        protected virtual TypeTranslationUtil GetTypeTranslationUtil()
        {
            return new TypeTranslationUtil();
        }

        protected virtual IEntity GetContextFreeEntity(IEntity source)
        {
            return source;
        }

        #endregion
    }
}
