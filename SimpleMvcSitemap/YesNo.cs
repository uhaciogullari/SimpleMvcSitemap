using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    public enum YesNo
    {
        [XmlEnum("yes")]
        Yes,
        
        [XmlEnum("no")]
        No
    }
}