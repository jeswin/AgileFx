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

using AgileFx.ORM.Materialization;
using AgileFx.ORM.Utils;
using System.Reflection;
using AgileFx.ORM.ContextServices;

namespace AgileFx.ORM.Serialization
{
    [Serializable]
    public abstract class SerializableObjectGraph : IObjectGraph
    {
        Dictionary<PropertyInfo, SerializableObjectGraph> includedMembers 
            = new Dictionary<PropertyInfo, SerializableObjectGraph>();
        public Dictionary<PropertyInfo, SerializableObjectGraph> IncludedMembers
        {
            get { return includedMembers; } 
        }

        //private List<MemberData> _IncludedMembers = new List<MemberData>();
        //public List<MemberData> IncludedMembers
        //{
        //    get { return _IncludedMembers; }
        //    set { _IncludedMembers = value; }
        //}

        public abstract bool IsAnonymous { get; }

        public abstract bool IsCollection { get; }

        public abstract object GetInstance();

        public Type InstanceType { get; set; }
        public Type GetInstanceType()
        {
            return InstanceType;
        }

        public abstract List<IObjectGraph> GetSourceCollection();

        //All included members need context binding
        public PropertyInfo[] GetPropertiesNeedingContextBinding()
        {
            return IncludedMembers.Keys.ToArray();
        }

        public object GetPropertyValue(PropertyInfo property)
        {
            return IncludedMembers.ContainsKey(property) ?
                IncludedMembers[property].GetInstance() : null;
        }

        public IObjectGraph GetObjectGraphForProperty(System.Reflection.PropertyInfo property)
        {
            return IncludedMembers.ContainsKey(property) ?
                IncludedMembers[property] : null;
        }

        public IEnumerable<IObjectGraph> GetObjectGraphForCollection(System.Reflection.PropertyInfo property)
        {
            return IncludedMembers.ContainsKey(property) ?
                ((SerializableCollection)IncludedMembers[property]).Collection : null;
        }
    }
}
