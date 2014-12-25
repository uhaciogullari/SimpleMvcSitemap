using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    public class VideoPrice
    {
        [XmlAttribute("currency")]
        public string Currency { get; set; }

        [XmlAttribute("type")]
        public VideoPurchaseOption Type { get; set; }

        [XmlAttribute("resolution")]
        public VideoPurchaseResolution Resolution { get; set; }

        [XmlText]
        public decimal Value { get; set; }


        public bool ShouldSerializeType()
        {
            return Type != VideoPurchaseOption.None;
        }

        public bool ShouldSerializeResolution()
        {
            return Resolution != VideoPurchaseResolution.None;
        }
    }
}