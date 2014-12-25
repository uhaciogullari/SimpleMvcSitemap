using System;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace SimpleMvcSitemap
{
    class ReflectionHelper : IReflectionHelper
    {
        public UrlPropertyModel GetPropertyModel(Type type)
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
                else if (propertyInfo.PropertyType.IsClass && propertyInfo.PropertyType != typeof(string) && propertyInfo.CanRead)
                {
                    result.ClassPropeties.Add(propertyInfo);
                }
            }


            return result;
        }
    }
}