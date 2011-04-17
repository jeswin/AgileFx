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
using System.Text;
using AgileFx.ORM.QueryAnalysis;

namespace AgileFx.ORM.Materialization
{
    public class PostProjectionLoadedResult
    {
        public IUnprojectedBinding ProjectionBinding { get; set; }
        public IList Value { get; set; }
    }
}
