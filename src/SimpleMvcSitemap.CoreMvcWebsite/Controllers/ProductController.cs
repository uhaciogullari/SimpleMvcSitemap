using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SimpleMvcSitemap.Sample.Models;
using SimpleMvcSitemap.Website.SampleBusiness;

namespace SimpleMvcSitemap.Website.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index(int? id)
        {
            var products = CreateProducts(200).ToList().AsQueryable();
            var dataSource = products.Where(item => item.Status == ProductStatus.Active);
            var productSitemapIndexConfiguration = new ProductSitemapIndexConfiguration(dataSource, id, Url);
            return new DynamicSitemapIndexProvider().CreateSitemapIndex(new SitemapProvider(), productSitemapIndexConfiguration);
        }

        public ActionResult Detail(int id)
        {
            return new EmptyResult();
        }

        private IEnumerable<Product> CreateProducts(int count)
        {
            return Enumerable.Range(1, count).Select(i => new Product { Id = i });
        }
    }
}