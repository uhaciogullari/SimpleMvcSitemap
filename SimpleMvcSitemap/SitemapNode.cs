using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    /// <summary>
    /// Encloses all information about a specific URL.
    /// </summary>
    [XmlRoot("url", Namespace = Namespaces.Sitemap)]
    public class SitemapNode
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
        /// URL of the page.
        /// This URL must begin with the protocol (such as http) and end with a trailing slash, if your web server requires it.
        /// This value must be less than 2,048 characters.
        /// </summary>
        [XmlElement("loc", Order = 1), Url]
        public string Url { get; set; }


        /// <summary>
        /// Shows the date the URL was last modified, value is optional.
        /// </summary>
        [XmlElement("lastmod", Order = 2)]
        public DateTime? LastModificationDate { get; set; }


        /// <summary>
        /// How frequently the page is likely to change. 
        /// This value provides general information to search engines and may not correlate exactly to how often they crawl the page.
        /// </summary>
        [XmlElement("changefreq", Order = 3)]
        public ChangeFrequency? ChangeFrequency { get; set; }


        /// <summary>
        /// The priority of this URL relative to other URLs on your site. Valid values range from 0.0 to 1.0. This value does not affect how your pages are compared to pages on other sites—it only lets the search engines know which pages you deem most important for the crawlers.
        /// The default priority of a page is 0.5.
        /// Please note that the priority you assign to a page is not likely to influence the position of your URLs in a search engine's result pages.
        /// Search engines may use this information when selecting between URLs on the same site, 
        /// so you can use this tag to increase the likelihood that your most important pages are present in a search index.
        /// Also, please note that assigning a high priority to all of the URLs on your site is not likely to help you.
        /// Since the priority is relative, it is only used to select between URLs on your site.
        /// </summary>
        [XmlElement("priority", Order = 4)]
        public decimal? Priority { get; set; }


        /// <summary>
        /// Additional information about important images on the page.
        /// It can include up to 1000 images.
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


        [XmlElement("mobile", Order = 8, Namespace = Namespaces.Mobile)]
        public SitemapMobile Mobile { get; set; }


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