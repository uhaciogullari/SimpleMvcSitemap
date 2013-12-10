using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace SimpleMvcSitemap
{
    class XmlSerializer : IXmlSerializer
    {
        public string Serialize<T>(T data)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (XmlWriter writer = XmlWriter.Create(memoryStream))
                {
                    new DataContractSerializer(data.GetType()).WriteObject(writer, data);
                    writer.Flush();
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    return new StreamReader(memoryStream).ReadToEnd();
                }
            }
        }
    }
}