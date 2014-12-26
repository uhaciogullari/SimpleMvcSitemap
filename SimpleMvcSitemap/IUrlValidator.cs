using System.Web;

namespace SimpleMvcSitemap
{
    public interface IUrlValidator
    {
        void ValidateUrls(HttpContextBase httpContext, object item);
    }
}