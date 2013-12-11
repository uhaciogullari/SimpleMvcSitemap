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
            return CreateSitemapInternal(baseUrl, nodes);
        }

        private ActionResult CreateSitemapInternal(string baseUrl, IEnumerable<SitemapNode> nodes)
        {
            List<SitemapNode> nodeList = nodes != null ? nodes.ToList() : new List<SitemapNode>();
            nodeList.ForEach(node => ValidateUrl(baseUrl, node));

            SitemapModel sitemap = new SitemapModel(nodeList);
            return _actionResultFactory.CreateXmlResult(sitemap);
        }

        public ActionResult CreateSitemap(HttpContextBase httpContext, IEnumerable<SitemapNode> nodes, ISitemapConfiguration configuration)
        {
            throw new NotImplementedException();
        }

        private void ValidateUrl(string baseUrl, SitemapNode node)
        {
            if (!Uri.IsWellFormedUriString(node.Url, UriKind.Absolute))
            {
                node.Url = string.Concat(baseUrl, node.Url);
            }
        }
    }
}