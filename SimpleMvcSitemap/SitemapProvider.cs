using System;
using System.Collections.Generic;
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

        public ActionResult CreateSiteMap(HttpContextBase httpContext, IList<SitemapNode> nodeList)
        {
            string baseUrl = _baseUrlProvider.GetBaseUrl(httpContext);

            foreach (SitemapNode node in nodeList)
            {
                ValidateUrl(baseUrl, node);
            }

            SitemapModel sitemap = new SitemapModel(nodeList);
            return _actionResultFactory.CreateXmlResult(sitemap);
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