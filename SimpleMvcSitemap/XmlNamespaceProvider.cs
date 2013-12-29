using System.Collections.Generic;
using System.Linq;

namespace SimpleMvcSitemap
{
    class XmlNamespaceResolver : IXmlNamespaceResolver
    {
        public IEnumerable<XmlSerializerNamespace> GetNamespaces(IEnumerable<SitemapNode> nodes)
        {
            IEnumerable<XmlSerializerNamespace> namespaces = null;

            if (nodes.Any(node => node.ImageDefinition != null))
            {
                namespaces = new List<XmlSerializerNamespace>
                             {
                                 new XmlSerializerNamespace
                                 {
                                     Prefix = SitemapNamespaceConstants.IMAGE_PREFIX,
                                     Namespace = SitemapNamespaceConstants.IMAGE
                                 }
                             };
            }

            return namespaces;
        }
    }
}