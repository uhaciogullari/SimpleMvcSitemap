using System.Collections.Generic;

namespace SimpleMvcSitemap.Serialization
{
    interface IXmlNamespaceProvider
    {
        /// <summary>
        /// Gets the XML namespaces.
        /// Exposed for XML serializer.
        /// </summary>
        IEnumerable<string> GetNamespaces();
    }
}