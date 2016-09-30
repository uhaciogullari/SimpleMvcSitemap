#if Mvc
using System.Web.Mvc;
#endif

#if CoreMvc
using Microsoft.AspNetCore.Mvc;
#endif

using System;
using SimpleMvcSitemap.Routing;


namespace SimpleMvcSitemap
{
    /// <inheritDoc/>
    public class SitemapProvider : ISitemapProvider
    {
        private readonly IBaseUrlProvider baseUrlProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="SitemapProvider"/> class.
        /// </summary>
        public SitemapProvider()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SitemapProvider"/> class.
        /// </summary>
        public SitemapProvider(IBaseUrlProvider baseUrlProvider)
        {
            this.baseUrlProvider = baseUrlProvider;
        }


        /// <inheritDoc/>
        public ActionResult CreateSitemap(SitemapModel sitemapModel)
        {
            if (sitemapModel == null)
            {
                throw new ArgumentNullException(nameof(sitemapModel));
            }

            return new XmlResult<SitemapModel>(sitemapModel, baseUrlProvider);
        }


        /// <inheritDoc/>
        public ActionResult CreateSitemapIndex(SitemapIndexModel sitemapIndexModel)
        {
            if (sitemapIndexModel == null)
            {
                throw new ArgumentNullException(nameof(sitemapIndexModel));
            }

            return new XmlResult<SitemapIndexModel>(sitemapIndexModel, baseUrlProvider);
        }
    }
}