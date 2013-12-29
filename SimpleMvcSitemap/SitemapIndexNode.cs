using System;
using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    public class SitemapIndexNode : IHasUrl
    {
        [XmlElement("loc", Order = 1)]
        public string Url { get; set; }

        [XmlElement("lastmod", Order = 2)]
        public DateTime? LastModificationDate { get; set; }
    }
}