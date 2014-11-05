using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    public class SitemapNews
    {

        [XmlElement("publication", Order = 1)]
        public SitemapNewsPublication Publication { get; set; }

        [XmlElement("genres", Order = 2)]
        public string Genres { get; set; }

        [XmlElement("publication_date", Order = 3)]
        public DateTime? PublicationDate { get; set; }

        [XmlElement("title", Order = 4)]
        public string Title { get; set; }

        [XmlElement("keywords", Order = 5)]
        public string Keywords { get; set; }

        public bool ShouldSerializePublicationDate()
        {
            return PublicationDate.HasValue;
        }
    }
}