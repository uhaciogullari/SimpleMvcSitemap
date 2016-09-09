using System.Xml.Serialization;

namespace SimpleMvcSitemap.Videos
{
    /// <summary>
    /// The price to download or view the video.
    /// </summary>
    public class VideoPrice
    {
        internal VideoPrice() { }

        /// <summary>
        /// Creates an instance of VideoPrice
        /// </summary>
        /// <param name="currency">Specifies the currency in ISO 4217 format</param>
        /// <param name="value">The price to download or view the video.</param>
        public VideoPrice(string currency, decimal value)
        {
            Currency = currency;
            Value = value;
        }

        /// <summary>
        /// The required attribute currency specifies the currency in ISO 4217 format.
        /// </summary>
        [XmlAttribute("currency")]
        public string Currency { get; set; }


        /// <summary>
        /// The optional attribute type specifies the purchase option. 
        /// Allowed values are rent and own.
        /// If this is not specified, the default value is own.
        /// </summary>
        [XmlAttribute("type")]
        public VideoPurchaseOption Type { get; set; }


        /// <summary>
        /// The optional attribute resolution specifies the purchased resolution. 
        /// Allows values are HD and SD.
        /// </summary>
        [XmlAttribute("resolution")]
        public VideoPurchaseResolution Resolution { get; set; }


        /// <summary>
        /// The price to download or view the video.
        /// </summary>
        [XmlText]
        public decimal Value { get; set; }


        /// <summary>
        /// Used for not serializing null values.
        /// </summary>
        public bool ShouldSerializeType()
        {
            return Type != VideoPurchaseOption.None;
        }

        /// <summary>
        /// Used for not serializing null values.
        /// </summary>
        public bool ShouldSerializeResolution()
        {
            return Resolution != VideoPurchaseResolution.None;
        }
    }
}