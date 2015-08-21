using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    class XmlSerializer : IXmlSerializer
    {
        private readonly IXmlNamespaceBuilder _xmlNamespaceBuilder;

        private XmlSerializer(IXmlNamespaceBuilder xmlNamespaceBuilder)
        {
            _xmlNamespaceBuilder = xmlNamespaceBuilder;
        }

        public XmlSerializer() : this(new XmlNamespaceBuilder()) { }

        public void SerializeToStream<T>(T data, Stream stream)
        {
            IXmlNamespaceProvider namespaceProvider = data as IXmlNamespaceProvider;
            IEnumerable<string> namespaces = namespaceProvider != null ? namespaceProvider.GetNamespaces() : Enumerable.Empty<string>();
            XmlSerializerNamespaces xmlSerializerNamespaces = _xmlNamespaceBuilder.Create(namespaces);
            var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T));


            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
                NamespaceHandling = NamespaceHandling.OmitDuplicates
            };

            using (XmlWriter writer = XmlWriter.Create(stream, xmlWriterSettings))
            {
                xmlSerializer.Serialize(writer, data, xmlSerializerNamespaces);
                writer.Flush();
            }
        }

    }
}