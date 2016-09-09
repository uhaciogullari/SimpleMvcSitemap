using System.Collections.Generic;

namespace SimpleMvcSitemap.Serialization
{
    interface IXmlNamespaceProvider
    {
        IEnumerable<string> GetNamespaces();
    }
}