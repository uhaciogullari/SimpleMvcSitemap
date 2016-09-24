#if Mvc
using System.Web.Mvc;
#endif

#if CoreMvc
using Microsoft.AspNetCore.Mvc;
#endif

using System;


namespace SimpleMvcSitemap
{
    /// <inheritDoc/>
    public class SitemapProvider : ISitemapProvider
    {
        /// <inheritDoc/>
        public ActionResult CreateSitemap(SitemapModel sitemapModel)
        {
            if (sitemapModel == null)
            {
                throw new ArgumentNullException(nameof(sitemapModel));
            }

            return new XmlResult<SitemapModel>(sitemapModel);
        }


        /// <inheritDoc/>
        public ActionResult CreateSitemapIndex(SitemapIndexModel sitemapIndexModel)
        {
            if (sitemapIndexModel == null)
            {
                throw new ArgumentNullException(nameof(sitemapIndexModel));
            }

            return new XmlResult<SitemapIndexModel>(sitemapIndexModel);
        }
    }
}