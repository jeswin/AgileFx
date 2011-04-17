using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace AgileFx.ORM
{
    public interface ITypeFactory
    {
        //Return null if the factory does not want to construct this type.
        Type GetTypeOf(object instance);
    }

    public abstract class TypeFactory<TBase> : ITypeFactory
    {
        public Type GetTypeOf(object instance)
        {
            return GetTypeOf((TBase)instance);
        }

        public abstract Type GetTypeOf(TBase instance);

        public abstract Expression GetPredicateForCreateQuery();
    }
}