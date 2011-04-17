using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace taskometer.Core.Models
{
    public abstract class TemplatePlaceHolder
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public TemplatePlaceHolder(string name, string type)
        {
            this.Name = name;
            this.Type = type;
        }

        public TemplatePlaceHolder()
        {
        }
    }

    public class SingleValuePlaceHolder : TemplatePlaceHolder
    {
        public SingleValuePlaceHolder(string name)
            : base(name, TEMPLATE_PLACEHOLDER_TYPE.SINGLE_VALUE)
        {
        }

        public SingleValuePlaceHolder() : base() { }
    }

    public class MultiLinePlaceHolder : TemplatePlaceHolder
    {
        public MultiLinePlaceHolder(string name)
            : base(name, TEMPLATE_PLACEHOLDER_TYPE.MULTI_LINE)
        {
        }

        public MultiLinePlaceHolder() : base() { }
    }

    public class ListPlaceHolder : TemplatePlaceHolder
    {
        public string[] ListValues { get; set; }
        public ListPlaceHolder(string name, string[] values)
            : base(name, TEMPLATE_PLACEHOLDER_TYPE.LIST)
        {
            this.ListValues = values;
        }

        public ListPlaceHolder() : base() { }
    }

    [XmlInclude(typeof(SingleValuePlaceHolder))]
    [XmlInclude(typeof(MultiLinePlaceHolder))]
    [XmlInclude(typeof(ListPlaceHolder))]
    public class TemplatePlaceHolderCollection : List<TemplatePlaceHolder>
    {
        public TemplatePlaceHolderCollection() : base() { }

        public TemplatePlaceHolderCollection(IEnumerable<TemplatePlaceHolder> collection) : base(collection) { }
    }
}
