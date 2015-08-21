using System.IO;

namespace SimpleMvcSitemap
{
    interface IXmlSerializer
    {
        void SerializeToStream<T>(T data, Stream stream);
    }
}