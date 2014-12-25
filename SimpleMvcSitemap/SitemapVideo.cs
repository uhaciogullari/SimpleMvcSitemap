using System;
using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    /// <summary>
    /// Encloses all information about the video.
    /// </summary>
    public class SitemapVideo
    {
        /// <summary>
        /// A URL pointing to the video thumbnail image file. 
        /// Images must be at least 160 x 90 pixels and at most 1920x1080 pixels. 
        /// We recommend images in .jpg, .png, or. gif formats.
        /// </summary>
        [XmlElement("thumbnail_loc", Order = 1)]
        public string ThumbnailUrl { get; set; }

        /// <summary>
        /// The title of the video. Maximum 100 characters. 
        /// The title must be in plain text only, and any HTML entities should be escaped or wrapped in a CDATA block.
        /// </summary>
        [XmlElement("title", Order = 2)]
        public string Title { get; set; }

        /// <summary>
        /// The description of the video. Maximum 2048 characters. 
        /// The description must be in plain text only, and any HTML entities should be escaped or wrapped in a CDATA block.
        /// </summary>
        [XmlElement("description", Order = 3)]
        public string Description { get; set; }


        /// <summary>
        /// You must specify at least one of  &lt;video:player_loc&gt; or  &lt;video:content_loc&gt;.
        /// A URL pointing to the actual video media file. This file should be in .mpg, .mpeg, .mp4, .m4v, .mov, .wmv, .asf, .avi, .ra, .ram, .rm, .flv, or other video file format.
        /// Providing this file allows Google to generate video thumbnails and video previews, and can help Google verify your video.
        /// Best practice: Ensure that only Googlebot accesses your content by using a reverse DNS lookup.
        /// </summary>
        [XmlElement("content_loc", Order = 4)]
        public string ContentUrl { get; set; }

        /// <summary>
        /// You must specify at least one of  &lt;video:player_loc&gt; or  &lt;video:content_loc&gt;.
        /// A URL pointing to a player for a specific video. 
        /// Usually this is the information in the src element of an &lt;embed&gt; tag and should not be the same as the content of the &lt;loc&gt; tag. ​
        /// </summary>
        [XmlElement("player_loc", Order = 5)]
        public SitemapPlayerUrl PlayerUrl { get; set; }

        /// <summary>
        /// The duration of the video in seconds. Value must be between 0 and 28800 (8 hours).
        /// </summary>
        [XmlElement("duration", Order = 6)]
        public int? Duration { get; set; }

        /// <summary>
        /// The date after which the video will no longer be available, in W3C format. Don't supply this information if your video does not expire.
        /// </summary>
        [XmlElement("expiration_date", Order = 7)]
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// The rating of the video. Allowed values are float numbers in the range 0.0 to 5.0.
        /// </summary>
        [XmlElement("rating", Order = 8)]
        public float? Rating { get; set; }

        /// <summary>
        /// The number of times the video has been viewed.
        /// </summary>
        [XmlElement("view_count", Order = 9)]
        public long? ViewCount { get; set; }

        /// <summary>
        /// The date the video was first published, in W3C format. 
        /// </summary>
        [XmlElement("publication_date", Order = 10)]
        public DateTime? PublicationDate { get; set; }

        /// <summary>
        /// No if the video should be available only to users with SafeSearch turned off.
        /// </summary>
        [XmlElement("family_friendly", Order = 11)]
        public YesNo? FamilyFriendly { get; set; }

        //[XmlElement("tag", Order = 10)]
        //public string[] Tags { get; set; }

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
    }
}