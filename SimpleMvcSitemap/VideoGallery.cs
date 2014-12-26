using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    public class VideoGallery
    {
        /// <summary>
        /// The optional attribute title indicates the title of the gallery.
        /// </summary>
        [XmlAttribute("title")]
        public string Title { get; set; }

        /// <summary>
        /// A link to the gallery (collection of videos) in which this video appears
        /// </summary>
        [XmlText, Url]
        public string Url { get; set; }
    }
}