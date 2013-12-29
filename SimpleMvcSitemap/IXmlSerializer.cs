using System.Collections.Generic;
using System.IO;

namespace SimpleMvcSitemap
{
    interface IXmlSerializer
    {
        void Serialize<T>(T data, TextWriter textWriter, IEnumerable<XmlSerializerNamespace> namespaces);
    }
}