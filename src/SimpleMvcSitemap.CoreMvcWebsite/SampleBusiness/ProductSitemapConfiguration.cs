using Microsoft.AspNetCore.Mvc;
using SimpleMvcSitemap.Sample.Models;

namespace SimpleMvcSitemap.Website.SampleBusiness
{
    public class ProductSitemapConfiguration : ISitemapConfiguration<Product>
    {
        private readonly IUrlHelper _urlHelper;

        public ProductSitemapConfiguration(IUrlHelper urlHelper, int? currentPage)
        {
            _urlHelper = urlHelper;
            Size = 50000;
            CurrentPage = currentPage;
        }

        public int? CurrentPage { get; private set; }

        public int Size { get; private set; }

        public string CreateSitemapUrl(int currentPage)
        {
            return _urlHelper.RouteUrl("ProductSitemap", new { currentPage });
        }

        public SitemapNode CreateNode(Product source)
        {
            return new SitemapNode(_urlHelper.RouteUrl("Product", new { id = source.Id }));
        }
    }
}