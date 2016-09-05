using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace SimpleMvcSitemap
{
    /// <summary>
    /// Provides sitemap files that can be returned from ASP.NET MVC controllers
    /// </summary>
    public class SitemapProvider : ISitemapProvider
    {
        private readonly ISitemapActionResultFactory _sitemapActionResultFactory;

        internal SitemapProvider(ISitemapActionResultFactory sitemapActionResultFactory)
        {
            _sitemapActionResultFactory = sitemapActionResultFactory;
        }

        /// <summary>
        /// Creates a sitemap.
        /// </summary>
        /// <param name="actionContext"></param>
        /// <param name="nodes">Nodes for linking documents.
        ///     Make sure the count does not exceed the limits(50000 for now).
        /// </param>
        public ActionResult CreateSitemap(ActionContext actionContext, IEnumerable<SitemapNode> nodes)
        {
            if (actionContext == null)
            {
                throw new ArgumentNullException(nameof(actionContext));
            }

            List<SitemapNode> nodeList = nodes?.ToList() ?? new List<SitemapNode>();
            return CreateSitemapInternal(actionContext, nodeList);
        }

        /// <summary>
        /// Creates a sitemap from a LINQ data source and handles the paging.
        /// </summary>
        /// <typeparam name="T">Source item type</typeparam>
        /// <param name="actionContext"></param>
        /// <param name="nodes">Data source for creating nodes.</param>
        /// <param name="configuration">Sitemap configuration for index files.</param>
        public ActionResult CreateSitemap<T>(ActionContext actionContext, IQueryable<T> nodes, ISitemapConfiguration<T> configuration)
        {
            if (actionContext == null)
            {
                throw new ArgumentNullException(nameof(actionContext));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }


            int nodeCount = nodes.Count();

            if (configuration.Size >= nodeCount)
            {
                return CreateSitemapInternal(actionContext, nodes.ToList().Select(configuration.CreateNode).ToList());
            }

            if (configuration.CurrentPage.HasValue && configuration.CurrentPage.Value > 0)
            {
                int skipCount = (configuration.CurrentPage.Value - 1) * configuration.Size;
                List<SitemapNode> pageNodes = nodes.Skip(skipCount).Take(configuration.Size).ToList().Select(configuration.CreateNode).ToList();
                return CreateSitemapInternal(actionContext, pageNodes);
            }

            int pageCount = (int)Math.Ceiling((double)nodeCount / configuration.Size);
            var indexNodes = CreateIndexNode(configuration, pageCount);
            return _sitemapActionResultFactory.CreateSitemapResult(actionContext, new SitemapIndexModel(indexNodes));
        }


        /// <summary>
        /// Creates a sitemap.
        /// </summary>
        /// <param name="actionContext"></param>
        /// <param name="nodes">Nodes for linking sitemap files</param>
        public ActionResult CreateSitemap(ActionContext actionContext, IEnumerable<SitemapIndexNode> nodes)
        {
            if (actionContext == null)
            {
                throw new ArgumentNullException(nameof(actionContext));
            }

            List<SitemapIndexNode> nodeList = nodes?.ToList() ?? new List<SitemapIndexNode>();

            SitemapIndexModel sitemap = new SitemapIndexModel(nodeList);
            return _sitemapActionResultFactory.CreateSitemapResult(actionContext, sitemap);
        }

        private ActionResult CreateSitemapInternal(ActionContext actionContext, List<SitemapNode> nodes)
        {
            SitemapModel sitemap = new SitemapModel(nodes);

            return _sitemapActionResultFactory.CreateSitemapResult(actionContext, sitemap);
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