using System.Web;

namespace SimpleMvcSitemap
{
    /// <summary>
    /// Checks an object for URL properties marked with UrlAttribute and
    /// converts relative URLs to absolute ones.
    /// </summary>
    public interface IUrlValidator
    {
        /// <summary>
        /// Validates the urls.
        /// </summary>
        /// <param name="httpContext">ASP.NET HTTP context.</param>
        /// <param name="item">An object containing URLs.</param>
        void ValidateUrls(HttpContextBase httpContext, object item);
    }
}