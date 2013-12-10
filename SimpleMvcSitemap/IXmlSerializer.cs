namespace SimpleMvcSitemap
{
    interface IXmlSerializer
    {
        string Serialize<T>(T data);
    }
}