using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgileFx.AgileModeler
{   
    public static class MultiplicityExtensions
    {
        public static string ToText(this Multiplicity multiplicity)
        {
            if (multiplicity == Multiplicity.One) return "1 (One)";
            if (multiplicity == Multiplicity.ZeroOne) return "0..1 (Zero or One)";
            if (multiplicity == Multiplicity.ZeroMany) return "* (Many)";

            throw new Exception("Unsupported multiplicity");
        }
    }
}
