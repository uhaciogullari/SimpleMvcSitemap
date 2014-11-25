using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleMvcSitemap
{
    public interface ISitemapProvider
    {
        ActionResult CreateSitemap(HttpContextBase httpContext, IEnumerable<SitemapNode> nodes);

        ActionResult CreateSitemap<T>(HttpContextBase httpContext, IQueryable<T> nodes, ISitemapConfiguration<T> configuration);

        ActionResult CreateSitemap(HttpContextBase httpContext, IEnumerable<SitemapIndexNode> nodes);
    }
}