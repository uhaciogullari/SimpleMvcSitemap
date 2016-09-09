#if Mvc
using System.Web.Mvc;
#endif

#if CoreMvc
using Microsoft.AspNetCore.Mvc;
#endif

using SimpleMvcSitemap.Routing;


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
            return new XmlResult<T>(data, _urlValidator);
        }
    }
}