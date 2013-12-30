using System.Web.Mvc;
using System.Web.Routing;

namespace SimpleMvcSitemap.Sample.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("sitemapcategories", "sitemapcategories",
                new { controller = "Home", action = "Categories", id = UrlParameter.Optional }
                );

            routes.MapRoute("sitemapbrands", "sitemapbrands",
                new { controller = "Home", action = "Brands", id = UrlParameter.Optional }
                );

            routes.MapRoute("Default", "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );

        }
    }
}