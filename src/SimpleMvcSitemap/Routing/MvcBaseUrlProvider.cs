using System.Web;
using System.Web.Mvc;

namespace SimpleMvcSitemap.Routing
{
    class MvcBaseUrlProvider : IBaseUrlProvider
    {
        private HttpContextBase httpContext;

        public MvcBaseUrlProvider(HttpContextBase httpContext)
        {
            this.httpContext = httpContext;

        }

        public string BaseUrl => $"{httpContext.Request.Url.Scheme}://{httpContext.Request.Url.Authority}{httpContext.Request.ApplicationPath}";
    }
}