/* AgileFx Version 2.0
 * An open-source framework for rapid development of applications and services using Microsoft.net
 * Developed by: AgileHead
 * Website: www.agilefx.org
 * This component is licensed under the terms of the Apache 2.0 License.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AgileFx.ORM
{
    public interface IPartialResultset
    {
        int TotalCount { get; set; }
        int StartIndex { get; set; }
        int MaxItems { get; set; }
        int GetCurrentPage();
    }

    public class PartialResultset<T> : List<T>, IPartialResultset
    {
        public PartialResultset(IEnumerable<T> values, int startIndex, int totalCount, int maxItems) : base(values)
        {
            StartIndex = startIndex;
            TotalCount = totalCount;
            MaxItems = maxItems;
        }

        #region IPartialEntityList Members

        public int TotalCount
        {
            get;
            set;
        }

        public int StartIndex
        {
            get;
            set;
        }

        public int MaxItems
        {
            get;
            set;
        }

        public int GetCurrentPage()
        {
            return (StartIndex - 1) / MaxItems + 1;
        }

        #endregion
    }
}