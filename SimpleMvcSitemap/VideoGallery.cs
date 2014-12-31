using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    public class VideoGallery
    {
        internal VideoGallery() { }

        /// <summary>
        /// Creates an instance of video gallery
        /// </summary>
        /// <param name="url">A link to the gallery (collection of videos) in which this video appears</param>
        public VideoGallery(string url)
        {
            Url = url;
        }

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