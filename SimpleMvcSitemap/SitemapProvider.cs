using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleMvcSitemap
{
    public class SitemapProvider : ISitemapProvider
    {
        private readonly ISitemapActionResultFactory _sitemapActionResultFactory;

        internal SitemapProvider(ISitemapActionResultFactory sitemapActionResultFactory)
        {
            _sitemapActionResultFactory = sitemapActionResultFactory;
        }

        public SitemapProvider() : this(new SitemapActionResultFactory(new UrlValidator(new ReflectionHelper(), new BaseUrlProvider()))) { }

        public ActionResult CreateSitemap(HttpContextBase httpContext, IEnumerable<SitemapNode> nodes)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            List<SitemapNode> nodeList = nodes != null ? nodes.ToList() : new List<SitemapNode>();
            return CreateSitemapInternal(httpContext, nodeList);
        }

        public ActionResult CreateSitemap<T>(HttpContextBase httpContext, IQueryable<T> nodes, ISitemapConfiguration<T> configuration)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            if (configuration == null)
            {
                throw new ArgumentNullException("configuration");
            }

            int nodeCount = nodes.Count();

            if (configuration.Size >= nodeCount)
            {
                return CreateSitemapInternal(httpContext, nodes.ToList().Select(configuration.CreateNode).ToList());
            }

            if (configuration.CurrentPage.HasValue && configuration.CurrentPage.Value > 0)
            {
                int skipCount = (configuration.CurrentPage.Value - 1) * configuration.Size;
                List<SitemapNode> pageNodes = nodes.Skip(skipCount).Take(configuration.Size).ToList().Select(configuration.CreateNode).ToList();
                return CreateSitemapInternal(httpContext, pageNodes);
            }

            int pageCount = (int)Math.Ceiling((double)nodeCount / configuration.Size);
            var indexNodes = CreateIndexNode(configuration, pageCount);
            return _sitemapActionResultFactory.CreateSitemapResult(httpContext, new SitemapIndexModel(indexNodes));
        }

        public ActionResult CreateSitemap(HttpContextBase httpContext, IEnumerable<SitemapIndexNode> nodes)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            List<SitemapIndexNode> nodeList = nodes != null ? nodes.ToList() : new List<SitemapIndexNode>();

            SitemapIndexModel sitemap = new SitemapIndexModel(nodeList);
            return _sitemapActionResultFactory.CreateSitemapResult(httpContext, sitemap);
        }

        private ActionResult CreateSitemapInternal(HttpContextBase httpContext, List<SitemapNode> nodes)
        {
            SitemapModel sitemap = new SitemapModel(nodes);

            return _sitemapActionResultFactory.CreateSitemapResult(httpContext, sitemap);
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