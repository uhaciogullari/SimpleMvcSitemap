using System;
using Microsoft.AspNetCore.Http;

namespace SimpleMvcSitemap
{
    /// <summary>
    /// Provides the base URL for converting relative URLs to absolute ones
    /// </summary>
    public class BaseUrlProvider : IBaseUrlProvider
    {
        /// <summary>
        /// Gets the base URL from ASP.NET HTTP context.
        /// </summary>
        /// <param name="httpContext">ASP.NET HTTP context.</param>
        public string GetBaseUrl(HttpContext httpContext)
        {
            throw new NotImplementedException();
            //http://stackoverflow.com/a/1288383/205859
            //HttpRequestBase request = httpContext.Request;
            //return $"{request.Url.Scheme}://{request.Url.Authority}{UrlHelper.GenerateContentUrl("~", httpContext)}".TrimEnd('/');
        }
    }
}