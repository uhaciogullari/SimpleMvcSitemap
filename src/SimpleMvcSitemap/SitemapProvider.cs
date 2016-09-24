#if Mvc
using System.Web.Mvc;
#endif

#if CoreMvc
using Microsoft.AspNetCore.Mvc;
#endif

using System;
using System.Collections.Generic;
using System.Linq;
using SimpleMvcSitemap.Routing;


namespace SimpleMvcSitemap
{
    /// <summary>
    /// Provides sitemap files that can be returned from ASP.NET MVC controllers
    /// </summary>
    public class SitemapProvider : ISitemapProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SitemapProvider"/> class.
        /// </summary>
        public SitemapProvider()
        {
        }

        /// <summary>
        /// Creates a sitemap.
        /// </summary>
        /// <param name="sitemapModel"></param>
        public ActionResult CreateSitemap(SitemapModel sitemapModel)
        {
            if (sitemapModel == null)
            {
                throw new ArgumentNullException(nameof(sitemapModel));
            }

            return new XmlResult<SitemapModel>(sitemapModel);
        }

        /// <summary>
        /// Creates a sitemap from a LINQ data source and handles the paging.
        /// </summary>
        /// <typeparam name="T">Source item type</typeparam>
        /// <param name="nodes">Data source for creating nodes.</param>
        /// <param name="configuration">Sitemap configuration for index files.</param>
        public ActionResult CreateSitemap<T>(IQueryable<T> nodes, ISitemapConfiguration<T> configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }


            int nodeCount = nodes.Count();

            if (configuration.Size >= nodeCount)
            {
                return CreateActionResult(nodes.ToList().Select(configuration.CreateNode).ToList());
            }

            if (configuration.CurrentPage.HasValue && configuration.CurrentPage.Value > 0)
            {
                int skipCount = (configuration.CurrentPage.Value - 1) * configuration.Size;
                List<SitemapNode> pageNodes = nodes.Skip(skipCount).Take(configuration.Size).ToList().Select(configuration.CreateNode).ToList();
                return CreateActionResult(pageNodes);
            }

            int pageCount = (int)Math.Ceiling((double)nodeCount / configuration.Size);
            var indexNodes = CreateIndexNode(configuration, pageCount);
            throw new NotImplementedException();
        }


        /// <summary>
        /// Creates a sitemap.
        /// </summary>
        /// <param name="nodes">Nodes for linking sitemap files</param>
        public ActionResult CreateSitemap(IEnumerable<SitemapIndexNode> nodes)
        {
            List<SitemapIndexNode> nodeList = nodes?.ToList() ?? new List<SitemapIndexNode>();

            SitemapIndexModel sitemap = new SitemapIndexModel(nodeList);
            throw new NotImplementedException();
        }

        private ActionResult CreateActionResult<T>(T model)
        {
            return new XmlResult<T>(model);
        }

        private IEnumerable<SitemapIndexNode> CreateIndexNode<T>(ISitemapConfiguration<T> configuration, int pageCount)
        {
            for (int page = 1; page <= pageCount; page++)
            {
                string url = configuration.CreateSitemapUrl(page);
                SitemapIndexNode indexNode = new SitemapIndexNode { Url = url };
                yield return indexNode;
            }
        }

    }
}