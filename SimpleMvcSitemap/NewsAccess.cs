using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    public enum NewsAccess
    {
        [XmlEnum]
        Subscription,

        [XmlEnum]
        Registration
    }
}