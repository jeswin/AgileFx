using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Practices.RepositoryFactory.SchemaDiscovery.CommonHelpers
{
    /// <summary>
    /// Represents a class for helper naming stuff
    /// </summary>
    public static class NamingHelper
    {
        /// <summary>
        /// Escapes the name of the object.
        /// </summary>
        /// <param name="objectName">Name of the object.</param>
        /// <returns></returns>
        public static string EscapeObjectName(string objectName)
        {
            if (objectName == null)
                throw new ArgumentNullException("objectName");
            
            return objectName.Replace("]", "]]");
        }
    }
}
