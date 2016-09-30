using System;
using System.Collections.Generic;
using SimpleMvcSitemap.Routing;

namespace SimpleMvcSitemap.Tests
{
    internal class FakeReflectionHelper : ReflectionHelper
    {
        private readonly IDictionary<Type, bool> typeMap;

        public FakeReflectionHelper()
        {
            typeMap = new Dictionary<Type, bool>();
        }

        public override UrlPropertyModel GetPropertyModel(Type type)
        {
            if (typeMap.ContainsKey(type))
            {
                throw new InvalidOperationException("Property scan for the type should be executed only once");
            }

            typeMap[type] = true;

            return base.GetPropertyModel(type);
        }
    }
}