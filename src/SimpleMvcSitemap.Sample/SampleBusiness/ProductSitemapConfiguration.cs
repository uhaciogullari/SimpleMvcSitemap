using System.Web.Mvc;
using SimpleMvcSitemap.Sample.Models;

namespace SimpleMvcSitemap.Sample.SampleBusiness
{
    public class ProductSitemapConfiguration : ISitemapConfiguration<Product>
    {
        private readonly UrlHelper _urlHelper;

        public ProductSitemapConfiguration(UrlHelper urlHelper, int? currentPage, bool? revertIndex)
        {
            _urlHelper = urlHelper;
            Size = 50000;
            CurrentPage = currentPage;
            RevertIndex = revertIndex ?? false;
        }

        public int? CurrentPage { get; private set; }

        public int Size { get; private set; }
        public bool RevertIndex { get; private set; }

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