using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace AgileFx.ORM.ContextServices
{
    public interface IObjectGraph
    {
        bool IsAnonymous { get; }

        bool IsCollection { get; }
        
        object GetInstance();
        
        Type GetInstanceType();
        
        PropertyInfo[] GetPropertiesNeedingContextBinding();

        object GetPropertyValue(PropertyInfo property);

        //Gets the list of object graph.. Only for a collection
        List<IObjectGraph> GetSourceCollection();

        //Get the Serialized Object Graph corresponding to a property, if the property is not a collection.
        IObjectGraph GetObjectGraphForProperty(PropertyInfo property);

        //Get the Serialized Object Graph corresponding to a property, if the property is a collection.
        IEnumerable<IObjectGraph> GetObjectGraphForCollection(PropertyInfo property);
    }
}
