using System;
using SimpleMvcSitemap.Routing;

namespace SimpleMvcSitemap.Website.SampleBusiness
{
    public class BaseUrlProvider : IBaseUrlProvider
    {
        public Uri BaseUrl => new Uri("http://example.com");
    }
}