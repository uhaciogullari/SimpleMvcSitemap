using System.Web;

namespace SimpleMvcSitemap
{
    interface IBaseUrlProvider
    {
        string GetBaseUrl(HttpContextBase httpContext);
    }
}