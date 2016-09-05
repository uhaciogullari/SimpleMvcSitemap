using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SimpleMvcSitemap
{
    /// <summary>
    /// Creates an XML document from the data
    /// </summary>
    /// <typeparam name="T">Serialized model type</typeparam>
    public class XmlResult<T> : ActionResult
    {
        private readonly T _data;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlResult{T}"/> class.
        /// </summary>
        public XmlResult(T data)
        {
            _data = data;
        }


        public override Task ExecuteResultAsync(ActionContext context)
        {
            var response = context.HttpContext.Response;
            response.ContentType = "text/xml";
            response.WriteAsync(new XmlSerializer().Serialize(_data), Encoding.UTF8);

            return base.ExecuteResultAsync(context);
        }
    }
}