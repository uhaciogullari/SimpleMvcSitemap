using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    [XmlRoot("sitemapindex", Namespace = Namespaces.Sitemap)]
    public class SitemapIndexModel
    {
        private IEnumerable<SitemapIndexNode> _nodeList;
        
        public SitemapIndexModel() { }
        
        public SitemapIndexModel(IEnumerable<SitemapIndexNode> sitemapIndexNodes)
        {
            _nodeList = sitemapIndexNodes;
        }

        [XmlElement("sitemap")]
        public List<SitemapIndexNode> Nodes
        {
            get { return _nodeList.ToList(); }
        }
    }
}