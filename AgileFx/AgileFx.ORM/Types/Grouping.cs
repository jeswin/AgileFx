using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgileFx.ORM.Types
{
    public class Grouping<TKey, TElement> : List<TElement>, IGrouping<TKey, TElement>
    {
        public TKey Key { get; set; }
    }
}
