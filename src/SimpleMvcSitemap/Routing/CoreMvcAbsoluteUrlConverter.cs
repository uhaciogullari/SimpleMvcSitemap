using Microsoft.AspNetCore.Http;

namespace SimpleMvcSitemap.Routing
{
    class CoreMvcAbsoluteUrlConverter : AbsoluteUrlConverterBase, IAbsoluteUrlConverter
    {
        private readonly HttpRequest request;

        public CoreMvcAbsoluteUrlConverter(HttpRequest request)
        {
            this.request = request;
        }


        public string ConvertToAbsoluteUrl(string relativeUrl)
        {
            string baseUrl = $"{request.Scheme}://{request.Host.Value}{request.PathBase}".TrimEnd('/');
            return CreateAbsoluteUrl(baseUrl, relativeUrl);
        }
    }
}