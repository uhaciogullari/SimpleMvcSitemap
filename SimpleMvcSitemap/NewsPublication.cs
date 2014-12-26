using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    [XmlRoot("url", Namespace = Namespaces.News)]
    public class NewsPublication
    {
        /// <summary>
        /// Name of the news publication.
        /// It must exactly match the name as it appears on your articles in news.google.com, omitting any trailing parentheticals.
        /// For example, if the name appears in Google News as "The Example Times (subscription)", you should use the name, "The Example Times".
        /// </summary>
        [XmlElement("name")]
        public string Name { get; set; }

        
        /// <summary>
        /// The &lt;language&gt; is the language of your publication.
        /// It should be an ISO 639 Language Code (either 2 or 3 letters).
        /// Exception: For Chinese, please use zh-cn for Simplified Chinese or zh-tw for Traditional Chinese.
        /// </summary>
        [XmlElement("language")]
        public string Language { get; set; }
    }
}