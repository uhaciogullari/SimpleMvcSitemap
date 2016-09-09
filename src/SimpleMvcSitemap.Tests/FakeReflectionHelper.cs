using System;
using System.Collections.Generic;
using SimpleMvcSitemap.Routing;

namespace SimpleMvcSitemap.Tests
{
    internal class FakeReflectionHelper : ReflectionHelper
    {
        private readonly IDictionary<Type, bool> _typeMap;

        public FakeReflectionHelper()
        {
            _typeMap = new Dictionary<Type, bool>();
        }

        public override UrlPropertyModel GetPropertyModel(Type type)
        {
            if (_typeMap.ContainsKey(type))
            {
                throw new InvalidOperationException("Property scan for the type should be executed only once");
            }

            _typeMap[type] = true;

            return base.GetPropertyModel(type);
        }
    }
}