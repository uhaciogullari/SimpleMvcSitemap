using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    public enum ChangeFrequency
    {
        /// <summary>
        /// The value "always" should be used to describe documents that change each time they are accessed.
        /// </summary>
        [XmlEnum("always")]
        Always,

        [XmlEnum("hourly")]
        Hourly,

        [XmlEnum("daily")]
        Daily,

        [XmlEnum("weekly")]
        Weekly,

        [XmlEnum("monthly")]
        Monthly,

        [XmlEnum("yearly")]
        Yearly,

        /// <summary>
        /// The value "never" should be used to describe archived URLs.
        /// </summary>
        [XmlEnum("never")]
        Never
    }
}