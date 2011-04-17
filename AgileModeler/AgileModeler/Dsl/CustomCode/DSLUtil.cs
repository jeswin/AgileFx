using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Modeling;

namespace AgileFx.AgileModeler.CustomCode
{
    public class DSLUtil
    {
        public static IEnumerable<string> GetClasses(Store store)
        {
            return store.ElementDirectory.FindElements<ModelClass>().Select(c => c.Name);
        }

        public static IEnumerable<string> GetMultiplicityTextValues()
        {
            return new[] { "1 (One)", "0..1 (Zero or One)", "* (Many)" };
        }

        public static Multiplicity ToMultiplicity(string text)
        {
            if (text == "1 (One)") return Multiplicity.One;
            if (text == "0..1 (Zero or One)") return Multiplicity.ZeroOne;
            if (text == "* (Many)") return Multiplicity.ZeroMany;

            throw new Exception("Unsupported multiplicity");
        }
    }
}
