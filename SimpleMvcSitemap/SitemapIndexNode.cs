using System;
using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    [XmlRoot("sitemap", Namespace = Namespaces.Sitemap)]
    public class SitemapIndexNode : IHasUrl
    {
        internal SitemapIndexNode() { }

        public SitemapIndexNode(string url)
        {
            Url = url;
        }

        [XmlElement("loc", Order = 1)]
        public string Url { get; set; }

        [XmlElement("lastmod", Order = 2)]
        public DateTime? LastModificationDate { get; set; }

        public bool ShouldSerializeUrl()
        {
            return Url != null;
        }

        public bool ShouldSerializeLastModificationDate()
        {
            return LastModificationDate != null;
        }

    }
}