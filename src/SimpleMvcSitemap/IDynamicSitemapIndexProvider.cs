#if Mvc
using System.Web.Mvc;
#endif

#if CoreMvc
using Microsoft.AspNetCore.Mvc;
#endif

namespace SimpleMvcSitemap
{
    public interface IDynamicSitemapIndexProvider
    {
        ActionResult CreateSitemapIndex<T>(ISitemapProvider sitemapProvider, ISitemapIndexConfiguration<T> sitemapIndexConfiguration);
    }
}
