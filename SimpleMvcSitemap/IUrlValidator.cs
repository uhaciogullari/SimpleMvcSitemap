namespace SimpleMvcSitemap
{
    public interface IUrlValidator
    {
        void ValidateUrls(object item, string baseUrl);
    }
}