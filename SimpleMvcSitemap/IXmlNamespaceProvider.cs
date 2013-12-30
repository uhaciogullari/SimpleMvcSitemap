using System.Collections.Generic;

namespace SimpleMvcSitemap
{
    interface IXmlNamespaceProvider
    {
        IEnumerable<string> GetNamespaces();
    }
}