using System.Web;
using System.Web.Mvc;

namespace SimpleMvcSitemap
{
    class SitemapActionResultFactory : ISitemapActionResultFactory
    {
        private readonly IUrlValidator _urlValidator;

        public SitemapActionResultFactory(IUrlValidator urlValidator)
        {
            _urlValidator = urlValidator;
        }

        public ActionResult CreateSitemapResult<T>(HttpContextBase httpContext, T data)
        {
            _urlValidator.ValidateUrls(httpContext, data);
            return new XmlResult<T>(data);
        }
    }
}