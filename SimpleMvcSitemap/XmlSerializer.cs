using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    class XmlSerializer : IXmlSerializer
    {
        public void Serialize<T>(T data, TextWriter textWriter, IEnumerable<XmlSerializerNamespace> namespaces)
        {
            var ns = new XmlSerializerNamespaces();
            ns.Add("", SitemapNamespaceConstants.SITEMAP);

            List<XmlSerializerNamespace> xmlSerializerNamespaces = namespaces != null
                ? namespaces.ToList()
                : Enumerable.Empty<XmlSerializerNamespace>().ToList();
            if (xmlSerializerNamespaces.Any())
                xmlSerializerNamespaces.ForEach(item => ns.Add(item.Prefix, item.Namespace));

            var ser = new System.Xml.Serialization.XmlSerializer(typeof(T));

            ser.Serialize(textWriter, data, ns);
        }
    }
}