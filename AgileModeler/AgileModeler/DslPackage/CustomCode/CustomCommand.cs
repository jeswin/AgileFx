using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgileFx.AgileModeler.DslPackage
{
    public enum CommandType
    {
        UpdateDbSchema = 1,
        AddEntity,
        AddAssociation
    }

    public class ActionCommand
    {
        public CommandType Type { get; set; }
        public Guid CommandGuid { get; set; }
        public int CommandId { get; set; }
    }
}
