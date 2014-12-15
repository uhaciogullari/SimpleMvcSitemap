using System.Web;
using System.Web.Mvc;

namespace SimpleMvcSitemap
{
    public class BaseUrlProvider : IBaseUrlProvider
    {
        public string GetBaseUrl(HttpContextBase httpContext)
        {
            //http://stackoverflow.com/a/1288383/205859
            HttpRequestBase request = httpContext.Request;
            return string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, UrlHelper.GenerateContentUrl("~", httpContext)).TrimEnd('/');
        }
    }
}