using System;
using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    public class SitemapVideo 
    {
        [XmlElement("thumbnail_loc", Order = 1)]
        public string ThumbnailUrl { get; set; }

        [XmlElement("title", Order = 2)]
        public string Title { get; set; }

        [XmlElement("description", Order = 3)]
        public string Description { get; set; }

        [XmlElement("content_loc", Order = 4)]
        public string ContentUrl { get; set; }

        [XmlElement("player_loc", Order = 5)]
        public string PlayerUrl { get; set; }

        [XmlElement("duration", Order = 6)]
        public int? Duration { get; set; }

        [XmlElement("view_count", Order = 7)]
        public long? ViewCount { get; set; }

        [XmlElement("publication_date", Order = 8)]
        public DateTime? PublicationDate { get; set; }

        [XmlElement("family_friendly", Order = 9)]
        public FamilyFriendly? FamilyFriendly { get; set; }


        public bool ShouldSerializePlayerUrl()
        {
            return PlayerUrl != null;
        }

        public bool ShouldSerializeDuration()
        {
            return Duration.HasValue;
        }

        public bool ShouldSerializeViewCount()
        {
            return ViewCount.HasValue;
        }

        public bool ShouldSerializePublicationDate()
        {
            return PublicationDate.HasValue;
        }

        public bool ShouldSerializeFamilyFriendly()
        {
            return FamilyFriendly.HasValue;
        }
    }
}