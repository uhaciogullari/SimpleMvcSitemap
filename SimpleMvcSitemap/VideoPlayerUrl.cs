using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    /// <summary>
    /// Encapsulates the information about player URL
    /// </summary>
    public class VideoPlayerUrl
    {
        internal VideoPlayerUrl() { }

        /// <summary>
        /// Creates an instance of VideoPlayerUrl
        /// </summary>
        /// <param name="url">A URL pointing to a player for a specific video.</param>
        public VideoPlayerUrl(string url)
        {
            Url = url;
        }

        /// <summary>
        /// The optional attribute allow_embed specifies whether Google can embed the video in search results. Allowed values are Yes or No.
        /// </summary>
        [XmlAttribute("allow_embed")]
        public YesNo AllowEmbed { get; set; }


        /// <summary>
        /// The optional attribute autoplay has a user-defined string (in the example above, ap=1) that Google may append (if appropriate) to the flashvars parameter
        ///  to enable autoplay of the video. For example: &lt;embed src="http://www.example.com/videoplayer.swf?video=123" autoplay="ap=1"/&gt;.
        /// </summary>
        [XmlAttribute("autoplay")]
        public string Autoplay { get; set; }


        /// <summary>
        /// A URL pointing to a player for a specific video. 
        /// </summary>
        [XmlText, Url]
        public string Url { get; set; }


        public bool ShouldSerializeAllowEmbed()
        {
            return AllowEmbed != YesNo.None;
        }
    }
}