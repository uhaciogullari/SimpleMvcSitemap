#if Mvc
using System.Web;
using System.Web.Mvc;
#endif

#if CoreMvc
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
#endif

using System.Text;
using SimpleMvcSitemap.Routing;
using SimpleMvcSitemap.Serialization;
using SimpleMvcSitemap.StyleSheets;


namespace SimpleMvcSitemap
{
    class XmlResult<T> : ActionResult where T : IHasStyleSheets
    {
        private readonly T data;
        private readonly IUrlValidator urlValidator;


        internal XmlResult(T data, IUrlValidator urlValidator)
        {
            this.data = data;
            this.urlValidator = urlValidator;
        }

        internal XmlResult(T data) : this(data, new UrlValidator(new ReflectionHelper()))
        {

        }


#if CoreMvc
        public override Task ExecuteResultAsync(ActionContext context)
        {
            IAbsoluteUrlConverter absoluteUrlConverter = new CoreMvcAbsoluteUrlConverter(context.HttpContext.Request);
            urlValidator.ValidateUrls(data, absoluteUrlConverter);

            HttpRequest httpContextRequest = context.HttpContext.Request;

            var response = context.HttpContext.Response;
            response.ContentType = "text/xml";
            response.WriteAsync(new XmlSerializer().Serialize(data), Encoding.UTF8);

            return base.ExecuteResultAsync(context);
        }
#endif

#if Mvc
        public override void ExecuteResult(ControllerContext context)
        {
            IAbsoluteUrlConverter absoluteUrlConverter = new MvcAbsoluteUrlConverter(context.HttpContext);
            urlValidator.ValidateUrls(data, absoluteUrlConverter);

            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = "text/xml";
            response.ContentEncoding = Encoding.UTF8;

            response.BufferOutput = false;
            new XmlSerializer().SerializeToStream(data, response.OutputStream);
        }
#endif

    }
}