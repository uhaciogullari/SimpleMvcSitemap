using System;

namespace SimpleMvcSitemap.Routing
{
    static class TypeExtensions
    {
        //Hack for .NET 4.0 because it doesn't contain TypeInfo class.
        public static Type GetTypeInfo(this Type type)
        {
            return type;
        }
    }
}