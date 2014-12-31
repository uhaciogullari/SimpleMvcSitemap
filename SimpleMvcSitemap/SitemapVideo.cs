using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    /// <summary>
    /// Encloses all information about the video.
    /// </summary>
    public class SitemapVideo
    {
        internal SitemapVideo() { }

        /// <summary>
        /// Creates an instance of SitemapVideo
        /// </summary>
        /// <param name="title">The title of the video. Maximum 100 characters.</param>
        /// <param name="description">The description of the video. Maximum 2048 characters.</param>
        /// <param name="thumbnailUrl"></param>
        /// <param name="contentUrl">A URL pointing to the actual video media file.
        ///     This file should be in .mpg, .mpeg, .mp4, .m4v, .mov, .wmv, .asf, .avi, .ra, .ram, .rm, .flv, or other video file format.</param>
        public SitemapVideo(string title, string description, string thumbnailUrl, string contentUrl)
        {
            Title = title;
            Description = description;
            ThumbnailUrl = thumbnailUrl;
            ContentUrl = contentUrl;
        }


        /// <summary>
        /// Creates an instance of SitemapVideo
        /// </summary>
        /// <param name="title">The title of the video. Maximum 100 characters.</param>
        /// <param name="description">The description of the video. Maximum 2048 characters.</param>
        /// <param name="thumbnailUrl"></param>
        /// <param name="playerUrl">A URL pointing to a player for a specific video.</param>
        public SitemapVideo(string title, string description, string thumbnailUrl, VideoPlayerUrl playerUrl)
        {
            Title = title;
            Description = description;
            ThumbnailUrl = thumbnailUrl;
            PlayerUrl = playerUrl;
        }

        /// <summary>
        /// A URL pointing to the video thumbnail image file. 
        /// Images must be at least 160 x 90 pixels and at most 1920x1080 pixels. 
        /// We recommend images in .jpg, .png, or. gif formats.
        /// </summary>
        [XmlElement("thumbnail_loc", Order = 1), Url]
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
        [XmlElement("content_loc", Order = 4), Url]
        public string ContentUrl { get; set; }


        /// <summary>
        /// You must specify at least one of  &lt;video:player_loc&gt; or  &lt;video:content_loc&gt;.
        /// A URL pointing to a player for a specific video. 
        /// Usually this is the information in the src element of an &lt;embed&gt; tag and should not be the same as the content of the &lt;loc&gt; tag. ​
        /// </summary>
        [XmlElement("player_loc", Order = 5)]
        public VideoPlayerUrl PlayerUrl { get; set; }


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
        public decimal? Rating { get; set; }


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


        /// <summary>
        /// A tag associated with the video. 
        /// Tags are generally very short descriptions of key concepts associated with a video or piece of content. 
        /// A single video could have several tags, although it might belong to only one category. 
        /// For example, a video about grilling food may belong in the Grilling category, but could be tagged "steak", "meat", "summer", and "outdoor". 
        /// Create a new &lt;video:tag&gt; element for each tag associated with a video. A maximum of 32 tags is permitted.
        /// </summary>
        [XmlElement("tag", Order = 12)]
        public string[] Tags { get; set; }


        /// <summary>
        /// The video's category. 
        /// For example, cooking. The value should be a string no longer than 256 characters.
        /// In general, categories are broad groupings of content by subject.
        /// Usually a video will belong to a single category.
        /// For example, a site about cooking could have categories for Broiling, Baking, and Grilling.
        /// </summary>
        [XmlElement("category", Order = 13)]
        public string Category { get; set; }


        /// <summary>
        /// List of countries where the video may or may not be played. 
        /// Only one &lt;video:restriction&gt; tag can appear for each video. If there is no &lt;video:restriction&gt; tag, 
        /// it is assumed that the video can be played in all territories.
        /// </summary>
        [XmlElement("restriction", Order = 14)]
        public VideoRestriction Restriction { get; set; }


        /// <summary>
        /// A link to the gallery (collection of videos) in which this video appears. 
        /// Only one &lt;video:gallery_loc&gt; tag can be listed for each video.
        /// </summary>
        [XmlElement("gallery_loc", Order = 15)]
        public VideoGallery Gallery { get; set; }


        /// <summary>
        /// The price to download or view the video. Do not use this tag for free videos.
        /// More than one &lt;video:price&gt; element can be listed (for example, in order to specify various currencies, purchasing options, or resolutions).
        /// </summary>
        [XmlElement("price", Order = 16)]
        public List<VideoPrice> Prices { get; set; }


        /// <summary>
        /// Indicates whether a subscription (either paid or free) is required to view the video. Allowed values are yes or no.
        /// </summary>
        [XmlElement("requires_subscription", Order = 17)]
        public YesNo? RequiresSubscription { get; set; }


        /// <summary>
        /// The video uploader's name. Only one &lt;video:uploader&gt; is allowed per video.
        /// </summary>
        [XmlElement("uploader", Order = 18)]
        public VideoUploader Uploader { get; set; }


        /// <summary>
        /// A list of space-delimited platforms where the video may or may not be played. 
        /// Allowed values are web, mobile, and tv.
        /// Only one &lt;video:platform&gt; tag can appear for each video. 
        /// If there is no &lt;video:platform&gt; tag, it is assumed that the video can be played on all platforms.
        /// </summary>
        [XmlElement("platform", Order = 19)]
        public string Platform { get; set; }


        /// <summary>
        /// Indicates whether the video is a live stream.
        /// Allowed values are yes or no.
        /// </summary>
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