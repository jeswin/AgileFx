using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using AgileFx.AgileModeler.CustomCode.TypeDescriptors;

namespace AgileFx.AgileModeler.DslPackage
{
    partial class AgileModelerPackage
    {
        protected override void Initialize()
        {
            TypeDescriptorProvider.RegisterTypes();

            base.Initialize();
        }
    }
}
