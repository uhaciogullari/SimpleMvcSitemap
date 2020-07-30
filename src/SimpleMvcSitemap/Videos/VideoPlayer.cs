using System.Xml.Serialization;
using SimpleMvcSitemap.Routing;

namespace SimpleMvcSitemap.Videos
{
    /// <summary>
    /// Encapsulates the information about player URL
    /// </summary>
    public class VideoPlayer
    {
        internal VideoPlayer() { }

        /// <summary>
        /// Creates an instance of VideoPlayer
        /// </summary>
        /// <param name="url">A URL pointing to a player for a specific video.</param>
        public VideoPlayer(string url)
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


        /// <summary>
        /// Used for not serializing null values.
        /// </summary>
        public bool ShouldSerializeAllowEmbed()
        {
            return AllowEmbed != YesNo.None;
        }
    }
}