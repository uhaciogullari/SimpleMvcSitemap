
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SimpleMvcSitemap.StyleSheets;

namespace SimpleMvcSitemap.Website.SampleBusiness
{
    public class SitemapIndexConfiguration : ISitemapIndexConfiguration<string>
    {
        private readonly IUrlHelper _urlHelper;

        public SitemapIndexConfiguration(int? currentPage, IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
            CurrentPage = currentPage;
            Size = 1;
        }

        public IQueryable<string> DataSource { get; }
        public int? CurrentPage { get; private set; }

        public int Size { get; private set; }

        public SitemapIndexNode CreateSitemapIndexNode(int currentPage)
        {
            return new SitemapIndexNode(_urlHelper.Action("StaticPages", "Home", new { id = currentPage }));
        }

        public SitemapNode CreateNode(string source)
        {
            return new SitemapNode("url");
        }

        public List<XmlStyleSheet> SitemapStyleSheets { get; }
        public List<XmlStyleSheet> SitemapIndexStyleSheets { get; }
        public bool UseReverseOrderingForSitemapNodes { get; }
    }
}