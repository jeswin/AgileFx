using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgileFx.AgileModeler.DslPackage.CustomCode
{
    class ModelerTransaction
    {
        public static bool IsInTransaction { get; private set; }

        public static void Enter(Action code)
        {
            bool oldVal = IsInTransaction;

            IsInTransaction = true;
            try
            {
                code();
            }
            finally
            {
                IsInTransaction = oldVal;
            }
        }
    }
}
