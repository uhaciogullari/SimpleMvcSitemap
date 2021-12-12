using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SimpleMvcSitemap.Middleware
{
    internal class SitemapMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IActionDescriptorCollectionProvider _collectionProvider;
        private readonly SitemapGeneratorOptions _options;
        private readonly LinkGenerator _linkGenerator;
        private readonly SitemapProvider _sitemapProvider;
        private readonly List<Type> _excludedVerbs = new List<Type>()
        {
          typeof (HttpPostAttribute),
          typeof (HttpDeleteAttribute),
          typeof (HttpPutAttribute),
          typeof (HttpHeadAttribute),
          typeof (HttpOptionsAttribute),
          typeof (HttpPatchAttribute)
        };

        public SitemapMiddleware(RequestDelegate next, IActionDescriptorCollectionProvider collectionProvider, LinkGenerator linkGenerator)
        {
            _next = next;
            _collectionProvider = collectionProvider ?? throw new ArgumentNullException(nameof(collectionProvider));
            _options = new SitemapGeneratorOptions(); //Take all the defaults
            _linkGenerator = linkGenerator ?? throw new ArgumentNullException(nameof(linkGenerator));
            _sitemapProvider = new SitemapProvider();
        }

        public SitemapMiddleware(RequestDelegate next, IActionDescriptorCollectionProvider collectionProvider, LinkGenerator linkGenerator, SitemapGeneratorOptions options)
          : this(next, collectionProvider, linkGenerator)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            if (_options.BaseUrl != null)
                _sitemapProvider = new SitemapProvider(new SitemapGeneratorBaseUrl() { BaseUrl = new Uri(_options.BaseUrl) });
            else
                _sitemapProvider = new SitemapProvider();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (SitemapMiddleware.IsSiteMapRequested(context))
                await WriteSitemapAsync(context);
            else if (_options != null && _options.EnableRobotsTxtGeneration && SitemapMiddleware.IsRobotsRequested(context))
                await WriteRobotsAsync(context);
            else
                await _next.Invoke(context);
        }

        private Task WriteSitemapAsync(HttpContext context)
        {
            if (_options.DefaultSiteMapType == SiteMapType.Sitemap)
            {
                var model = GetSitemapModel(context, _collectionProvider.ActionDescriptors.Items, GetSiteBaseUrl(context.Request));
                var sitemapData = new Serialization.XmlSerializer().Serialize(model);
                return SitemapMiddleware.WriteStringContentAsync(context, sitemapData, "application/xml");
            }
            else //SitemapType.SitemapIndex
            {
                var model = GetSitemapIndexModel(context, _collectionProvider.ActionDescriptors.Items, GetSiteBaseUrl(context.Request));
                var sitemapData = new Serialization.XmlSerializer().Serialize(model);
                return SitemapMiddleware.WriteStringContentAsync(context, sitemapData, "application/xml");
            }
        }

        private SitemapModel GetSitemapModel(HttpContext context, IEnumerable<ActionDescriptor> routes, string siteBase)
        {
            List<SitemapNode> validUrls = new List<SitemapNode>();
            foreach (ActionDescriptor route in routes)
            {
                if (route is PageActionDescriptor && route?.AttributeRouteInfo != null && IsIncludedRoute(route)) //Razor page routing
                {
                    var pageRoute = (PageActionDescriptor)route;
                    string url = siteBase + "/" + route.AttributeRouteInfo.Template;
                    var sitemapNode = GetSiteMapNode(url);
                    if (url != null && !validUrls.Contains(sitemapNode))
                        validUrls.Add(sitemapNode);
                }
                else if (route is ControllerActionDescriptor && route != null && IsIncludedRoute(route)) //MVC/Controller page routing, supports routing that use attributes and without attributes, https://joonasw.net/view/discovering-actions-and-razor-pages
                {
                    var controllerRoute = (ControllerActionDescriptor)route;
                    var url = siteBase + _linkGenerator.GetPathByAction(controllerRoute.ActionName, controllerRoute.ControllerName, controllerRoute.RouteValues); //Link generator supports attribute and standard routing configuration
                    var sitemapNode = GetSiteMapNode(url);
                    if (url != null && !validUrls.Contains(sitemapNode))
                        validUrls.Add(sitemapNode);
                }
            }
            return new SitemapModel(validUrls);
        }
        private SitemapNode GetSiteMapNode(string url)
        {
            var node = new SitemapNode(url);
            if (_options.DefaultPriority != null)
                node.Priority = _options.DefaultPriority;
            if (_options.DefaultChangeFrequency != null)
                node.ChangeFrequency = _options.DefaultChangeFrequency;
            if (_options.LastModifiedDate != null)
                node.LastModificationDate = _options.LastModifiedDate;
            return node;
        }

        private SitemapIndexModel GetSitemapIndexModel(HttpContext context, IEnumerable<ActionDescriptor> routes, string siteBase)
        {
            List<SitemapIndexNode> validUrls = new List<SitemapIndexNode>();
            foreach (ActionDescriptor route in routes)
            {
                if (route is PageActionDescriptor && route?.AttributeRouteInfo != null && IsIncludedRoute(route)) //Razor page routing
                {
                    var pageRoute = (PageActionDescriptor)route;
                    string url = siteBase + "/" + route.AttributeRouteInfo.Template;
                    var sitemapNode = new SitemapIndexNode(url);
                    if (url != null && !validUrls.Contains(sitemapNode))
                        validUrls.Add(sitemapNode);
                }
                else if (route is ControllerActionDescriptor && route != null && IsIncludedRoute(route)) //MVC/Controller page routing, supports routing that use attributes and without attributes, https://joonasw.net/view/discovering-actions-and-razor-pages
                {
                    var controllerRoute = (ControllerActionDescriptor)route;
                    var url = siteBase + _linkGenerator.GetPathByAction(controllerRoute.ActionName, controllerRoute.ControllerName, controllerRoute.RouteValues); //Link generator supports attribute and standard routing configuration
                    var sitemapNode = new SitemapIndexNode(url);
                    if (url != null && !validUrls.Contains(sitemapNode))
                        validUrls.Add(sitemapNode);
                }
            }
            return new SitemapIndexModel(validUrls);
        }

        //private IEnumerable<string> GetValidUrls(HttpContext context, IEnumerable<ActionDescriptor> routes, string siteBase)
        //{
        //    List<string> validUrls = new List<string>();
        //    foreach (ActionDescriptor route in routes)
        //    {
        //        if (route is PageActionDescriptor && route?.AttributeRouteInfo != null && IsIncludedRoute(route)) //Razor page routing
        //        {
        //            var pageRoute = (PageActionDescriptor)route;
        //            string url = siteBase + "/" + route.AttributeRouteInfo.Template;
        //            if (url != null && !validUrls.Contains(url))
        //                validUrls.Add(url);
        //        }
        //        else if (route is ControllerActionDescriptor && route != null && IsIncludedRoute(route)) //MVC/Controller page routing, supports routing use attributes and without attributes, https://joonasw.net/view/discovering-actions-and-razor-pages
        //        {
        //            var controllerRoute = (ControllerActionDescriptor)route;
        //            var url = siteBase + _linkGenerator.GetPathByAction(controllerRoute.ActionName, controllerRoute.ControllerName, controllerRoute.RouteValues); //Link generator supports attribute and standard routing configuration
        //            if (url != null && !validUrls.Contains(url))
        //                validUrls.Add(url);
        //        }
        //    }
        //    return validUrls;
        //}

        private bool IsIncludedRoute(ActionDescriptor route)
        {
            if (route is ControllerActionDescriptor actionDescriptor)
            {
                MethodInfo methodInfo = actionDescriptor.MethodInfo;
                if (methodInfo != null && (SitemapMiddleware.HasExclusionAttribute(((MemberInfo)methodInfo).CustomAttributes) || IsExcludedVerb(((MemberInfo)methodInfo).CustomAttributes)))
                    return false;
                TypeInfo controllerTypeInfo = actionDescriptor.ControllerTypeInfo;
                if ((Type)controllerTypeInfo != null && SitemapMiddleware.HasExclusionAttribute(((MemberInfo)controllerTypeInfo).CustomAttributes))
                    return false;
            }
            return true;
        }

        private static string BuildSitemapXml(IEnumerable<string> urls)
        {
            StringBuilder stringBuilder = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">\r\n");
            foreach (string url in urls)
                stringBuilder.Append("<url>\r\n<loc>" + url + "</loc>\r\n</url>\r\n");
            stringBuilder.Append("</urlset>");
            return stringBuilder.ToString();
        }

        private static bool HasExclusionAttribute(IEnumerable<CustomAttributeData> attributes) => attributes != null && attributes.Any<CustomAttributeData>((Func<CustomAttributeData, bool>)(x => x.AttributeType == typeof(SitemapExcludeAttribute)));

        private bool IsExcludedVerb(IEnumerable<CustomAttributeData> attributes) => attributes != null && _excludedVerbs.Any<Type>((Func<Type, bool>)(excludedVerb => attributes.Any<CustomAttributeData>((Func<CustomAttributeData, bool>)(x => x.AttributeType == excludedVerb))));

        private Task WriteRobotsAsync(HttpContext context)
        {
            string content = "User-agent: *\r\nAllow: /\r\nSitemap: " + GetSitemapUrl(context.Request);
            return SitemapMiddleware.WriteStringContentAsync(context, content, "text/plain");
        }

        private string GetSitemapUrl(HttpRequest contextRequest) => GetSiteBaseUrl(contextRequest) + "/sitemap.xml";

        private static bool IsSiteMapRequested(HttpContext context)
        {
            PathString path = context.Request.Path;
            if (path == null)
                return false;
            if (path.Value != null)
                return path.Value.Equals("/sitemap.xml", StringComparison.OrdinalIgnoreCase);
            return false;
        }

        private static bool IsRobotsRequested(HttpContext context)
        {
            PathString path = context.Request.Path;
            if (path == null)
                return false;
            if (path.Value != null)
                return path.Value.Equals("/robots.txt", StringComparison.OrdinalIgnoreCase);
            return false;
        }

        private static async Task WriteStringContentAsync(HttpContext context, string content, string contentType)
        {
            Stream body = context.Response.Body;
            context.Response.StatusCode = 200;
            context.Response.ContentType = contentType;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(content);
                memoryStream.Write(bytes, 0, bytes.Length);
                memoryStream.Seek(0, SeekOrigin.Begin);
                await memoryStream.CopyToAsync(body, bytes.Length);
            }
        }

        private string GetSiteBaseUrl(HttpRequest request)
        {
            string str = request.Scheme;
            if (_options.BaseUrl != null)
                return _options.BaseUrl.ToString();
            return string.Format("{0}://{1}{2}", str, request.Host, request.PathBase);
        }
    }
}
