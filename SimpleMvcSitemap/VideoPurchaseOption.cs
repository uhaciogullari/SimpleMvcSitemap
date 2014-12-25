using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    public enum VideoPurchaseOption
    {
        None,
        
        [XmlEnum("rent")]
        Rent,

        [XmlEnum("own")]
        Own
    }
}