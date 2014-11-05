using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    [XmlRoot("url", Namespace = Namespaces.News)]
    public class SitemapNewsPublication
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("language")]
        public string Language { get; set; }
    }
}