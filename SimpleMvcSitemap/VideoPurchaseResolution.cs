using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    public enum VideoPurchaseResolution
    {
        None,
        
        [XmlEnum("hd")]
        Hd,

        [XmlEnum("sd")]
        Sd
    }
}