using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SimpleMvcSitemap
{
    [CollectionDataContract(Name = "sitemapindex", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
    internal class SitemapIndexModel : List<SitemapIndexNode>
    {
        public SitemapIndexModel() { }

        public SitemapIndexModel(IEnumerable<SitemapIndexNode> indexNodeList) : base(indexNodeList) { }
    }
}