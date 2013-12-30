using System.Collections.Generic;
using System.Linq;

namespace SimpleMvcSitemap
{
    class XmlNamespaceResolver : IXmlNamespaceResolver
    {
        public IEnumerable<XmlSerializerNamespace> GetNamespaces(IEnumerable<SitemapNode> nodes)
        {
            IEnumerable<XmlSerializerNamespace> namespaces = null;

            if (nodes.Any(node => node.SitemapImage != null))
            {
                namespaces = new List<XmlSerializerNamespace>
                             {
                                 new XmlSerializerNamespace
                                 {
                                     Prefix = Namespaces.ImagePrefix,
                                     Namespace = Namespaces.Image
                                 }
                             };
            }

            return namespaces;
        }
    }
}