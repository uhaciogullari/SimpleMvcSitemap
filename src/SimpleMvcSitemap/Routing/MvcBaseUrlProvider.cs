using System;
using System.Web;
using System.Web.Mvc;

namespace SimpleMvcSitemap.Routing
{
    class MvcBaseUrlProvider : IBaseUrlProvider
    {
        private readonly HttpContextBase httpContext;

        public MvcBaseUrlProvider(HttpContextBase httpContext)
        {
            this.httpContext = httpContext;

        }

        public Uri BaseUrl => new Uri($"{httpContext.Request.Url.Scheme}://{httpContext.Request.Url.Authority}{httpContext.Request.ApplicationPath}");
    }
}