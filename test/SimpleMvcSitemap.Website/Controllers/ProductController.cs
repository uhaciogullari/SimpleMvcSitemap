using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SimpleMvcSitemap.Sample.Models;
using SimpleMvcSitemap.Website.SampleBusiness;

namespace SimpleMvcSitemap.Website.Controllers
{
    [Route("product-sitemap")]
    public class ProductController : Controller
    {
        [Route("{id?}")]
        public ActionResult Index(int? id)
        {
            var products = CreateProducts(200).ToList().AsQueryable();
            var dataSource = products.Where(item => item.Status == ProductStatus.Active);
            var productSitemapIndexConfiguration = new ProductSitemapIndexConfiguration(dataSource, id, Url);
            return new DynamicSitemapIndexProvider().CreateSitemapIndex(new SitemapProvider(new BaseUrlProvider()), productSitemapIndexConfiguration);
        }

        [Route("product-detail/{id}")]
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