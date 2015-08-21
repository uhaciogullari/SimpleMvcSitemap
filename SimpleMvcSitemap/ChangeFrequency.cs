using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    /// <summary>
    /// Change frequency of the linked document
    /// </summary>
    public enum ChangeFrequency
    {
        /// <summary>
        /// The value "always" should be used to describe documents that change each time they are accessed.
        /// </summary>
        [XmlEnum("always")]
        Always,

        /// <summary>
        /// Hourly change
        /// </summary>
        [XmlEnum("hourly")]
        Hourly,

        /// <summary>
        /// Daily change
        /// </summary>
        [XmlEnum("daily")]
        Daily,

        /// <summary>
        /// Weekly change
        /// </summary>
        [XmlEnum("weekly")]
        Weekly,

        /// <summary>
        /// Monthly change
        /// </summary>
        [XmlEnum("monthly")]
        Monthly,

        /// <summary>
        /// Yearly change
        /// </summary>
        [XmlEnum("yearly")]
        Yearly,

        /// <summary>
        /// The value "never" should be used to describe archived URLs.
        /// </summary>
        [XmlEnum("never")]
        Never
    }
}