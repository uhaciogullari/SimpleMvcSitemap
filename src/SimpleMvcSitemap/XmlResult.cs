using System.Text;
using System.Web;
using System.Web.Mvc;

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

        /// <summary>
        /// Outputs the XML document to response.
        /// </summary>
        /// <param name="context">Controller context.</param>
        public override void ExecuteResult(ControllerContext context)
        {
            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = "text/xml";
            response.ContentEncoding = Encoding.UTF8;

            response.BufferOutput = false;
            new XmlSerializer().SerializeToStream(_data, response.OutputStream);
        }
    }
}