using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using SimpleMvcSitemap.Serialization;
using SimpleMvcSitemap.StyleSheets;

namespace SimpleMvcSitemap
{
    /// <summary>
    /// Encapsulates the sitemap file and references the current protocol standard.
    /// </summary>
    [XmlRoot("urlset", Namespace = XmlNamespaces.Sitemap)]
    public class SitemapModel : IXmlNamespaceProvider, IHasStyleSheets
    {
        internal SitemapModel() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SitemapModel"/> class.
        /// </summary>
        /// <param name="nodes">Sitemap nodes.</param>
        public SitemapModel(List<SitemapNode> nodes)
        {
            Nodes = nodes;
        }

        /// <summary>
        /// Sitemap nodes linking to documents
        /// </summary>
        [XmlElement("url")]
        public List<SitemapNode> Nodes { get; }

        /// <inheritDoc/>
        public IEnumerable<string> GetNamespaces()
        {
            if (Nodes == null)
            {
                yield break;
            }

            if (Nodes.Any(node => node.Images != null && node.Images.Any()))
            {
                yield return XmlNamespaces.Image;
            }

            if (Nodes.Any(node => node.News != null))
            {
                yield return XmlNamespaces.News;
            }

            if (Nodes.Any(node => node.Videos != null && node.Videos.Any()))
            {
                yield return XmlNamespaces.Video;
            }

            if (Nodes.Any(node => node.Translations != null && node.Translations.Any()))
            {
                yield return XmlNamespaces.Xhtml;
            }
        }


        /// <inheritDoc/>
        [XmlIgnore]
        public List<XmlStyleSheet> StyleSheets { get; set; }
    }
}