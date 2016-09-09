namespace SimpleMvcSitemap.Routing
{
    /// <summary>
    /// Checks an object for URL properties marked with UrlAttribute and
    /// converts relative URLs to absolute ones.
    /// </summary>
    interface IUrlValidator
    {
        /// <summary>
        /// Validates the urls.
        /// </summary>
        /// <param name="item">An object containing URLs.</param>
        /// <param name="absoluteUrlConverter"></param>
        void ValidateUrls(object item, IAbsoluteUrlConverter absoluteUrlConverter);
    }
}