using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SimpleMvcSitemap.Sample.Models;
using SimpleMvcSitemap.Sample.SampleBusiness;
using SimpleMvcSitemap.Website.SampleBusiness;

namespace SimpleMvcSitemap.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISampleSitemapNodeBuilder _builder;
        private readonly ISitemapProvider _sitemapProvider;

        private readonly IQueryable<Product> _products;


        public HomeController(ISitemapProvider sitemapProvider, ISampleSitemapNodeBuilder sampleSitemapNodeBuilder)
        {
            _sitemapProvider = sitemapProvider;
            _builder = sampleSitemapNodeBuilder;

            _products = new List<Product>().AsQueryable();
        }

        public ActionResult Index()
        {
            return _sitemapProvider.CreateSitemap(_builder.BuildSitemapIndex());
        }

        [Route("sitemapcategories")]
        public ActionResult Categories()
        {
            return _sitemapProvider.CreateSitemap(_builder.BuildSitemapModel());
        }

        [Route("sitemapbrands")]
        public ActionResult Brands()
        {
            return _sitemapProvider.CreateSitemap(_builder.BuildSitemapModel());
        }

        public ActionResult Products(int? currentPage)
        {
            IQueryable<Product> dataSource = _products.Where(item => item.Status == ProductStatus.Active);
            ProductSitemapConfiguration configuration = new ProductSitemapConfiguration(Url, currentPage);

            return _sitemapProvider.CreateSitemap(dataSource, configuration);
        }

        public ActionResult StaticPages(int? id)
        {
            IQueryable<string> urls = new List<string> { "/1", "/1", "/1", "/1", "/1" }.AsQueryable();
            return _sitemapProvider.CreateSitemap(urls, new SitemapConfiguration(id, Url));
        }
    }
}