using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web;

namespace SimpleMvcSitemap
{
    class UrlValidator : IUrlValidator
    {
        private readonly IReflectionHelper _reflectionHelper;
        private readonly IBaseUrlProvider _baseUrlProvider;
        private readonly Dictionary<Type, UrlPropertyModel> _propertyModelList;

        public UrlValidator(IReflectionHelper reflectionHelper, IBaseUrlProvider baseUrlProvider)
        {
            _reflectionHelper = reflectionHelper;
            _baseUrlProvider = baseUrlProvider;
            _propertyModelList = new Dictionary<Type, UrlPropertyModel>();
        }

        public void ValidateUrls(HttpContextBase httpContext, object item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            UrlPropertyModel urlPropertyModel = GetPropertyModel(item.GetType());
            Lazy<string> baseUrlProvider = new Lazy<string>(() => _baseUrlProvider.GetBaseUrl(httpContext));

            foreach (PropertyInfo urlProperty in urlPropertyModel.UrlProperties)
            {
                CheckForAbsoluteUrl(item, urlProperty, baseUrlProvider);
            }

            foreach (PropertyInfo classProperty in urlPropertyModel.ClassPropeties)
            {
                object value = classProperty.GetValue(item, null);
                if (value != null)
                {
                    ValidateUrls(httpContext, value);
                }
            }

            foreach (PropertyInfo enumerableProperty in urlPropertyModel.EnumerableProperties)
            {
                IEnumerable value = enumerableProperty.GetValue(item, null) as IEnumerable;
                if (value != null)
                {
                    foreach (object obj in value)
                    {
                        ValidateUrls(httpContext, obj);
                    }
                }
            }
        }

        private void CheckForAbsoluteUrl(object item, PropertyInfo propertyInfo, Lazy<string> baseUrlProvider)
        {
            object value = propertyInfo.GetValue(item, null);
            if (value != null)
            {
                string url = value.ToString();
                if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                {
                    propertyInfo.SetValue(item, string.Concat(baseUrlProvider.Value, url), null);
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