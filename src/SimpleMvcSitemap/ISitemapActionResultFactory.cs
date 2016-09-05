using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SimpleMvcSitemap
{
    interface ISitemapActionResultFactory
    {
        ActionResult CreateSitemapResult<T>(HttpContext httpContext, T data);
    }
}