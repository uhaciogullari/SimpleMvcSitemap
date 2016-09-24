﻿#if Mvc
using System.Web.Mvc;
# endif

#if CoreMvc
using Microsoft.AspNetCore.Mvc;
#endif

using System.Collections.Generic;
using System.Linq;


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
        /// <param name="sitemapModel"></param>
        ActionResult CreateSitemap(SitemapModel sitemapModel);

        /// <summary>
        /// Creates a sitemap from a LINQ data source and handles the paging.
        /// </summary>
        /// <typeparam name="T">Source item type</typeparam>
        /// <param name="nodes">Data source for creating nodes.</param>
        /// <param name="configuration">Sitemap configuration for index files.</param>
        ActionResult CreateSitemap<T>(IQueryable<T> nodes, ISitemapConfiguration<T> configuration);

        /// <summary>
        /// Creates a sitemap.
        /// </summary>
        /// <param name="nodes">Nodes for linking sitemap files</param>
        ActionResult CreateSitemap(IEnumerable<SitemapIndexNode> nodes);
    }
}