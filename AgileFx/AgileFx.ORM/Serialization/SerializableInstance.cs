using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgileFx.ORM.Utils;

namespace AgileFx.ORM.Serialization
{
    [Serializable]
    public class SerializableInstance : SerializableObjectGraph
    {
        public object Object { get; set; }

        public override bool IsAnonymous
        {
            get { return (InstanceType != null) && TypesUtil.IsCompilerGeneratedAnonymousType(InstanceType); }
        }

        public override bool IsCollection
        {
            get { return false; }
        }

        public override object GetInstance()
        {
            return Object;
        }

        public override List<AgileFx.ORM.ContextServices.IObjectGraph> GetSourceCollection()
        {
            throw new InvalidOperationException("The given object graph is not a collection");
        }
    }
}
