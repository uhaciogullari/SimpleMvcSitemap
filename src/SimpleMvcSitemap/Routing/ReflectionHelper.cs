using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using SimpleMvcSitemap.Routing;

namespace SimpleMvcSitemap.Routing
{
    class ReflectionHelper : IReflectionHelper
    {
        public virtual UrlPropertyModel GetPropertyModel(Type type)
        {
            UrlPropertyModel result = new UrlPropertyModel();
            PropertyInfo[] propertyInfos = type.GetProperties();

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if (propertyInfo.GetCustomAttributes(typeof (UrlAttribute), true).Any() &&
                    propertyInfo.PropertyType == typeof(string) &&
                    propertyInfo.CanRead &&
                    propertyInfo.CanWrite)
                {
                    result.UrlProperties.Add(propertyInfo);
                }
                else if (typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType) && propertyInfo.CanRead)
                {
                    result.EnumerableProperties.Add(propertyInfo);

                }
                else if (propertyInfo.PropertyType.GetTypeInfo().IsClass && propertyInfo.PropertyType != typeof(string) && propertyInfo.CanRead)
                {
                    result.ClassProperties.Add(propertyInfo);
                }
            }


            return result;
        }
    }
}