using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SimpleMvcSitemap
{
    public class XmlResult<T> : ActionResult
    {
        private readonly T _data;

        public XmlResult(T data)
        {
            _data = data;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = "text/xml";
            response.ContentEncoding = Encoding.UTF8;

            string xml = new XmlSerializer().Serialize(_data);
            context.HttpContext.Response.Write(xml);
        }
    }
}