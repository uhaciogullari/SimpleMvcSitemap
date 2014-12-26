using System;
using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    /// <summary>
    /// Encapsulates information about an individual Sitemap.
    /// </summary>
    [XmlRoot("sitemap", Namespace = Namespaces.Sitemap)]
    public class SitemapIndexNode : IHasUrl
    {
        internal SitemapIndexNode() { }

        public SitemapIndexNode(string url)
        {
            Url = url;
        }

        
        /// <summary>
        /// Identifies the location of the Sitemap.
        /// This location can be a Sitemap, an Atom file, RSS file or a simple text file.
        /// </summary>
        [XmlElement("loc", Order = 1), Url]
        public string Url { get; set; }

        
        /// <summary>
        /// Identifies the time that the corresponding Sitemap file was modified.
        /// It does not correspond to the time that any of the pages listed in that Sitemap were changed.
        /// By providing the last modification timestamp, you enable search engine crawlers to retrieve only a subset of the Sitemaps in the index
        ///  i.e. a crawler may only retrieve Sitemaps that were modified since a certain date.
        /// This incremental Sitemap fetching mechanism allows for the rapid discovery of new URLs on very large sites.
        /// </summary>
        [XmlElement("lastmod", Order = 2)]
        public DateTime? LastModificationDate { get; set; }

        
        public bool ShouldSerializeLastModificationDate()
        {
            return LastModificationDate != null;
        }
    }
}