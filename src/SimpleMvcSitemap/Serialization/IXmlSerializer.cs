namespace SimpleMvcSitemap.Serialization
{
    interface IXmlSerializer
    {
        string Serialize<T>(T data);
    }
}