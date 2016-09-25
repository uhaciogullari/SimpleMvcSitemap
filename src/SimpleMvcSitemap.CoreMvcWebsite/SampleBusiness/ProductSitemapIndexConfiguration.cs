using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SimpleMvcSitemap.Sample.Models;
using SimpleMvcSitemap.StyleSheets;

namespace SimpleMvcSitemap.Website.SampleBusiness
{
    public class ProductSitemapIndexConfiguration : ISitemapIndexConfiguration<Product>
    {
        private readonly IUrlHelper _urlHelper;

        public ProductSitemapIndexConfiguration(IUrlHelper urlHelper, int? currentPage)
        {
            _urlHelper = urlHelper;
            Size = 50000;
            CurrentPage = currentPage;
        }

        public IQueryable<Product> DataSource { get; }
        public int? CurrentPage { get; private set; }

        public int Size { get; private set; }

        public SitemapIndexNode CreateSitemapIndexNode(int currentPage)
        {
            return new SitemapIndexNode(_urlHelper.RouteUrl("ProductSitemap", new { currentPage }));
        }

        public SitemapNode CreateNode(Product source)
        {
            return new SitemapNode(_urlHelper.RouteUrl("Product", new { id = source.Id }));
        }

        public List<XmlStyleSheet> SitemapStyleSheets { get; }
        public List<XmlStyleSheet> SitemapIndexStyleSheets { get; }
        public bool UseReverseOrderingForSitemapIndexNodes { get; }
    }
}