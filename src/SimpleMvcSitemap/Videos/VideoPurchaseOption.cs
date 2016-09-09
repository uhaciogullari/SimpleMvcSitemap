using System.Xml.Serialization;

namespace SimpleMvcSitemap.Videos
{
    /// <summary>
    /// Purchase option for a video.
    /// </summary>
    public enum VideoPurchaseOption
    {
        /// <summary>
        /// Default value, won't be serialized.
        /// </summary>
        None,

        /// <summary>
        /// Video can be rented.
        /// </summary>
        [XmlEnum("rent")]
        Rent,

        /// <summary>
        /// Video can be owned
        /// </summary>
        [XmlEnum("own")]
        Own
    }
}