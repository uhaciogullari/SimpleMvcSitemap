using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using SimpleMvcSitemap.StyleSheets;

namespace SimpleMvcSitemap.Serialization
{
    class XmlSerializer : IXmlSerializer
    {
        private readonly IXmlNamespaceBuilder xmlNamespaceBuilder;
        private readonly XmlProcessingInstructionHandler xmlProcessingInstructionHandler;

        public XmlSerializer()
        {
            xmlNamespaceBuilder = new XmlNamespaceBuilder();
            xmlProcessingInstructionHandler = new XmlProcessingInstructionHandler();
        }

        public string Serialize<T>(T data)
        {
            StringWriter stringWriter = new StringWriter();
            SerializeToStream(data, settings => XmlWriter.Create(stringWriter, settings));
            return stringWriter.ToString();
        }

        public void SerializeToStream<T>(T data, Stream stream)
        {
            SerializeToStream(data, settings => XmlWriter.Create(stream, settings));
        }

        private void SerializeToStream<T>(T data, Func<XmlWriterSettings, XmlWriter> createXmlWriter)
        {
            IXmlNamespaceProvider namespaceProvider = data as IXmlNamespaceProvider;
            IEnumerable<string> namespaces = namespaceProvider != null ? namespaceProvider.GetNamespaces() : Enumerable.Empty<string>();
            XmlSerializerNamespaces xmlSerializerNamespaces = xmlNamespaceBuilder.Create(namespaces);

            var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T));

            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
                NamespaceHandling = NamespaceHandling.OmitDuplicates
            };

            using (XmlWriter writer = createXmlWriter(xmlWriterSettings))
            {
                if (data is IHasStyleSheets)
                {
                    xmlProcessingInstructionHandler.AddStyleSheets(writer, data as IHasStyleSheets);
                }

                xmlSerializer.Serialize(writer, data, xmlSerializerNamespaces);
            }
        }

    }
}