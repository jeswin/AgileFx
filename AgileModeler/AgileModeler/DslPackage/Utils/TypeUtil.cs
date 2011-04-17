using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgileFx.AgileModeler.DslPackage.Lib
{
    public static class TypeUtil
    {
        public static IEnumerable<string> GetBuiltInTypes()
        {
            return new string[] { "Boolean", "Byte", "DateTime", "Decimal", "Double", 
                                    "Guid", "Int16", "Int32", "Int64", "Single", "String" };
        }
    }
}
