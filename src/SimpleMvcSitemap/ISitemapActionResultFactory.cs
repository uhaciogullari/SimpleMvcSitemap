using Microsoft.AspNetCore.Mvc;

namespace SimpleMvcSitemap
{
    public interface ISitemapActionResultFactory
    {
        ActionResult CreateSitemapResult<T>(T data);
    }
}