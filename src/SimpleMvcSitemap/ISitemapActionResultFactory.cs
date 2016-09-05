using Microsoft.AspNetCore.Mvc;

namespace SimpleMvcSitemap
{
    interface ISitemapActionResultFactory
    {
        ActionResult CreateSitemapResult<T>(ActionContext actionContext, T data);
    }
}