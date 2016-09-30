using System.Collections.Generic;
using System.Xml.Serialization;
using SimpleMvcSitemap.Serialization;
using SimpleMvcSitemap.StyleSheets;

namespace SimpleMvcSitemap
{
    /// <summary>
    /// Encapsulates information about all of the Sitemaps in the file.
    /// </summary>
    [XmlRoot("sitemapindex", Namespace = XmlNamespaces.Sitemap)]
    public class SitemapIndexModel : IHasStyleSheets
    {
        internal SitemapIndexModel() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SitemapIndexModel"/> class.
        /// </summary>
        /// <param name="nodes">The sitemap index nodes.</param>
        public SitemapIndexModel(List<SitemapIndexNode> nodes)
        {
            Nodes = nodes;
        }

        /// <summary>
        /// Index nodes linking to sitemap files.
        /// </summary>
        [XmlElement("sitemap")]
        public List<SitemapIndexNode> Nodes { get; }

        /// <inheritDoc/>
        [XmlIgnore]
        public List<XmlStyleSheet> StyleSheets { get; set; }
    }
}