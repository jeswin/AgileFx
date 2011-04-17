using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgileFx.ORM.Tests
{
    class ArgForGetGenericArgumentForBaseType : ArgForGetGenericArgumentForBaseTypeBase<string>
    {
    }

    class GenericArgForGetGenericArgumentForBaseType<T> : ArgForGetGenericArgumentForBaseTypeBase<T>
    {
    }

    class ArgForGetGenericArgumentForBaseTypeBase<T> : IEnumerable<int>
    {
        #region IEnumerable<int> Members

        public IEnumerator<int> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
