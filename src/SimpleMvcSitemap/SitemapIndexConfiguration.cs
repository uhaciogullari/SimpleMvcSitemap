using System.Collections.Generic;
using System.Linq;
using SimpleMvcSitemap.StyleSheets;

namespace SimpleMvcSitemap
{
    public abstract class SitemapIndexConfiguration<T> : ISitemapIndexConfiguration<T>
    {
        public SitemapIndexConfiguration(IQueryable<T> dataSource, int? currentPage)
        {
            DataSource = dataSource;
            CurrentPage = currentPage;
            Size = 50000;
        }

        public IQueryable<T> DataSource { get; }

        public int? CurrentPage { get; }

        public int Size { get; protected set; }

        public abstract SitemapIndexNode CreateSitemapIndexNode(int currentPage);

        public abstract SitemapNode CreateNode(T source);

        public List<XmlStyleSheet> SitemapStyleSheets { get; protected set; }

        public List<XmlStyleSheet> SitemapIndexStyleSheets { get; protected set; }

        public bool UseReverseOrderingForSitemapIndexNodes { get; protected set; }
    }
}