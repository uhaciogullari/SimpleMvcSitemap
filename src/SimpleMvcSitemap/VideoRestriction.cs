using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    /// <summary>
    /// List of countries where the video may or may not be played.
    /// </summary>
    public class VideoRestriction
    {
        internal VideoRestriction() { }

        /// <summary>
        /// Creates an instance of VideoRestriction
        /// </summary>
        /// <param name="countries">A space-delimited list of countries where the video may or may not be played.
        /// Allowed values are country codes in ISO 3166 format.</param>
        /// <param name="relationship">Specifies whether the video is restricted or permitted for the specified countries.</param>
        public VideoRestriction(string countries, VideoRestrictionRelationship relationship)
        {
            Countries = countries;
            Relationship = relationship;
        }

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