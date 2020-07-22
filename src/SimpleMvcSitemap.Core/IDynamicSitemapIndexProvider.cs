using Microsoft.AspNetCore.Mvc;

namespace SimpleMvcSitemap
{
    /// <summary>
    /// Creates sitemap index or sitemap files using dynamic data
    /// </summary>
    public interface IDynamicSitemapIndexProvider
    {
        /// <summary>
        /// Creates sitemap index or sitemap files using dynamic data
        /// </summary>
        /// <typeparam name="T">Source item type</typeparam>
        /// <param name="sitemapProvider">Sitemap provider</param>
        /// <param name="sitemapIndexConfiguration">Sitemap index configuration</param>
        ActionResult CreateSitemapIndex<T>(ISitemapProvider sitemapProvider, ISitemapIndexConfiguration<T> sitemapIndexConfiguration);
    }
}
