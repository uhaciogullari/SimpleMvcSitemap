using System.Web.Mvc;

namespace SimpleMvcSitemap
{
    interface IActionResultFactory
    {
        ActionResult CreateXmlResult<T>(T data);
    }
}