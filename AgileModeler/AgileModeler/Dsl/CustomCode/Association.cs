using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgileFx.AgileModeler
{
    public partial class Association
    {
        private string GetMultiplicityDisplay(Multiplicity multiplicity)
        {
            switch (multiplicity)
            {
                case Multiplicity.One:
                    return "1";
                case Multiplicity.ZeroOne:
                    return "0..1";
                case Multiplicity.ZeroMany:
                    return "*";
                default:
                    throw new NotSupportedException();
            }
        }

        public string GetEnd1MultiplicityDisplayValue()
        {
            return GetMultiplicityDisplay(this.End1Multiplicity);
        }

        public string GetEnd2MultiplicityDisplayValue()
        {
            return GetMultiplicityDisplay(this.End2Multiplicity);
        }
    }
}
