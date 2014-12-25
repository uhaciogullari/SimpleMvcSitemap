using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    public class VideoRestriction
    {
        /// <summary>
        /// The required attribute "relationship" specifies whether the video is restricted or permitted for the specified countries.
        /// Allowed values are allow or deny.
        /// </summary>
        [XmlAttribute("relationship")]
        public VideoRestrictionRelationship Relationship { get; set; }

        /// <summary>
        /// A space-delimited list of countries where the video may or may not be played. 
        /// Allowed values are country codes in ISO 3166 format. 
        /// </summary>
        [XmlText]
        public string Countries { get; set; }
    }
}