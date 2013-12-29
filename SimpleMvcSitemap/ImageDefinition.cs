using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    public class ImageDefinition
    {
        [XmlElement("caption", Order = 1)]
        public string Caption { get; set; }

        [XmlElement("title", Order = 2)]
        public string Title { get; set; }

        [XmlElement("loc", Order = 3)]
        public string Url { get; set; }
    }
}
