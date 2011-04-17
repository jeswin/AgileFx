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

namespace AgileFx.ORM.Types
{
    public class Binary
    {
        public byte[] data;

        public Binary(byte[] data)
        {
            this.data = data;
        }

        public byte[] ToArray()
        {
            return data;
        }
    }
}
