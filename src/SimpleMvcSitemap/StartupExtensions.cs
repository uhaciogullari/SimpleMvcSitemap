using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using SimpleMvcSitemap.Routing;

namespace SimpleMvcSitemap
{
    public static class StartupExtensions
    {
        public static void AddSimpleMvcSitemap(this IServiceCollection services)
        {
            services.AddSingleton<ISitemapProvider, SitemapProvider>();
            services.AddSingleton<ISitemapActionResultFactory, SitemapActionResultFactory>();
            services.AddSingleton<IUrlValidator, UrlValidator>();
            services.AddSingleton<IReflectionHelper, ReflectionHelper>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }
    }
}