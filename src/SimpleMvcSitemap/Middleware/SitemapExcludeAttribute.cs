using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMvcSitemap.Middleware
{
    /// <summary>
    /// Excludes a controller or action from being output in a sitemap.xml file when using the automatic sitemap generator
    /// </summary>
    public class SitemapExcludeAttribute : Attribute
    {
    }
}
