using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    public class VideoUploader
    {
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