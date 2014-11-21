using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleMvcSitemap
{
    public interface ISitemapProvider
    {
        ActionResult CreateSitemap(HttpContextBase httpContext, IEnumerable<SitemapNode> nodes);

        ActionResult CreateSitemap(HttpContextBase httpContext, IQueryable<SitemapNode> nodes, ISitemapConfiguration configuration);

        ActionResult CreateSitemap(HttpContextBase httpContext, IEnumerable<SitemapIndexNode> nodes);
    }
}