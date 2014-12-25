using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    public class VideoUploader
    {
        [XmlAttribute("info")]
        public string Info { get; set; }

        [XmlText]
        public string Name { get; set; }
    }
}