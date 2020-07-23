using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SimpleMvcSitemap.Website.Models;

namespace SimpleMvcSitemap.Website.SampleBusiness
{
    public class ProductSitemapIndexConfiguration : SitemapIndexConfiguration<Product>
    {
        private readonly IUrlHelper urlHelper;

        public ProductSitemapIndexConfiguration(IQueryable<Product> dataSource, int? currentPage, IUrlHelper urlHelper)
            : base(dataSource, currentPage)
        {
            this.urlHelper = urlHelper;
            Size = 45;
        }

        public override SitemapIndexNode CreateSitemapIndexNode(int currentPage)
        {
            return new SitemapIndexNode(urlHelper.Action("Index", "Product", new { id = currentPage }));
        }

        public override SitemapNode CreateNode(Product source)
        {
            return new SitemapNode(urlHelper.Action("Detail", "Product", new { id = source.Id }));
        }
    }
}