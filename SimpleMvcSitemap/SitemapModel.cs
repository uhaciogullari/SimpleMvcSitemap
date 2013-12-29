using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    [XmlRoot("urlset", Namespace = SitemapNamespaceConstants.SITEMAP)]
    public class SitemapModel
    {
        private readonly IEnumerable<SitemapNode> _nodeList;
        public SitemapModel() { }

        public SitemapModel(IEnumerable<SitemapNode> sitemapNodes)
        {
            _nodeList = sitemapNodes;
        }

        [XmlElement("url")]
        public List<SitemapNode> Nodes
        {
            get { return _nodeList.ToList(); }
        }
    }
}