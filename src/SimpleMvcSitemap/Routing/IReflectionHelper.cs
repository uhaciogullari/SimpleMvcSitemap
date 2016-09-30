using System;

namespace SimpleMvcSitemap.Routing
{
    internal interface IReflectionHelper
    {
        UrlPropertyModel GetPropertyModel(Type type);
    }
}