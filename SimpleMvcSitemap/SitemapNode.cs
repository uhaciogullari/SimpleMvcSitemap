using System;
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

        [DataMember(Name = "loc", Order = 1)]
        public string Url { get; set; }

        [DataMember(Name = "lastmod", EmitDefaultValue = false, Order = 2)]
        public DateTime? LastModificationDate { get; set; }

        [DataMember(Name = "changefreq", EmitDefaultValue = false, Order = 3)]
        public ChangeFrequency ChangeFrequency { get; set; }
    }
}