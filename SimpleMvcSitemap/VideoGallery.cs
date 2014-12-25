using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    public class VideoGallery
    {
        [XmlAttribute("title")]
        public string Title { get; set; }

        [XmlText]
        public string Url { get; set; }
    }
}