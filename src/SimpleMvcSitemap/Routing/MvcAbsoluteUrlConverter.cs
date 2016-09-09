using System.Web;
using System.Web.Mvc;

namespace SimpleMvcSitemap.Routing
{
    public class MvcAbsoluteUrlConverter : AbsoluteUrlConverterBase, IAbsoluteUrlConverter
    {
        private readonly HttpContextBase _httpContext;

        public MvcAbsoluteUrlConverter(HttpContextBase httpContext)
        {
            _httpContext = httpContext;
        }


        public string ConvertToAbsoluteUrl(string relativeUrl)
        {
            HttpRequestBase request = _httpContext.Request;
            string baseUrl = $"{request.Url.Scheme}://{request.Url.Authority}{UrlHelper.GenerateContentUrl("~", _httpContext)}".TrimEnd('/');

            return CreateAbsoluteUrl(baseUrl, relativeUrl);
        }
    }
}