using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace SimpleMvcSitemap
{
    class XmlSerializer : IXmlSerializer
    {
        private readonly IXmlNamespaceBuilder _xmlNamespaceBuilder;

        internal XmlSerializer(IXmlNamespaceBuilder xmlNamespaceBuilder)
        {
            _xmlNamespaceBuilder = xmlNamespaceBuilder;
        }

        public XmlSerializer() : this(new XmlNamespaceBuilder()) { }

        public string Serialize<T>(T data)
        {
            IXmlNamespaceProvider namespaceProvider = data as IXmlNamespaceProvider;
            IEnumerable<string> namespaces = namespaceProvider != null ? namespaceProvider.GetNamespaces() : Enumerable.Empty<string>();
            var xmlSerializerNamespaces = _xmlNamespaceBuilder.Create(namespaces);


            var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T));

            using (MemoryStream memoryStream = new MemoryStream())
            {
                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings
                {
                    Encoding = Encoding.UTF8,
                    NamespaceHandling = NamespaceHandling.OmitDuplicates
                };

                using (XmlWriter writer = XmlWriter.Create(memoryStream, xmlWriterSettings))
                {
                    xmlSerializer.Serialize(writer, data, xmlSerializerNamespaces);
                    writer.Flush();
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    return new StreamReader(memoryStream).ReadToEnd();
                }
            }
        }
    }
}