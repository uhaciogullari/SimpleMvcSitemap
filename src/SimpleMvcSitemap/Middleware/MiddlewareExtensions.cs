using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMvcSitemap.Middleware
{
    /// <summary>
    /// Sitemap Auto generator middleware
    /// </summary>
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// Activates the automatic sitemap generator. Usage: app.UseSitemap() within your application startup.
        /// Uses default SitemapGeneratorOptions
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSitemap(this IApplicationBuilder builder) => UseMiddlewareExtensions.UseMiddleware<SitemapMiddleware>(builder);

        /// <summary>
        /// Activates the automatic sitemap generator. Usage: app.UseSitemap() within your application startup.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="sitemapOptions">Sitemap Generator options</param>
        /// <returns></returns>
        public static IApplicationBuilder UseSitemap(this IApplicationBuilder builder, SitemapGeneratorOptions sitemapOptions)
        {
            return UseMiddlewareExtensions.UseMiddleware<SitemapMiddleware>(builder, sitemapOptions);
        }
    }
}
