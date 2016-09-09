using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using SimpleMvcSitemap.Serialization;

namespace SimpleMvcSitemap
{
    /// <summary>
    /// Encapsulates the sitemap file and references the current protocol standard.
    /// </summary>
    [XmlRoot("urlset", Namespace = XmlNamespaces.Sitemap)]
    public class SitemapModel : IXmlNamespaceProvider
    {
        private readonly IEnumerable<SitemapNode> _nodeList;

        internal SitemapModel() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SitemapModel"/> class.
        /// </summary>
        /// <param name="sitemapNodes">Sitemap nodes.</param>
        public SitemapModel(IEnumerable<SitemapNode> sitemapNodes)
        {
            _nodeList = sitemapNodes ?? Enumerable.Empty<SitemapNode>();
        }

        /// <summary>
        /// Sitemap nodes linking to documents
        /// </summary>
        [XmlElement("url")]
        public List<SitemapNode> Nodes => _nodeList.ToList();

        /// <summary>
        /// Gets the XML namespaces.
        /// Exposed for XML serializer.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetNamespaces()
        {
            List<string> namespaces = new List<string>();

            if (Nodes.Any(node => node.Images != null && node.Images.Any()))
            {
                namespaces.Add(XmlNamespaces.Image);
            }

            if (Nodes.Any(node => node.News != null))
            {
                namespaces.Add(XmlNamespaces.News);
            }

            if (Nodes.Any(node => node.Video != null))
            {
                namespaces.Add(XmlNamespaces.Video);
            }

            if (Nodes.Any(node => node.Mobile != null))
            {
                namespaces.Add(XmlNamespaces.Mobile);
            }

            if (Nodes.Any(node => node.Translations != null && node.Translations.Any()))
            {
                namespaces.Add(XmlNamespaces.Xhtml);
            }

            return namespaces;
        }
    }
}