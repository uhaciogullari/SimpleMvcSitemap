using System;

namespace SimpleMvcSitemap.Routing
{
    /// <summary>
    /// An abstraction for retrieving the Base URL that will be
    /// used for creating absolute URLs
    /// </summary>
    public interface IBaseUrlProvider
    {
        /// <summary>
        /// Gets the base URL.
        /// </summary>
        Uri BaseUrl { get; }
    }
}