using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    [XmlRoot("sitemap", Namespace = Namespaces.Sitemap)]
    public class SitemapIndexNode : IHasUrl, IXmlNamespaceProvider
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

        IEnumerable<string> IXmlNamespaceProvider.GetNamespaces()
        {
            return new List<string> { Namespaces.Sitemap };
        }

    }
}