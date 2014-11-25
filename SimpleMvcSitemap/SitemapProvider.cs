using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleMvcSitemap
{
    public class SitemapProvider : ISitemapProvider
    {
        private readonly IActionResultFactory _actionResultFactory;
        private readonly IBaseUrlProvider _baseUrlProvider;

        internal SitemapProvider(IActionResultFactory actionResultFactory, IBaseUrlProvider baseUrlProvider)
        {
            _actionResultFactory = actionResultFactory;
            _baseUrlProvider = baseUrlProvider;
        }

        public SitemapProvider() : this(new ActionResultFactory(), new BaseUrlProvider()) { }

        public ActionResult CreateSitemap(HttpContextBase httpContext, IEnumerable<SitemapNode> nodes)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            string baseUrl = _baseUrlProvider.GetBaseUrl(httpContext);
            List<SitemapNode> nodeList = nodes != null ? nodes.ToList() : new List<SitemapNode>();
            return CreateSitemapInternal(baseUrl, nodeList);
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

            string baseUrl = _baseUrlProvider.GetBaseUrl(httpContext);
            int nodeCount = nodes.Count();

            if (configuration.Size >= nodeCount)
            {
                return CreateSitemapInternal(baseUrl, nodes.ToList().Select(configuration.CreateNode).ToList());
            }

            if (configuration.CurrentPage.HasValue && configuration.CurrentPage.Value > 0)
            {
                int skipCount = (configuration.CurrentPage.Value - 1) * configuration.Size;
                List<SitemapNode> pageNodes = nodes.Skip(skipCount).Take(configuration.Size).ToList().Select(configuration.CreateNode).ToList();
                return CreateSitemapInternal(baseUrl, pageNodes);
            }

            int pageCount = (int)Math.Ceiling((double)nodeCount / configuration.Size);
            var indexNodes = CreateIndexNode(configuration, baseUrl, pageCount);
            return _actionResultFactory.CreateXmlResult(new SitemapIndexModel(indexNodes));
        }

        public ActionResult CreateSitemap(HttpContextBase httpContext, IEnumerable<SitemapIndexNode> nodes)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            string baseUrl = _baseUrlProvider.GetBaseUrl(httpContext);

            List<SitemapIndexNode> nodeList = nodes != null ? nodes.ToList() : new List<SitemapIndexNode>();
            nodeList.ForEach(node => ValidateUrl(baseUrl, node));

            SitemapIndexModel sitemap = new SitemapIndexModel(nodeList);
            return _actionResultFactory.CreateXmlResult(sitemap);
        }

        private ActionResult CreateSitemapInternal(string baseUrl, List<SitemapNode> nodes)
        {
            foreach (SitemapNode sitemapNode in nodes)
            {
                ValidateUrl(baseUrl, sitemapNode);
                if (sitemapNode.Images != null)
                {
                    sitemapNode.Images.ForEach(image => ValidateUrl(baseUrl, image));
                }
            }

            SitemapModel sitemap = new SitemapModel(nodes);

            return _actionResultFactory.CreateXmlResult(sitemap);
        }

        private IEnumerable<SitemapIndexNode> CreateIndexNode<T>(ISitemapConfiguration<T> configuration,
                                                              string baseUrl, int pageCount)
        {
            for (int page = 1; page <= pageCount; page++)
            {
                string url = configuration.CreateSitemapUrl(page);
                SitemapIndexNode indexNode = new SitemapIndexNode { Url = url };
                ValidateUrl(baseUrl, indexNode);
                yield return indexNode;
            }
        }

        private void ValidateUrl(string baseUrl, IHasUrl node)
        {
            if (!Uri.IsWellFormedUriString(node.Url, UriKind.Absolute))
            {
                node.Url = string.Concat(baseUrl, node.Url);
            }
        }
    }
}