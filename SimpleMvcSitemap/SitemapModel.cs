using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    [XmlRoot("urlset", Namespace = Namespaces.Sitemap)]
    public class SitemapModel
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
    }
}