#if Mvc
using System.Web.Mvc;
#endif

#if CoreMvc
using Microsoft.AspNetCore.Mvc;
#endif

using System;
using System.Collections.Generic;
using System.Linq;
using SimpleMvcSitemap.StyleSheets;

namespace SimpleMvcSitemap
{
    public class DynamicSitemapIndexProvider : IDynamicSitemapIndexProvider
    {
        public ActionResult CreateSitemapIndex<T>(ISitemapProvider sitemapProvider, ISitemapIndexConfiguration<T> sitemapIndexConfiguration)
        {
            if (sitemapProvider == null)
            {
                throw new ArgumentNullException(nameof(sitemapProvider));
            }

            if (sitemapIndexConfiguration == null)
            {
                throw new ArgumentNullException(nameof(sitemapIndexConfiguration));
            }

            var count = sitemapIndexConfiguration.DataSource.Count();

            if (sitemapIndexConfiguration.Size >= count)
            {
                return CreateSitemap(sitemapProvider, sitemapIndexConfiguration, sitemapIndexConfiguration.DataSource.ToList());
            }

            if (sitemapIndexConfiguration.CurrentPage.HasValue && sitemapIndexConfiguration.CurrentPage.Value > 0)
            {

            }





            throw new System.NotImplementedException();
        }

        private ActionResult CreateSitemap<T>(ISitemapProvider sitemapProvider, ISitemapIndexConfiguration<T> sitemapIndexConfiguration, List<T> items)
        {
            var sitemapNodes = items.Select(sitemapIndexConfiguration.CreateNode).ToList();

            return sitemapProvider.CreateSitemap(new SitemapModel(sitemapNodes)
            {
                StyleSheets = sitemapIndexConfiguration.SitemapStyleSheets
            });
        }

        private SitemapIndexModel CreateSitemapIndex<T>(ISitemapIndexConfiguration<T> sitemapIndexConfiguration, int sitemapCount)
        {
            return new SitemapIndexModel();
        }

        private List<XmlStyleSheet> GetXmlStyleSheets(Func<List<XmlStyleSheet>> getter)
        {
            try
            {
                return getter();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}