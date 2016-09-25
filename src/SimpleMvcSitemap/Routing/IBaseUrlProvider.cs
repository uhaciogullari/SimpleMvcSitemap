using System;

namespace SimpleMvcSitemap.Routing
{
    public interface IBaseUrlProvider
    {
        Uri BaseUrl { get; }
    }
}