using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace SimpleMvcSitemap
{
    class UrlValidator : IUrlValidator
    {
        private readonly IReflectionHelper _reflectionHelper;
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly Dictionary<Type, UrlPropertyModel> _propertyModelList;

        public UrlValidator(IReflectionHelper reflectionHelper, IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionContextAccessor)
        {
            _reflectionHelper = reflectionHelper;
            _urlHelperFactory = urlHelperFactory;
            _actionContextAccessor = actionContextAccessor;
            _propertyModelList = new Dictionary<Type, UrlPropertyModel>();
        }

        public void ValidateUrls(object item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var urlHelper = _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext);
            ValidateUrls(urlHelper, item);
        }

        private void ValidateUrls(IUrlHelper urlHelper, object item)
        {
            UrlPropertyModel urlPropertyModel = GetPropertyModel(item.GetType());

            foreach (PropertyInfo urlProperty in urlPropertyModel.UrlProperties)
            {
                CheckForAbsoluteUrl(item, urlProperty, urlHelper);
            }

            foreach (PropertyInfo classProperty in urlPropertyModel.ClassProperties)
            {
                object value = classProperty.GetValue(item, null);
                if (value != null)
                {
                    ValidateUrls(urlHelper, value);
                }
            }

            foreach (PropertyInfo enumerableProperty in urlPropertyModel.EnumerableProperties)
            {
                IEnumerable value = enumerableProperty.GetValue(item, null) as IEnumerable;
                if (value != null)
                {
                    foreach (object obj in value)
                    {
                        ValidateUrls(urlHelper, obj);
                    }
                }
            }
        }

        private void CheckForAbsoluteUrl(object item, PropertyInfo propertyInfo, IUrlHelper urlHelper)
        {
            object value = propertyInfo.GetValue(item, null);
            if (value != null)
            {
                string url = value.ToString();
                if (urlHelper.IsLocalUrl(url))
                {
                    propertyInfo.SetValue(item, urlHelper.Content(url));
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