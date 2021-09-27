using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SimpleMvcSitemap.Tests;

namespace SimpleMvcSitemap.Website.Controllers
{
    [Route("sitemap")]
    public class SitemapController : Controller
    {
        private readonly ISitemapProvider sitemapProvider;

        private TestDataBuilder dataBuilder;


        public SitemapController(ISitemapProvider sitemapProvider)
        {
            this.sitemapProvider = sitemapProvider;
            dataBuilder = new TestDataBuilder();
        }

        
        public ActionResult Index()
        {
            return sitemapProvider.CreateSitemapIndex(new SitemapIndexModel(new List<SitemapIndexNode>
            {
                new SitemapIndexNode(Url.Action("Default")),
                new SitemapIndexNode(Url.Action("Image")),
                new SitemapIndexNode(Url.Action("Video")),
                new SitemapIndexNode(Url.Action("News")),
                new SitemapIndexNode(Url.Action("Translation")),
                new SitemapIndexNode(Url.Action("StyleSheet")),
                new SitemapIndexNode(Url.Action("Huge")),
            }));
        }

        [Route("default")]
        public ActionResult Default()
        {
            return sitemapProvider.CreateSitemap(new SitemapModel(new List<SitemapNode>
            {
                dataBuilder.CreateSitemapNodeWithRequiredProperties(),
                dataBuilder.CreateSitemapNodeWithAllProperties()
            }));
        }


        [Route("image")]
        public ActionResult Image()
        {
            return sitemapProvider.CreateSitemap(new SitemapModel(new List<SitemapNode>
            {
                dataBuilder.CreateSitemapNodeWithImageRequiredProperties(),
                dataBuilder.CreateSitemapNodeWithImageAllProperties()
            }));
        }

        [Route("video")]
        public ActionResult Video()
        {
            return sitemapProvider.CreateSitemap(new SitemapModel(new List<SitemapNode>
            {
                dataBuilder.CreateSitemapNodeWithVideoRequiredProperties(),
                dataBuilder.CreateSitemapNodeWithVideoAllProperties()
            }));
        }

        [Route("news")]
        public ActionResult News()
        {
            return sitemapProvider.CreateSitemap(new SitemapModel(new List<SitemapNode>
            {
                dataBuilder.CreateSitemapNodeWithNewsRequiredProperties(),
                dataBuilder.CreateSitemapNodeWithNewsAllProperties()
            }));
        }

        [Route("translation")]
        public ActionResult Translation()
        {
            return sitemapProvider.CreateSitemap(dataBuilder.CreateSitemapWithTranslations());
        }

        [Route("stylesheet")]
        public ActionResult StyleSheet()
        {
            return sitemapProvider.CreateSitemap(dataBuilder.CreateSitemapWithSingleStyleSheet());
        }

        [Route("huge")]
        public ActionResult Huge()
        {
            return sitemapProvider.CreateSitemap(dataBuilder.CreateHugeSitemap());
        }

        //[Route("sitemapcategories")]
        //public ActionResult Categories()
        //{
        //    return _sitemapProvider.CreateSitemap(_builder.BuildSitemapModel());
        //}

        //[Route("sitemapbrands")]
        //public ActionResult Brands()
        //{
        //    return _sitemapProvider.CreateSitemap(_builder.BuildSitemapModel());
        //}

        //public ActionResult Products(int? currentPage)
        //{
        //    IQueryable<Product> dataSource = _products.Where(item => item.Status == ProductStatus.Active);
        //    ProductSitemapIndexConfiguration configuration = new ProductSitemapIndexConfiguration(Url, currentPage);

        //    return _sitemapProvider.CreateSitemap(dataSource, configuration);
        //}

        public ActionResult StaticPages(int? id)
        {
            return sitemapProvider.CreateSitemap(dataBuilder.CreateSitemapWithMultipleStyleSheets());
        }
    }
}