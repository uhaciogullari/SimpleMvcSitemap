namespace SimpleMvcSitemap
{
    public interface ISitemapConfiguration
    {
        int? CurrentPage { get; } 

        int Size { get; }

        string CreateSitemapUrl(int currentPage);
    }
}