using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    /// <summary>
    /// Encloses all information about a specific URL.
    /// </summary>
    [XmlRoot("url", Namespace = Namespaces.Sitemap)]
    public class SitemapNode : IHasUrl
    {
        internal SitemapNode() { }

        /// <summary>
        /// Creates a sitemap node
        /// </summary>
        /// <param name="url">Specifies the URL. For images and video, specifies the landing page (aka play page).</param>
        public SitemapNode(string url)
        {
            Url = url;
        }


        /// <summary>
        /// Specifies the URL. For images and video, specifies the landing page (aka play page).
        /// </summary>
        [XmlElement("loc", Order = 1)]
        public string Url { get; set; }

        
        /// <summary>
        /// Shows the date the URL was last modified, value is optional.
        /// </summary>
        [XmlElement("lastmod", Order = 2)]
        public DateTime? LastModificationDate { get; set; }

        
        /// <summary>
        /// Provides a hint about how frequently the page is likely to change.
        /// </summary>
        [XmlElement("changefreq", Order = 3)]
        public ChangeFrequency? ChangeFrequency { get; set; }


        /// <summary>
        /// Describes the priority of a URL relative to all the other URLs on the site.
        /// This priority can range from 1.0 (extremely important) to 0.1 (not important at all).
        /// Note that the priority tag does not affect your site ranking in Google search results.
        /// Priority values are only considered relative to other pages on your site so, 
        /// assigning a high priority (or specifying the same priority for all URLs) will not boost your entire site search ranking.
        /// </summary>
        [XmlElement("priority", Order = 4)]
        public decimal? Priority { get; set; }

        
        /// <summary>
        /// Additional information about important images on the page.
        /// </summary>
        [XmlElement("image", Order = 5, Namespace = Namespaces.Image)]
        public List<SitemapImage> Images { get; set; }

        /// <summary>
        /// Additional information about news article on the page.
        /// </summary>
        [XmlElement("news", Order = 6, Namespace = Namespaces.News)]
        public SitemapNews News { get; set; }

        
        /// <summary>
        /// Additional information about video content on the page.
        /// </summary>
        [XmlElement("video", Order = 7, Namespace = Namespaces.Video)]
        public SitemapVideo Video { get; set; }


        public bool ShouldSerializeLastModificationDate()
        {
            return LastModificationDate.HasValue;
        }

        public bool ShouldSerializeChangeFrequency()
        {
            return ChangeFrequency.HasValue;
        }

        public bool ShouldSerializePriority()
        {
            return Priority.HasValue;
        }

    }
}