﻿using System.Web.Mvc;

namespace SimpleMvcSitemap.Sample.SampleBusiness
{
    public class SitemapConfiguration : ISitemapConfiguration<string>
    {
        private readonly UrlHelper _urlHelper;

        public SitemapConfiguration(int? currentPage, UrlHelper urlHelper, bool? revertIndex)
        {
            _urlHelper = urlHelper;
            CurrentPage = currentPage;
            Size = 1;
	        RevertIndex = revertIndex ?? false;
        }

        public int? CurrentPage { get; private set; }

        public int Size { get; private set; }
	    public bool RevertIndex { get; private set; }

        public string CreateSitemapUrl(int currentPage)
        {
            return _urlHelper.Action("StaticPages", "Home", new { id = currentPage });
        }

        public SitemapNode CreateNode(string source)
        {
            return new SitemapNode("url");
        }
    }
}