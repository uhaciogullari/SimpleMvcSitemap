using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

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
            XmlSerializerNamespaces xmlSerializerNamespaces = null;
            if (namespaceProvider != null)
            {
                xmlSerializerNamespaces = _xmlNamespaceBuilder.Create(namespaceProvider.GetNamespaces());
            }

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