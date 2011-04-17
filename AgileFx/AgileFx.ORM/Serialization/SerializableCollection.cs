using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgileFx.ORM.ContextServices;

namespace AgileFx.ORM.Serialization
{
    [Serializable]
    public class SerializableCollection : SerializableObjectGraph
    {
        public List<IObjectGraph> Collection { get; set; }

        public override bool IsAnonymous
        {
            get { return false; }
        }

        public override bool IsCollection
        {
            get { return true; }
        }

        public override object GetInstance()
        {
            var items = Collection.Select(og => og.GetInstance()).ToList();
            return ContextBinder.GetCollectionInstance(InstanceType, items);
        }

        public override List<IObjectGraph> GetSourceCollection()
        {
            return Collection;
        }
    }
}
