using System.Web;

namespace SimpleMvcSitemap
{
    /// <summary>
    /// Provides the base URL for converting relative URLs to absolute ones
    /// </summary>
    public interface IBaseUrlProvider
    {
        /// <summary>
        /// Gets the base URL from ASP.NET HTTP context.
        /// </summary>
        /// <param name="httpContext">ASP.NET HTTP context.</param>
        string GetBaseUrl(HttpContextBase httpContext);
    }
}