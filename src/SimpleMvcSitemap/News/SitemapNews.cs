using System;
using System.Xml.Serialization;

namespace SimpleMvcSitemap.News
{
    /// <summary>
    /// Encloses all information about the news article.
    /// </summary>
    public class SitemapNews
    {
        internal SitemapNews() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newsPublication">Specifies the publication in which the article appears</param>
        /// <param name="publicationDate">Article publication date</param>
        /// <param name="title">The title of the news article. </param>
        public SitemapNews(NewsPublication newsPublication, DateTime publicationDate, string title)
        {
            Publication = newsPublication;
            PublicationDate = publicationDate;
            Title = title;
        }

        /// <summary>
        /// Specifies the publication in which the article appears. 
        /// </summary>
        [XmlElement("publication", Order = 1)]
        public NewsPublication Publication { get; set; }


        /// <summary>
        /// Possible values include "Subscription" or "Registration", describing the accessibility of the article.
        /// If the article is accessible to Google News readers without a registration or subscription, this tag should be omitted.
        /// </summary>
        [XmlElement("access", Order = 2)]
        public NewsAccess? Access { get; set; }


        /// <summary>
        /// A comma-separated list of properties characterizing the content of the article.
        /// Values:
        /// PressRelease (visible): an official press release.
        /// Satire (visible): an article which ridicules its subject for didactic purposes.
        /// Blog (visible): any article published on a blog, or in a blog format.
        /// OpEd: an opinion-based article which comes specifically from the Op-Ed section of your site.
        /// Opinion: any other opinion-based article not appearing on an Op-Ed page, i.e., reviews, interviews, etc.
        /// UserGenerated: newsworthy user-generated content which has already gone through a formal editorial review process on your site.
        /// </summary>
        [XmlElement("genres", Order = 3)]
        public string Genres { get; set; }


        /// <summary>
        /// Article publication date
        /// Please ensure that you give the original date and time at which the article was published on your site; do not give the time at which the article was added to your Sitemap.
        /// </summary>
        [XmlElement("publication_date", Order = 4)]
        public DateTime PublicationDate { get; set; }


        /// <summary>
        /// The title of the news article. 
        /// Note: The title may be truncated for space reasons when shown on Google News.
        /// Article title tags should only include the title of the article as it appears on your site.
        /// Please make sure not to include the author name, the publication name, or publication date as part of the title tag.
        /// </summary>
        [XmlElement("title", Order = 5)]
        public string Title { get; set; }


        /// <summary>
        /// A comma-separated list of keywords describing the topic of the article. 
        /// </summary>
        [XmlElement("keywords", Order = 6)]
        public string Keywords { get; set; }


        /// <summary>
        /// A comma-separated list of up to 5 stock tickers of the companies, mutual funds, or other financial entities that are the main subject of the article.
        /// Relevant primarily for business articles. Each ticker must be prefixed by the name of its stock exchange, and must match its entry in Google Finance.
        /// For example, "NASDAQ:AMAT" (but not "NASD:AMAT"), or "BOM:500325" (but not "BOM:RIL").
        /// </summary>
        [XmlElement("stock_tickers", Order = 7)]
        public string StockTickers { get; set; }


        /// <summary>
        /// Used for not serializing null values.
        /// </summary>
        public bool ShouldSerializeAccess()
        {
            return Access.HasValue;
        }

    }
}