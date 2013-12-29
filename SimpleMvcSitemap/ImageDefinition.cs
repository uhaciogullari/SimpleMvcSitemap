using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    public class ImageDefinition
    {
        [XmlElement("caption", Order = 1, Namespace = SitemapNamespaceConstants.IMAGE)]
        public string Caption { get; set; }

        [XmlElement("title", Order = 2, Namespace = SitemapNamespaceConstants.IMAGE)]
        public string Title { get; set; }

        [XmlElement("loc", Order = 3, Namespace = SitemapNamespaceConstants.IMAGE)]
        public string Url { get; set; }
    }
}
