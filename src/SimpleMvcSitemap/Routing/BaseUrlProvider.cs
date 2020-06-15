using System;
using Microsoft.AspNetCore.Http;

namespace SimpleMvcSitemap.Routing
{
    class BaseUrlProvider : IBaseUrlProvider
    {
        private readonly HttpRequest request;

        public BaseUrlProvider(HttpRequest request)
        {
            this.request = request;
        }

        public Uri BaseUrl => new Uri($"{request.Scheme}://{request.Host.Value}{request.PathBase}");
    }
}
