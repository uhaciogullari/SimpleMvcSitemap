using System.Runtime.Serialization;

namespace SimpleMvcSitemap
{
    [DataContract(Name = "sitemap", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
    internal class SitemapIndexNode
    {
        public SitemapIndexNode() { }

        [DataMember(Name = "loc")]
        public string Url { get; set; }
    }
}