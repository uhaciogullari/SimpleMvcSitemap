using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace SimpleMvcSitemap
{
    public interface ISitemapProvider
    {
        ActionResult CreateSiteMap(HttpContextBase httpContext, IList<SitemapNode> nodeList);
    }
}