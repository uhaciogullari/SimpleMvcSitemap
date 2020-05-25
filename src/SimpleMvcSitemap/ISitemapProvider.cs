using Microsoft.AspNetCore.Mvc;


namespace SimpleMvcSitemap
{
    /// <summary>
    /// Provides sitemap files that can be returned from the controllers
    /// </summary>
    public interface ISitemapProvider
    {
        /// <summary>
        /// Creates a sitemap file.
        /// </summary>
        /// <param name="sitemapModel">The sitemap model</param>
        ActionResult CreateSitemap(SitemapModel sitemapModel);

        /// <summary>
        /// Creates a sitemap index file.
        /// </summary>
        /// <param name="sitemapIndexModel">The sitemap index model</param>
        ActionResult CreateSitemapIndex(SitemapIndexModel sitemapIndexModel);
    }
}