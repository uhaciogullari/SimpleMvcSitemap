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

        public string BaseUrl => $"{request.Scheme}://{request.Host.Value}{request.PathBase}";
    }
}
