using System.Collections.Generic;

namespace SimpleMvcSitemap
{
    internal interface IXmlNamespaceResolver
    {
        IEnumerable<XmlSerializerNamespace> GetNamespaces(IEnumerable<SitemapNode> nodes);
    }
}