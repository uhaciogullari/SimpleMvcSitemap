using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    [XmlRoot("urlset", Namespace = Namespaces.Sitemap)]
    public class SitemapModel : IXmlNamespaceProvider
    {
        private readonly IEnumerable<SitemapNode> _nodeList;

        internal SitemapModel() { }

        public SitemapModel(IEnumerable<SitemapNode> sitemapNodes)
        {
            _nodeList = sitemapNodes ?? Enumerable.Empty<SitemapNode>();
        }

        [XmlElement("url")]
        public List<SitemapNode> Nodes
        {
            get { return _nodeList.ToList(); }
        }

        public IEnumerable<string> GetNamespaces()
        {
            List<string> namespaces = new List<string> { Namespaces.Sitemap };

            if (Nodes.Any(node => node.Images != null && node.Images.Any()))
            {
                namespaces.Add(Namespaces.Image);
            }

            if (Nodes.Any(node => node.News != null))
            {
                namespaces.Add(Namespaces.News);
            }
            if (Nodes.Any(node => node.Video != null))
            {
                namespaces.Add(Namespaces.Video);
            }

            return namespaces;
        }
    }
}