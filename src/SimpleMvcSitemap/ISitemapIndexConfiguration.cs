using System.Collections.Generic;
using System.Linq;
using SimpleMvcSitemap.StyleSheets;

namespace SimpleMvcSitemap
{
    /// <summary>
    /// Configuration for required for creating sitemap index files
    /// </summary>
    /// <typeparam name="T">Source item type</typeparam>
    public interface ISitemapIndexConfiguration<T>
    {
        /// <summary>
        /// Data source that will be queried for paging.
        /// </summary>
        IQueryable<T> DataSource { get; }

        /// <summary>
        /// Number of items in each sitemap file.
        /// </summary>
        int Size { get; }

        /// <summary>
        /// Current page for paged sitemap items.
        /// </summary>
        int? CurrentPage { get; }

        /// <summary>
        /// Creates a node in the the sitemap index file that will refer to a specific sitemap.
        /// </summary>
        /// <param name="currentPage">The specified page URL.</param>
        /// <returns></returns>
        SitemapIndexNode CreateSitemapIndexNode(int currentPage);

        /// <summary>
        /// Creates the sitemap node.
        /// </summary>
        /// <param name="source">The source item.</param>
        SitemapNode CreateNode(T source);

        /// <summary>
        /// XML stylesheets that will be attached to the sitemap files.
        /// </summary>
        List<XmlStyleSheet> SitemapStyleSheets { get; }

        /// <summary>
        /// XML stylesheets that will be attached to the sitemap index files.
        /// </summary>
        List<XmlStyleSheet> SitemapIndexStyleSheets { get; }
    }
}