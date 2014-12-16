using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    public class VideoPlayerUrl
    {
        [XmlAttribute("allow_embed")]
        public YesNo AllowEmbed { get; set; }

        [XmlAttribute("autoplay")]
        public string Autoplay { get; set; }

        [XmlText]
        public string Url { get; set; }
    }
}