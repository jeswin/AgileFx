/* AgileFx Version 2.0
 * An open-source framework for rapid development of applications and services using Microsoft.net
 * Developed by: AgileHead
 * Website: www.agilefx.org
 * This component is licensed under the terms of the Apache 2.0 License.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgileFx.ORM.Caching
{
    public class CacheParams
    {
        public string ItemKey { get; set; }

        private TimeSpan timeout = new TimeSpan(0, 5, 0);
        public TimeSpan Timeout
        {
            get { return timeout; }
            set { timeout = value; }
        }
    }
}
