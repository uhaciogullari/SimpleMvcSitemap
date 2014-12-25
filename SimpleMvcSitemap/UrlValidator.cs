using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace SimpleMvcSitemap
{
    class UrlValidator : IUrlValidator
    {
        private readonly IReflectionHelper _reflectionHelper;
        private readonly Dictionary<Type, UrlPropertyModel> _propertyModelList;

        public UrlValidator(IReflectionHelper reflectionHelper)
        {
            _reflectionHelper = reflectionHelper;
            _propertyModelList = new Dictionary<Type, UrlPropertyModel>();
        }

        public void ValidateUrls(object item, string baseUrl)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            UrlPropertyModel urlPropertyModel = GetPropertyModel(item.GetType());

            foreach (PropertyInfo urlProperty in urlPropertyModel.UrlProperties)
            {
                CheckForAbsoluteUrl(item, urlProperty, baseUrl);
            }

            foreach (PropertyInfo classProperty in urlPropertyModel.ClassPropeties)
            {
                object value = classProperty.GetValue(item, null);
                if (value != null)
                {
                    ValidateUrls(value, baseUrl);
                }
            }

            foreach (PropertyInfo enumerableProperty in urlPropertyModel.EnumerableProperties)
            {
                IEnumerable value = enumerableProperty.GetValue(item, null) as IEnumerable;
                if (value != null)
                {
                    foreach (object obj in value)
                    {
                        ValidateUrls(obj, baseUrl);
                    }
                }
            }
        }

        private void CheckForAbsoluteUrl(object item, PropertyInfo propertyInfo, string baseUrl)
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

        private UrlPropertyModel GetPropertyModel(Type type)
        {
            UrlPropertyModel result;
            if (!_propertyModelList.TryGetValue(type, out result))
            {
                result = _reflectionHelper.GetPropertyModel(type);
                _propertyModelList[type] = result;
            }

            return result;
        }
    }
}