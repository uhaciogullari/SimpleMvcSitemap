namespace SimpleMvcSitemap
{
    public interface ISitemapConfiguration<T>
    {
        int? CurrentPage { get; } 

        int Size { get; }

        string CreateSitemapUrl(int currentPage);

        SitemapNode CreateNode(T source);
    }
}