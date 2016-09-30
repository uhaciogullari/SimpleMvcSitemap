using System.Collections.Generic;
using System.Linq;
using SimpleMvcSitemap.StyleSheets;

namespace SimpleMvcSitemap
{
    /// <inheritDoc/>
    public abstract class SitemapIndexConfiguration<T> : ISitemapIndexConfiguration<T>
    {
        /// <summary>
        /// Base constructor for the abstract SitemapIndexConfiguration class.
        /// </summary>
        /// <param name="dataSource">Data source that will be queried for paging.</param>
        /// <param name="currentPage">Current page for paged sitemap items.</param>
        protected SitemapIndexConfiguration(IQueryable<T> dataSource, int? currentPage)
        {
            DataSource = dataSource;
            CurrentPage = currentPage;
            Size = 50000;
        }

        /// <inheritDoc/>
        public IQueryable<T> DataSource { get; }

        /// <inheritDoc/>
        public int? CurrentPage { get; }

        /// <inheritDoc/>
        public int Size { get; protected set; }

        /// <inheritDoc/>
        public abstract SitemapIndexNode CreateSitemapIndexNode(int currentPage);

        /// <inheritDoc/>
        public abstract SitemapNode CreateNode(T source);

        /// <inheritDoc/>
        public List<XmlStyleSheet> SitemapStyleSheets { get; protected set; }

        /// <inheritDoc/>
        public List<XmlStyleSheet> SitemapIndexStyleSheets { get; protected set; }

        /// <inheritDoc/>
        public bool UseReverseOrderingForSitemapIndexNodes { get; protected set; }
    }
}