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
using System.Data.Linq;
using System.Reflection;
using AgileFx.ORM.ObjectComposition;

namespace AgileFx.ORM.Backends.LinqToSql
{
    public class DataContextWrapper : ITableEntityContext
    {
        public DataContext LinqToSqlContext { get; set; }

        public DataContextWrapper(DataContext linqToSqlContext)
        {
            this.LinqToSqlContext = linqToSqlContext;
        }

        #region ITableEntityContext Members

        public void DeleteObject(ITableEntity tableEntity)
        {
            var method = Get_GetTable_Method(tableEntity.GetType());

            var table = method.Invoke(LinqToSqlContext, null) as ITable;
            table.DeleteOnSubmit(tableEntity);
        }

        public void SaveChanges()
        {
            LinqToSqlContext.SubmitChanges();
        }

        public void AddObject(ITableEntity tableEntity)
        {
            var method = Get_GetTable_Method(tableEntity.GetType());

            var table = method.Invoke(LinqToSqlContext, null) as ITable;
            table.InsertOnSubmit(tableEntity);
        }

        public void AttachObject(ITableEntity tableEntity)
        {
            var method = Get_GetTable_Method(tableEntity.GetType());

            var table = method.Invoke(LinqToSqlContext, null) as ITable;
            table.Attach(tableEntity);
        }

        public void AttachObject(ITableEntity tableEntity, ITableEntity original)
        {
            var method = Get_GetTable_Method(tableEntity.GetType());

            var table = method.Invoke(LinqToSqlContext, null) as ITable;
            table.Attach(tableEntity, original);
        }

        public void AttachObject(ITableEntity tableEntity, bool asModified)
        {
            var method = Get_GetTable_Method(tableEntity.GetType());

            var table = method.Invoke(LinqToSqlContext, null) as ITable;
            table.Attach(tableEntity, asModified);
        }

        #endregion
     
        public IQueryable<T> CreateQuery<T>()
            where T : class
        {
            return LinqToSqlContext.GetTable<T>();
        }

        private MethodInfo Get_GetTable_Method(Type tableEntityType)
        {
            var method = (from m in LinqToSqlContext.GetType().GetMethods()
                          where m.Name == "GetTable" && m.IsGenericMethod
                          select m).First()
              .GetGenericMethodDefinition().MakeGenericMethod(tableEntityType);

            return method;
        }
    }
}
