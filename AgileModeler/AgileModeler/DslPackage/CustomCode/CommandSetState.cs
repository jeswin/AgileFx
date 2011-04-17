using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.VisualStudio.Modeling.Shell;

namespace AgileFx.AgileModeler.DslPackage.CustomCode
{
    public class CommandSetState
    {
        public ICollection CurrentSelection { get; set; }
        public DiagramDocView CurrentDocView { get; set; }
    }
}
