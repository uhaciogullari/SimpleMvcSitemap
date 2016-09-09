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


namespace SimpleMvcSitemap
{
    class XmlResult<T> : ActionResult
    {
        private readonly T _data;
        private readonly IUrlValidator _urlValidator;


        internal XmlResult(T data, IUrlValidator urlValidator)
        {
            _data = data;
            _urlValidator = urlValidator;
        }



#if CoreMvc
        public override Task ExecuteResultAsync(ActionContext context)
        {
            IAbsoluteUrlConverter absoluteUrlConverter = new CoreMvcAbsoluteUrlConverter(context.HttpContext.Request);
            _urlValidator.ValidateUrls(_data, absoluteUrlConverter);

            HttpRequest httpContextRequest = context.HttpContext.Request;

            var response = context.HttpContext.Response;
            response.ContentType = "text/xml";
            response.WriteAsync(new XmlSerializer().Serialize(_data), Encoding.UTF8);

            return base.ExecuteResultAsync(context);
        }
#endif

#if Mvc
        public override void ExecuteResult(ControllerContext context)
        {
            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = "text/xml";
            response.ContentEncoding = Encoding.UTF8;

            response.BufferOutput = false;
            new XmlSerializer().SerializeToStream(_data, response.OutputStream);
        }
#endif

    }
}