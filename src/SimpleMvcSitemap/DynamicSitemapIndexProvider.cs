﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace SimpleMvcSitemap
{
    /// <inheritDoc/>
    public class DynamicSitemapIndexProvider : IDynamicSitemapIndexProvider
    {
        /// <inheritDoc/>
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

            var nodeCount = sitemapIndexConfiguration.DataSource.Count();

            if (sitemapIndexConfiguration.Size >= nodeCount)
            {
                return CreateSitemap(sitemapProvider, sitemapIndexConfiguration, sitemapIndexConfiguration.DataSource.ToList());
            }

            if (sitemapIndexConfiguration.CurrentPage.HasValue && sitemapIndexConfiguration.CurrentPage.Value > 0)
            {
                int skipCount = (sitemapIndexConfiguration.CurrentPage.Value - 1) * sitemapIndexConfiguration.Size;
                var items = sitemapIndexConfiguration.DataSource.Skip(skipCount).Take(sitemapIndexConfiguration.Size).ToList();
                return CreateSitemap(sitemapProvider,sitemapIndexConfiguration,items);
            }


            int pageCount = (int)Math.Ceiling((double)nodeCount / sitemapIndexConfiguration.Size);
            return sitemapProvider.CreateSitemapIndex(CreateSitemapIndex(sitemapIndexConfiguration, pageCount));
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
            var pageIndexes = Enumerable.Range(1, sitemapCount);

            if (sitemapIndexConfiguration.UseReverseOrderingForSitemapIndexNodes)
            {
                pageIndexes = pageIndexes.Reverse();
            }

            var sitemapIndexNodes = pageIndexes.Select(sitemapIndexConfiguration.CreateSitemapIndexNode).ToList();

            return new SitemapIndexModel(sitemapIndexNodes)
            {
                StyleSheets = sitemapIndexConfiguration.SitemapIndexStyleSheets
            };
        }
    }
}