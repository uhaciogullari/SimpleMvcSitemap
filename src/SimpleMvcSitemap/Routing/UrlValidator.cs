using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace SimpleMvcSitemap.Routing
{
    class UrlValidator : IUrlValidator
    {
        private readonly IReflectionHelper reflectionHelper;
        private readonly Dictionary<Type, UrlPropertyModel> propertyModelList;

        public UrlValidator(IReflectionHelper reflectionHelper)
        {
            this.reflectionHelper = reflectionHelper;
            propertyModelList = new Dictionary<Type, UrlPropertyModel>();
        }

        public void ValidateUrls(object item, IAbsoluteUrlConverter absoluteUrlConverter)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (absoluteUrlConverter == null)
            {
                throw new ArgumentNullException(nameof(absoluteUrlConverter));
            }


            UrlPropertyModel urlPropertyModel = GetPropertyModel(item.GetType());

            foreach (PropertyInfo urlProperty in urlPropertyModel.UrlProperties)
            {
                CheckForRelativeUrls(item, urlProperty, absoluteUrlConverter);
            }

            foreach (PropertyInfo classProperty in urlPropertyModel.ClassProperties)
            {
                object value = classProperty.GetValue(item, null);
                if (value != null)
                {
                    ValidateUrls(value, absoluteUrlConverter);
                }
            }

            foreach (PropertyInfo enumerableProperty in urlPropertyModel.EnumerableProperties)
            {
                IEnumerable value = enumerableProperty.GetValue(item, null) as IEnumerable;
                if (value != null)
                {
                    foreach (object obj in value)
                    {
                        ValidateUrls(obj, absoluteUrlConverter);
                    }
                }
            }

        }


        private UrlPropertyModel GetPropertyModel(Type type)
        {
            UrlPropertyModel result;
            if (!propertyModelList.TryGetValue(type, out result))
            {
                result = reflectionHelper.GetPropertyModel(type);
                propertyModelList[type] = result;
            }

            return result;
        }

        private void CheckForRelativeUrls(object item, PropertyInfo propertyInfo, IAbsoluteUrlConverter absoluteUrlConverter)
        {
            object value = propertyInfo.GetValue(item, null);
            if (value != null)
            {
                string url = value.ToString();
                if (!Uri.IsWellFormedUriString(url, UriKind.Absolute) && Uri.IsWellFormedUriString(url, UriKind.Relative))
                {
                    propertyInfo.SetValue(item, absoluteUrlConverter.ConvertToAbsoluteUrl(url), null);
                }
            }
        }
    }
}