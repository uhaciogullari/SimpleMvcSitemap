using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    public class VideoRestriction
    {
        [XmlAttribute("relationship")]
        public VideoRestrictionRelationship Relationship { get; set; }

        [XmlText]
        public string Countries { get; set; }
    }
}