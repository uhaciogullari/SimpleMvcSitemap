using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace SimpleMvcSitemap
{
    public interface ISitemapProvider
    {
        ActionResult CreateSitemap(HttpContextBase httpContext, IEnumerable<SitemapNode> nodes);
        
        ActionResult CreateSitemap(HttpContextBase httpContext, IEnumerable<SitemapNode> nodes, 
                                   ISitemapConfiguration configuration);
    }
}