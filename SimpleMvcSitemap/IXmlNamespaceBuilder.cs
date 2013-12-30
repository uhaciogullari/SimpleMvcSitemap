using System.Collections.Generic;
using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    interface IXmlNamespaceBuilder
    {
        XmlSerializerNamespaces Create(IEnumerable<string> namespaces);
    }
}