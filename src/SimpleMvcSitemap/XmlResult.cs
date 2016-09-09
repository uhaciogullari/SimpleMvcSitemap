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
using SimpleMvcSitemap.Serialization;


namespace SimpleMvcSitemap
{
    /// <summary>
    /// Creates an XML document from the data
    /// </summary>
    /// <typeparam name="T">Serialized model type</typeparam>
    class XmlResult<T> : ActionResult
    {
        private readonly T _data;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlResult{T}"/> class.
        /// </summary>
        public XmlResult(T data)
        {
            _data = data;
        }



#if CoreMvc
        public override Task ExecuteResultAsync(ActionContext context)
        {
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