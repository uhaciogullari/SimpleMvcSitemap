using System.Collections.Generic;
using System.Web.Mvc;

namespace SimpleMvcSitemap
{
    interface IActionResultFactory
    {
        ActionResult CreateXmlResult<T>(T data, IEnumerable<XmlSerializerNamespace> serializerNamespaces = null);
    }
}