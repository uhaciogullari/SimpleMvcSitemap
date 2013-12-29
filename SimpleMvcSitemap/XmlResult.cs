using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SimpleMvcSitemap
{
    public class XmlResult<T> : ActionResult
    {
        private readonly T _data;
        private readonly IEnumerable<XmlSerializerNamespace> _namespaces;

        public XmlResult(T data, IEnumerable<XmlSerializerNamespace> namespaces = null)
        {
            _data = data;
            _namespaces = namespaces;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = "text/xml";
            response.ContentEncoding = Encoding.UTF8;

            new XmlSerializer().Serialize(_data, context.HttpContext.Response.Output, _namespaces);
        }
    }
}