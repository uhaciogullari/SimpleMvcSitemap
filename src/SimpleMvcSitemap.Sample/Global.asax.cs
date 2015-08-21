using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SimpleMvcSitemap.Sample.App_Start;

namespace SimpleMvcSitemap.Sample
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}