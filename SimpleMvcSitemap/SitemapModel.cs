using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SimpleMvcSitemap
{
    [CollectionDataContract(Name = "urlset", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
    internal class SitemapModel : List<SitemapNode>
    {
        public SitemapModel() { }

        public SitemapModel(IEnumerable<SitemapNode> nodeList) : base(nodeList) { }
    }
}