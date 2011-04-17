using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using AgileFx.ORM.ContextServices;

namespace AgileFx.ORM.Serialization
{
    //This class is a container for an ObjectGraph (IObjectGraph),
    //  but also contains Type information and methods for deserialization.
    public class SerializedForm<T>
    {
        public byte[] serializedObjectGraph;

        public SerializedForm(byte[] serializedObjectGraph)
        {
            this.serializedObjectGraph = serializedObjectGraph;
        }

        public T Deserialize(ContextBinder binder, IFormatter formatter, EntityContext context)
        {
            IObjectGraph objectGraph;
            using (var ms = new System.IO.MemoryStream(serializedObjectGraph))
            {
                objectGraph = formatter.Deserialize(ms) as IObjectGraph;
            }
            var contextBinder = new ContextBinder(context);
            return (T)contextBinder.AttachGraphToContext<T>(objectGraph);
        }
    }
}
