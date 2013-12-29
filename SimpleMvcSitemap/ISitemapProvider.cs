using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace SimpleMvcSitemap
{
    public interface ISitemapProvider
    {
        ActionResult CreateSitemap(HttpContextBase httpContext, IEnumerable<SitemapNode> nodes, IEnumerable<XmlSerializerNamespace> namespaces = null);

        ActionResult CreateSitemap(HttpContextBase httpContext, IEnumerable<SitemapNode> nodes,
            ISitemapConfiguration configuration, IEnumerable<XmlSerializerNamespace> namespaces = null);

        ActionResult CreateSitemap(HttpContextBase httpContext, IEnumerable<SitemapIndexNode> nodes);
    }
}