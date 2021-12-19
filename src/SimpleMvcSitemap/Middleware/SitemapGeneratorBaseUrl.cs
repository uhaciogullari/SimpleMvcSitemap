using SimpleMvcSitemap.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMvcSitemap.Middleware
{
    internal class SitemapGeneratorBaseUrl : IBaseUrlProvider
    {
        public Uri BaseUrl { get; set; }
    }
}
