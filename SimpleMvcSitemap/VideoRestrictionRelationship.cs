using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    public enum VideoRestrictionRelationship
    {
        [XmlEnum("allow")]
        Allow,

        [XmlEnum("deny")]
        Deny
    }
}