using System.Web;
using System.Web.Mvc;

namespace SimpleMvcSitemap.Routing
{
    public class MvcAbsoluteUrlConverter : AbsoluteUrlConverterBase, IAbsoluteUrlConverter
    {
        private readonly HttpContextBase httpContext;

        public MvcAbsoluteUrlConverter(HttpContextBase httpContext)
        {
            this.httpContext = httpContext;
        }


        public string ConvertToAbsoluteUrl(string relativeUrl)
        {
            HttpRequestBase request = httpContext.Request;
            string baseUrl = $"{request.Url.Scheme}://{request.Url.Authority}{UrlHelper.GenerateContentUrl("~", httpContext)}".TrimEnd('/');

            return CreateAbsoluteUrl(baseUrl, relativeUrl);
        }
    }
}