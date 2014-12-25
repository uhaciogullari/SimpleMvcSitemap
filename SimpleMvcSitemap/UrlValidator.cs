using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SimpleMvcSitemap
{
    class UrlValidator : IUrlValidator
    {
        public void ValidateUrls(object item, string baseUrl)
        {
            PropertyInfo[] properties = item.GetType().GetProperties();


            foreach (PropertyInfo propertyInfo in properties)
            {
                if (propertyInfo.GetCustomAttributes(typeof(UrlAttribute), true).Any() && propertyInfo.CanRead &&
                    propertyInfo.CanWrite && propertyInfo.PropertyType == typeof(string))
                {
                    CheckForAbsolutUrl(item, propertyInfo,baseUrl);
                    continue;
                }

                if (propertyInfo.PropertyType.IsClass)
                {
                    
                }
            }

            IEnumerable<PropertyInfo> urlProperties = properties.Where(propertyInfo => propertyInfo.GetCustomAttributes(typeof(UrlAttribute), true).Any() &&
                                                                                       propertyInfo.CanRead &&
                                                                                       propertyInfo.CanWrite &&
                                                                                       propertyInfo.PropertyType == typeof(string));

            foreach (PropertyInfo urlProperty in urlProperties)
            {
                
            }
        }

        private void CheckForAbsolutUrl(object item, PropertyInfo propertyInfo, string baseUrl)
        {
            object value = propertyInfo.GetValue(item, null);
            if (value != null)
            {
                string url = value.ToString();
                if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                {
                    propertyInfo.SetValue(item, string.Concat(baseUrl, url), null);
                }
            }
        }
    }
}