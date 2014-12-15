using System.Web;

namespace SimpleMvcSitemap
{
    public interface IBaseUrlProvider
    {
        string GetBaseUrl(HttpContextBase httpContext);
    }
}