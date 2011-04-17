using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Modeling;

namespace AgileFx.AgileModeler.DslPackage.CustomCode.DomainUtils
{
    static class InheritanceUtil
    {
        public static void AddInheritance(Store store, string baseClassName, string derivedClassName)
        {
            ModelerTransaction.Enter(() =>
                {
                    using (Transaction tx = store.TransactionManager.BeginTransaction())
                    {
                        var entity = store.ElementDirectory.FindElements<ModelClass>()
                                .First(m => m.Name == derivedClassName);

                        var baseClass = store.ElementDirectory.FindElements<ModelClass>()
                                .First(m => m.Name == baseClassName);

                        AddInheritance(baseClass, entity);

                        tx.Commit();
                    }
                });
        }

        public static void AddInheritance(ModelClass baseClass, ModelClass entity)
        {
            var inheritance = new Inheritance(baseClass, entity);
            var baseClassPrimarykeyField = entity.Baseclass.Fields.Single(f => f.IsPrimaryKey);
            
            //The derived class primary key will be the same as the base class's.
            //However, if the derived class has its own different primary key, link them. (Untested)
            var derivedClassPrimarykeyField = entity.Fields.Find(f => f.IsPrimaryKey);
            
            inheritance.BaseClassPrimaryKeyColumn = baseClassPrimarykeyField.ColumnName;
            inheritance.DerivedClassPrimaryKeyColumn = derivedClassPrimarykeyField == null ? baseClassPrimarykeyField.ColumnName : derivedClassPrimarykeyField.ColumnName;            
        }
    }
}
