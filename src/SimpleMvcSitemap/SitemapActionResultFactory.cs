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

        public ActionResult CreateSitemapResult<T>(T data)
        {
            _urlValidator.ValidateUrls(data);
            return new XmlResult<T>(data);
        }
    }
}