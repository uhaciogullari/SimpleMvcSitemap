namespace SimpleMvcSitemap.Routing
{
    interface IAbsoluteUrlConverter
    {
        string ConvertToAbsoluteUrl(string relativeUrl);
    }
}
