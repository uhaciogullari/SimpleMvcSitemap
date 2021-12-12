using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMvcSitemap.Middleware
{
    /// <summary>
    /// Sitemap Automatic Generator Options
    /// </summary>
    public class SitemapGeneratorOptions
    {
        /// <summary>
        /// Default Sitemap Type (Defaults to SitemapIndex)
        /// </summary>
        public SiteMapType DefaultSiteMapType { get; set; } = SiteMapType.Sitemap;
        /// <summary>
        /// Base Url, if null defaults to the requests Url.
        /// </summary>
        public string BaseUrl { get; set; }
        /// <summary>
        /// Enable automatic robots.txt generation (Defaults to true)
        /// </summary>
        public bool EnableRobotsTxtGeneration { get; set; } = true;
        /// <summary>
        /// Default change frequency
        /// </summary>
        public ChangeFrequency? DefaultChangeFrequency { get; set; }
        /// <summary>
        /// Default priority
        /// </summary>
        public decimal? DefaultPriority { get; set; }
        /// <summary>
        /// Sets the last modification date on pages. Typically this would be the applications compile date as the views don't change once
        /// they are compiled into ASP .NET Core web app.
        /// </summary>
        public DateTime? LastModifiedDate { get; set; }
    }

    /// <summary>
    /// Sitemap Type
    /// </summary>
    public enum SiteMapType
    {
        /// <summary>
        /// Creates Sitemap files
        /// http://www.sitemaps.org/protocol.html
        /// </summary>
        Sitemap,
        /// <summary>
        /// Creates sitemap index files
        /// http://www.sitemaps.org/protocol.html#index
        /// </summary>
        SitemapIndex
    }
}
