using System.Runtime.Serialization;

namespace SimpleMvcSitemap
{
    [DataContract(Name = "url", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
    public class SitemapNode
    {
        internal SitemapNode() { }

        public SitemapNode(string url)
        {
            Url = url;
        }

        [DataMember(Name = "loc")]
        public string Url { get; set; }
    }
}