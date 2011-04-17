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
using System.Linq.Expressions;

namespace AgileFx.ORM.Serialization
{
    class MaterializationIncludes
    {
        public MemberExpression FieldChain { get; set; }
        public IEnumerable<MemberExpression> IncludesInCollection { get; set; }

        public MaterializationIncludes(MemberExpression fieldChain, IEnumerable<MemberExpression> includesInCollection)
        {
            this.FieldChain = fieldChain;
            this.IncludesInCollection = includesInCollection;
        }
    }
}
