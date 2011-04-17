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
using AgileFx.ORM.Mapping;

namespace AgileFx.ORM.ObjectComposition
{
    public interface IPOCOContainer
    {
        bool IsLoaded { get; }
    }

    [Serializable]
    public class POCOKeyMember
    {
        public POCOKeyMember() { }
        public POCOKeyMember(string key, object value) 
        {
            this.Key = key;
            this.Value = value;
        }

        public string Key { get; set; }
        public object Value { get; set; }
    }

    [Serializable]
    public class POCOReference : IPOCOContainer
    {
        public POCOReference() { }

        public POCOReference(object entity, bool isLoaded)
        {
            this.IsLoaded = isLoaded;
            if (entity != null)
            {
                var identities = new TypeTranslationUtil().GetMapping<IModelEntityMapping>(entity.GetType()).GetIdentityFields();
                var keyMembers = new List<POCOKeyMember>();
                foreach (LambdaExpression exp in identities)
                {
                    var prop = ExpressionUtil.GetProperty(exp, entity.GetType());
                    keyMembers.Add(new POCOKeyMember(prop.Name, prop.GetValue(entity, null)));
                }
                this.POCOKeyMembers = keyMembers.ToArray();
            }
        }

        public POCOKeyMember[] POCOKeyMembers { get; private set; }
        public bool IsLoaded { get; private set; }
    }

    [Serializable]
    public class POCOCollection : List<POCOReference>, IPOCOContainer
    {
        public bool IsLoaded { get; private set; }

        public POCOCollection() { }

        public POCOCollection(IEnumerable entityCollection, bool isLoaded)
        {
            this.IsLoaded = isLoaded;
            foreach (var entity in entityCollection) this.Add(new POCOReference(entity, true));
        }
    }
}
