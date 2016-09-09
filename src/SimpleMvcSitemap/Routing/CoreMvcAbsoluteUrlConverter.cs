using Microsoft.AspNetCore.Http;

namespace SimpleMvcSitemap.Routing
{
    class CoreMvcAbsoluteUrlConverter : AbsoluteUrlConverterBase, IAbsoluteUrlConverter
    {
        private readonly HttpRequest _request;

        public CoreMvcAbsoluteUrlConverter(HttpRequest request)
        {
            _request = request;
        }


        public string ConvertToAbsoluteUrl(string relativeUrl)
        {
            string baseUrl = $"{_request.Scheme}://{_request.Host.Value}/{_request.PathBase}".TrimEnd('/');
            return CreateAbsoluteUrl(baseUrl, relativeUrl);
        }
    }
}