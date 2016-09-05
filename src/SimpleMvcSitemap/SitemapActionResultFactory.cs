using Microsoft.AspNetCore.Mvc;

namespace SimpleMvcSitemap
{
    class SitemapActionResultFactory : ISitemapActionResultFactory
    {
        private readonly IUrlValidator _urlValidator;

        public SitemapActionResultFactory(IUrlValidator urlValidator)
        {
            _urlValidator = urlValidator;
        }

        public ActionResult CreateSitemapResult<T>(ActionContext actionContext, T data)
        {
            _urlValidator.ValidateUrls(actionContext, data);
            return new XmlResult<T>(data);
        }
    }
}