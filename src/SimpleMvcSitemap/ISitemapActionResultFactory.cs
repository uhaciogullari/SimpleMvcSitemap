#if CoreMvc
using Microsoft.AspNetCore.Mvc;
#endif
#if Mvc
using System.Web.Mvc;
#endif



namespace SimpleMvcSitemap
{
    interface ISitemapActionResultFactory
    {
        ActionResult CreateSitemapResult<T>(T data);
    }
}