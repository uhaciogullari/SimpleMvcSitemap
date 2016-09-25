using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SimpleMvcSitemap.Sample.Models;
using SimpleMvcSitemap.Sample.SampleBusiness;
using SimpleMvcSitemap.Tests;

namespace SimpleMvcSitemap.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISampleSitemapNodeBuilder _builder;
        private readonly ISitemapProvider _sitemapProvider;

        private readonly IQueryable<Product> _products;
        private TestDataBuilder dataBuilder;


        public HomeController(ISitemapProvider sitemapProvider)
        {
            _sitemapProvider = sitemapProvider;
            dataBuilder = new TestDataBuilder();

            _products = new List<Product>().AsQueryable();
        }

        public ActionResult Index()
        {
            return _sitemapProvider.CreateSitemapIndex(new SitemapIndexModel(new List<SitemapIndexNode>
            {
                new SitemapIndexNode(Url.Action("Default")),
                new SitemapIndexNode(Url.Action("Image")),
                new SitemapIndexNode(Url.Action("Video")),
                new SitemapIndexNode(Url.Action("News")),
                new SitemapIndexNode(Url.Action("Mobile")),
                new SitemapIndexNode(Url.Action("Translation")),
                new SitemapIndexNode(Url.Action("StyleSheet")),
            }));
        }

        public ActionResult Default()
        {
            return _sitemapProvider.CreateSitemap(new SitemapModel(new List<SitemapNode>
            {
                dataBuilder.CreateSitemapNodeWithRequiredProperties(),
                dataBuilder.CreateSitemapNodeWithAllProperties()
            }));
        }


        public ActionResult Image()
        {
            return _sitemapProvider.CreateSitemap(new SitemapModel(new List<SitemapNode>
            {
                dataBuilder.CreateSitemapNodeWithImageRequiredProperties(),
                dataBuilder.CreateSitemapNodeWithImageAllProperties()
            }));
        }

        public ActionResult Video()
        {
            return _sitemapProvider.CreateSitemap(new SitemapModel(new List<SitemapNode>
            {
                dataBuilder.CreateSitemapNodeWithVideoRequiredProperties(),
                dataBuilder.CreateSitemapNodeWithVideoAllProperties()
            }));
        }

        public ActionResult News()
        {
            return _sitemapProvider.CreateSitemap(new SitemapModel(new List<SitemapNode>
            {
                dataBuilder.CreateSitemapNodeWithNewsRequiredProperties(),
                dataBuilder.CreateSitemapNodeWithNewsAllProperties()
            }));
        }

        public ActionResult Mobile()
        {
            return _sitemapProvider.CreateSitemap(new SitemapModel(new List<SitemapNode>
            {
                dataBuilder.CreateSitemapNodeWithMobile()
            }));
        }

        public ActionResult Translation()
        {
            return _sitemapProvider.CreateSitemap(dataBuilder.CreateSitemapWithTranslations());
        }

        public ActionResult StyleSheet()
        {
            return _sitemapProvider.CreateSitemap(dataBuilder.CreateSitemapWithSingleStyleSheet());
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

        //public ActionResult Products(int? currentPage)
        //{
        //    IQueryable<Product> dataSource = _products.Where(item => item.Status == ProductStatus.Active);
        //    ProductSitemapConfiguration configuration = new ProductSitemapConfiguration(Url, currentPage);

        //    return _sitemapProvider.CreateSitemap(dataSource, configuration);
        //}

        //public ActionResult StaticPages(int? id)
        //{
        //    IQueryable<string> urls = new List<string> { "/1", "/1", "/1", "/1", "/1" }.AsQueryable();
        //    return _sitemapProvider.CreateSitemap(urls, new SitemapConfiguration(id, Url));
        //}
    }
}