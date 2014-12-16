using System;
using System.Collections.Generic;
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
        public SitemapPlayerUrl PlayerUrl { get; set; }

        [XmlElement("duration", Order = 6)]
        public int? Duration { get; set; }

        [XmlElement("expiration_date", Order = 7)]
        public DateTime? ExpirationDate { get; set; }

        [XmlElement("rating", Order = 8)]
        public float? Rating { get; set; }

        [XmlElement("view_count", Order = 9)]
        public long? ViewCount { get; set; }

        [XmlElement("publication_date", Order = 10)]
        public DateTime? PublicationDate { get; set; }

        [XmlElement("family_friendly", Order = 11)]
        public YesNo? FamilyFriendly { get; set; }

        [XmlElement("tag", Order = 12)]
        public string[] Tags { get; set; }

        [XmlElement("category", Order = 13)]
        public string Category { get; set; }

        [XmlElement("restriction", Order = 14)]
        public VideoRestriction Restriction { get; set; }

        [XmlElement("gallery_loc", Order = 15)]
        public VideoGallery Gallery { get; set; }

        [XmlElement("price", Order = 16)]
        public List<VideoPrice> Prices { get; set; }

        [XmlElement("requires_subscription", Order = 17)]
        public YesNo? RequiresSubscription { get; set; }

        [XmlElement("uploader", Order = 18)]
        public VideoUploader Uploader { get; set; }

        [XmlElement("platform", Order = 19)]
        public string Platform { get; set; }

        [XmlElement("live", Order = 20)]
        public YesNo? Live { get; set; }

        public bool ShouldSerializeDuration()
        {
            return Duration.HasValue;
        }

        public bool ShouldSerializeExpirationDate()
        {
            return ExpirationDate.HasValue;
        }

        public bool ShouldSerializeRating()
        {
            return Rating.HasValue;
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

        public bool ShouldSerializeTags()
        {
            return Tags != null;
        }

        public bool ShouldSerializeRequiresSubscription()
        {
            return RequiresSubscription.HasValue;
        }

        public bool ShouldSerializeLive()
        {
            return Live.HasValue;
        }
    }
}