using System;

namespace SimpleMvcSitemap
{
    internal interface IReflectionHelper
    {
        UrlPropertyModel GetPropertyModel(Type type);
    }
}