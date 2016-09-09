using System.Xml.Serialization;

namespace SimpleMvcSitemap.Videos
{
    /// <summary>
    /// Specifies if a video may or may not be played for the given countries.
    /// </summary>
    public enum VideoRestrictionRelationship
    {
        /// <summary>
        /// Video can be played in specified countries.
        /// </summary>
        [XmlEnum("allow")]
        Allow,

        /// <summary>
        /// Video can't be played in specified countries.
        /// </summary>
        [XmlEnum("deny")]
        Deny
    }
}