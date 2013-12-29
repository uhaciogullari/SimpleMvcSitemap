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
        private readonly IXmlNamespaceResolver _namespaceResolver;

        internal SitemapProvider(IActionResultFactory actionResultFactory, IBaseUrlProvider baseUrlProvider,IXmlNamespaceResolver namespaceResolver)
        {
            _actionResultFactory = actionResultFactory;
            _baseUrlProvider = baseUrlProvider;
            _namespaceResolver = namespaceResolver;
        }

        public SitemapProvider() : this(new ActionResultFactory(), new BaseUrlProvider(),new XmlNamespaceResolver()) { }

        public ActionResult CreateSitemap(HttpContextBase httpContext, IEnumerable<SitemapNode> nodes)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            string baseUrl = _baseUrlProvider.GetBaseUrl(httpContext);
            List<SitemapNode> nodeList = nodes != null ? nodes.ToList() : new List<SitemapNode>();
            IEnumerable<XmlSerializerNamespace> namespaces = _namespaceResolver.GetNamespaces(nodeList);
            return CreateSitemapInternal(baseUrl, nodeList, namespaces);
        }

        public ActionResult CreateSitemap(HttpContextBase httpContext, IEnumerable<SitemapNode> nodes,
                                          ISitemapConfiguration configuration)
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
            List<SitemapNode> nodeList = nodes != null ? nodes.ToList() : new List<SitemapNode>();
            IEnumerable<XmlSerializerNamespace> namespaces = _namespaceResolver.GetNamespaces(nodeList);

            if (configuration.Size >= nodeList.Count)
            {
                return CreateSitemapInternal(baseUrl, nodeList, namespaces);
            }
            if (configuration.CurrentPage.HasValue && configuration.CurrentPage.Value > 0)
            {
                int skipCount = (configuration.CurrentPage.Value - 1) * configuration.Size;
                List<SitemapNode> pageNodes = nodeList.Skip(skipCount).Take(configuration.Size).ToList();
                return CreateSitemapInternal(baseUrl, pageNodes, namespaces);
            }

            int pageCount = (int)Math.Ceiling((double)nodeList.Count / configuration.Size);
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

            var sitemap = new SitemapIndexModel(nodeList);
            return _actionResultFactory.CreateXmlResult(sitemap);
        }

        private ActionResult CreateSitemapInternal(string baseUrl, List<SitemapNode> nodes, IEnumerable<XmlSerializerNamespace> namespaces = null)
        {
            nodes.ForEach(node => ValidateUrl(baseUrl, node));

            SitemapModel sitemap = new SitemapModel(nodes);

            return _actionResultFactory.CreateXmlResult(sitemap, namespaces);
        }

        private IEnumerable<SitemapIndexNode> CreateIndexNode(ISitemapConfiguration configuration,
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