namespace SimpleMvcSitemap
{
    /// <summary>
    /// Configuration for required for creating sitemap index files
    /// </summary>
    /// <typeparam name="T">Source item type</typeparam>
    public interface ISitemapConfiguration<T>
    {
        /// <summary>
        /// Current page for paged sitemap items.
        /// </summary>
        int? CurrentPage { get; }

        /// <summary>
        /// Number of items in each sitemap file.
        /// </summary>
        int Size { get; }
		
		/// <summary>
		/// It sets if index page will be revert generated or not.
		/// </summary>
		bool RevertIndex { get; }

		/// <summary>
		/// Creates the sitemap URL for a page.
		/// </summary>
		/// <param name="currentPage">The specified page URL.</param>
		/// <returns></returns>
		string CreateSitemapUrl(int currentPage);

        /// <summary>
        /// Creates the sitemap node.
        /// </summary>
        /// <param name="source">The source item.</param>
        SitemapNode CreateNode(T source);
    }
}