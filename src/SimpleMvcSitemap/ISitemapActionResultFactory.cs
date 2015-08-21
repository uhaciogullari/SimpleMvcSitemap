using System.Web;
using System.Web.Mvc;

namespace SimpleMvcSitemap
{
    interface ISitemapActionResultFactory
    {
        ActionResult CreateSitemapResult<T>(HttpContextBase httpContext, T data);
    }
}