using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace taskometer.Core.Models
{
    public class ContentBlock
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public ContentBlock() { }

        public ContentBlock(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }
    }

    public class PageContents : List<ContentBlock>
    {
        public void Add(string name, string value)
        {
            this.Add(new ContentBlock(name, value));
        }
    }
}
