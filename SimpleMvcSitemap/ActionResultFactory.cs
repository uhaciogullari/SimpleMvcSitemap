using System.Web.Mvc;

namespace SimpleMvcSitemap
{
    class ActionResultFactory : IActionResultFactory
    {
        public ActionResult CreateXmlResult<T>(T data)
        {
            return new XmlResult<T>(data);
        }
    }
}