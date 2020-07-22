using System.IO;

namespace SimpleMvcSitemap.Serialization
{
    interface IXmlSerializer
    {
        string Serialize<T>(T data);
        void SerializeToStream<T>(T data, Stream stream);
    }
}