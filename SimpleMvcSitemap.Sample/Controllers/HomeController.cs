using System.Web.Mvc;
using SimpleMvcSitemap.Sample.SampleBusiness;

namespace SimpleMvcSitemap.Sample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISampleSitemapNodeBuilder _builder;
        private readonly ISitemapProvider _sitemapProvider;

        public HomeController()
            : this(new SitemapProvider(), new SampleSitemapNodeBuilder()) { }

        public HomeController(ISitemapProvider sitemapProvider, ISampleSitemapNodeBuilder sampleSitemapNodeBuilder)
        {
            _sitemapProvider = sitemapProvider;
            _builder = sampleSitemapNodeBuilder;
        }

        public ActionResult Index()
        {
            return _sitemapProvider.CreateSitemap(HttpContext, _builder.BuildSitemapIndex());
        }

        public ActionResult Categories()
        {
            return _sitemapProvider.CreateSitemap(HttpContext, _builder.BuildSitemapNodes());
        }

        public ActionResult Brands()
        {
            return _sitemapProvider.CreateSitemap(HttpContext, _builder.BuildSitemapNodes());
        }
    }
}