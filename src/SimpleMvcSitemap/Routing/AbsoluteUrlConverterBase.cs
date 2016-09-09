namespace SimpleMvcSitemap.Routing
{
    public abstract class AbsoluteUrlConverterBase
    {
        protected string CreateAbsoluteUrl(string baseUrl, string relativeUrl)
        {
            if (!string.IsNullOrWhiteSpace(relativeUrl))
            {
                if (!relativeUrl.StartsWith("/"))
                {
                    relativeUrl = $"/{relativeUrl}";
                }

                return $"{baseUrl}{relativeUrl}";
            }

            return baseUrl;
        }
    }
}