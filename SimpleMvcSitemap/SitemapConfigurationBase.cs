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
        
        public int Size { get; protected set; }

        public abstract string CreateSitemapUrl(int currentPage);
    }
}