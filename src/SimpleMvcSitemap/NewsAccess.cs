using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    /// <summary>
    /// Specifies if an article can only be accessed with a registration or subscription
    /// </summary>
    public enum NewsAccess
    {
        /// <summary>
        /// Article requires a subscription.
        /// </summary>
        [XmlEnum]
        Subscription,

        /// <summary>
        /// Article requires a registration.
        /// </summary>
        [XmlEnum]
        Registration
    }
}