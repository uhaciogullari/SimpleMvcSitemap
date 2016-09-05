using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SimpleMvcSitemap
{
    /// <summary>
    /// Provides sitemap files that can be returned from ASP.NET MVC controllers
    /// </summary>
    public interface ISitemapProvider
    {
        /// <summary>
        /// Creates a sitemap.
        /// </summary>
        /// <param name="httpContext">ASP.NET HTTP context.</param>
        /// <param name="nodes">Nodes for linking documents.
        ///     Make sure the count does not exceed the limits(50000 for now).
        /// </param>
        ActionResult CreateSitemap(HttpContext httpContext, IEnumerable<SitemapNode> nodes);

        /// <summary>
        /// Creates a sitemap from a LINQ data source and handles the paging.
        /// </summary>
        /// <typeparam name="T">Source item type</typeparam>
        /// <param name="httpContext">ASP.NET HTTP context.</param>
        /// <param name="nodes">Data source for creating nodes.</param>
        /// <param name="configuration">Sitemap configuration for index files.</param>
        ActionResult CreateSitemap<T>(HttpContext httpContext, IQueryable<T> nodes, ISitemapConfiguration<T> configuration);

        /// <summary>
        /// Creates a sitemap.
        /// </summary>
        /// <param name="httpContext">ASP.NET HTTP context.</param>
        /// <param name="nodes">Nodes for linking sitemap files</param>
        ActionResult CreateSitemap(HttpContext httpContext, IEnumerable<SitemapIndexNode> nodes);
    }
}