using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    public enum YesNo
    {
        None,
        
        [XmlEnum("yes")]
        Yes,
        
        [XmlEnum("no")]
        No
    }
}