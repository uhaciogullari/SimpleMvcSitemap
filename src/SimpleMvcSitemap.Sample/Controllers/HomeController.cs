using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SimpleMvcSitemap.Sample.Models;
using SimpleMvcSitemap.Sample.SampleBusiness;

namespace SimpleMvcSitemap.Sample.Controllers
{
	public class HomeController : Controller
	{
		private readonly ISampleSitemapNodeBuilder _builder;
		private readonly ISitemapProvider _sitemapProvider;
		private IQueryable<Product> _products;

		public HomeController()
			: this(new SitemapProvider(), new SampleSitemapNodeBuilder()) { }

		public HomeController(ISitemapProvider sitemapProvider, ISampleSitemapNodeBuilder sampleSitemapNodeBuilder)
		{
			_sitemapProvider = sitemapProvider;
			_builder = sampleSitemapNodeBuilder;
			_products = new List<Product>().AsQueryable();
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

		public ActionResult Products(int? currentPage)
		{
			IQueryable<Product> dataSource = _products.Where(item => item.Status == ProductStatus.Active);
			ProductSitemapConfiguration configuration = new ProductSitemapConfiguration(Url, currentPage, false);

			return new SitemapProvider().CreateSitemap(HttpContext, dataSource, configuration);
		}

		public ActionResult StaticPages(int? id)
		{
			IQueryable<string> urls = new List<string> { "/1", "/1", "/1", "/1", "/1" }.AsQueryable();
			return _sitemapProvider.CreateSitemap(HttpContext, urls, new SitemapConfiguration(id, Url, false));
		}
	}
}