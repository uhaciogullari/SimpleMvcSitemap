using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using SimpleMvcSitemap.Routing;
using SimpleMvcSitemap.Serialization;


namespace SimpleMvcSitemap
{
    class XmlResult<T> : ActionResult
    {
        private readonly IBaseUrlProvider baseUrlProvider;
        private readonly T data;
        private readonly IUrlValidator urlValidator;


        internal XmlResult(T data, IUrlValidator urlValidator)
        {
            this.data = data;
            this.urlValidator = urlValidator;
        }

        internal XmlResult(T data, IBaseUrlProvider baseUrlProvider) : this(data, new UrlValidator(new ReflectionHelper()))
        {
            this.baseUrlProvider = baseUrlProvider;
        }

        public override async Task ExecuteResultAsync(ActionContext context)
        {
            urlValidator.ValidateUrls(data, baseUrlProvider ?? new BaseUrlProvider(context.HttpContext.Request));

            HttpRequest httpContextRequest = context.HttpContext.Request;

            var response = context.HttpContext.Response;
            response.ContentType = "text/xml";
            await response.WriteAsync(new XmlSerializer().Serialize(data), Encoding.UTF8);

            await base.ExecuteResultAsync(context);
        }

    }
}