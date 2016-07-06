using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    /// <summary>
    /// Encloses alternative links to a url for another language or locale
    /// </summary>
    public class SitemapUrlLink
    {
        internal SitemapUrlLink() { }

        /// <summary>
        /// Set an alternative link for a URL
        /// </summary>
        /// <param name="href">The URL to the other resource (should be absolute)</param>
        /// <param name="hreflang">The locale for the other resource, e.g. 'de-DE'</param>
        /// <param name="rel">Defaults to 'alternate'</param>
        public SitemapUrlLink(string href, string hreflang, string rel = "alternate")
        {
            Href = href;
            Hreflang = hreflang;
            Rel = rel;
        }


        /// <summary>
        /// The URL of the alternative language version of the URL
        /// </summary>
        [XmlAttribute("href"), Url]
        public string Href { get; set; }


        /// <summary>
        /// Defaults to alternate
        /// </summary>
        [XmlAttribute("rel")]
        public string Rel { get; set; }


        /// <summary>
        /// The locale of the alternative version, e.g. de-DE
        /// </summary>
        [XmlAttribute("hreflang")]
        public string Hreflang { get; set; }

    }
}
