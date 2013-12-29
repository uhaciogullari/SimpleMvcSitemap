using System.Collections.Generic;
using System.Web.Mvc;

namespace SimpleMvcSitemap
{
    class ActionResultFactory : IActionResultFactory
    {
        public ActionResult CreateXmlResult<T>(T data, IEnumerable<XmlSerializerNamespace> serializerNamespaces = null)
        {
            return new XmlResult<T>(data);
        }
    }
}