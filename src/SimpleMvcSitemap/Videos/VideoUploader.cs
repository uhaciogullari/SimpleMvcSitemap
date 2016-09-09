using System.Xml.Serialization;
using SimpleMvcSitemap.Routing;

namespace SimpleMvcSitemap.Videos
{
    /// <summary>
    /// Info about the uploader of the video.
    /// </summary>
    public class VideoUploader
    {
        internal VideoUploader() { }

        /// <summary>
        /// Creates an instance of VideoUploader
        /// </summary>
        /// <param name="name">The video uploader's name.</param>
        public VideoUploader(string name)
        {
            Name = name;
        }

        /// <summary>
        /// The optional attribute info specifies the URL of a webpage with additional information about this uploader.
        /// This URL must be on the same domain as the &lt;loc&gt; tag.
        /// </summary>
        [XmlAttribute("info"), Url]
        public string Info { get; set; }


        /// <summary>
        /// The video uploader's name.
        /// </summary>
        [XmlText]
        public string Name { get; set; }
    }
}