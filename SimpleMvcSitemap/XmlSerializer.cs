using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    class XmlSerializer : IXmlSerializer
    {
        public string Serialize<T>(T data, IEnumerable<XmlSerializerNamespace> namespaces)
        {
            var serializerNamespaces = new XmlSerializerNamespaces();
            serializerNamespaces.Add("", SitemapNamespaceConstants.SITEMAP);

            List<XmlSerializerNamespace> xmlSerializerNamespaces = namespaces != null
                ? namespaces.ToList()
                : Enumerable.Empty<XmlSerializerNamespace>().ToList();
            if (xmlSerializerNamespaces.Any())
                xmlSerializerNamespaces.ForEach(item => serializerNamespaces.Add(item.Prefix, item.Namespace));

            var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T));

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (XmlWriter writer = XmlWriter.Create(memoryStream, new XmlWriterSettings
                                                                        {
                                                                            Encoding = Encoding.UTF8,
                                                                            NamespaceHandling = NamespaceHandling.OmitDuplicates
                                                                        }))
                {
                    xmlSerializer.Serialize(writer, data, serializerNamespaces);
                    writer.Flush();
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    return new StreamReader(memoryStream).ReadToEnd();
                }
            }
        }
    }
}