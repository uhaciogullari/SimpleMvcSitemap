using System;
using System.Runtime.Serialization;

namespace SimpleMvcSitemap
{
    [DataContract(Name = "sitemap", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
    internal class SitemapIndexNode
    {
        public SitemapIndexNode() { }

        [DataMember(Name = "loc", Order = 1)]
        public string Url { get; set; }

        [DataMember(Name = "lastmod", EmitDefaultValue = false, Order = 2)]
        public DateTime? LastModificationDate { get; set; }
    }
}