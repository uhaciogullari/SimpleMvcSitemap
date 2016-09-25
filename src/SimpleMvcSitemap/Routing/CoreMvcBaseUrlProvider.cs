using System;
using Microsoft.AspNetCore.Http;

namespace SimpleMvcSitemap.Routing
{
    class CoreMvcBaseUrlProvider : IBaseUrlProvider
    {
        private readonly HttpRequest request;

        public CoreMvcBaseUrlProvider(HttpRequest request)
        {
            this.request = request;
        }

        public Uri BaseUrl => new Uri($"{request.Scheme}://{request.Host.Value}{request.PathBase}");
    }
}
