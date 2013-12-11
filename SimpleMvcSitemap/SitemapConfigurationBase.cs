namespace SimpleMvcSitemap
{
    public abstract class SitemapConfigurationBase : ISitemapConfiguration
    {
        protected SitemapConfigurationBase(int? currentPage)
        {
            CurrentPage = currentPage;
            Size = 50000;
        }
        
        public int? CurrentPage { get; private set; }
        
        public int Size { get; private set; }

        public abstract string CreateIndexUrl(int currentPage);
    }
}