using System.Collections.Generic;
using System.IO;

namespace SimpleMvcSitemap
{
    interface IXmlSerializer
    {
        string Serialize<T>(T data, IEnumerable<XmlSerializerNamespace> namespaces);
    }
}