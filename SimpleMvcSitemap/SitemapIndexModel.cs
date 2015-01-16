using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    /// <summary>
    /// Encapsulates information about all of the Sitemaps in the file.
    /// </summary>
    [XmlRoot("sitemapindex", Namespace = Namespaces.Sitemap)]
    public class SitemapIndexModel
    {
        private List<SitemapIndexNode> _nodeList;
        
        public SitemapIndexModel() { }
        
        public SitemapIndexModel(IEnumerable<SitemapIndexNode> sitemapIndexNodes)
        {
            _nodeList = sitemapIndexNodes.ToList();
        }

        [XmlElement("sitemap")]
        public List<SitemapIndexNode> Nodes
        {
            get { return _nodeList; }
        }

    }
}