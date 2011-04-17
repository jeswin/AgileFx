using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgileFx.ORM
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class TypeFactoryAttribute : Attribute
    {
        public Type FactoryType { get; private set; }

        public TypeFactoryAttribute(Type factoryType)
        {
            this.FactoryType = factoryType;
        }
    }
}
