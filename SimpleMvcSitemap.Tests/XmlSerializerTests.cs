using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace SimpleMvcSitemap.Tests
{
    public class XmlSerializerTests : TestBase
    {
        private IXmlSerializer _serializer;

        private Mock<IXmlNamespaceBuilder> _namespaceBuilder;

        XmlSerializerNamespaces _namespaces;

        protected override void FinalizeSetUp()
        {
            _namespaceBuilder = MockFor<IXmlNamespaceBuilder>();
            _serializer = new XmlSerializer(_namespaceBuilder.Object);

            _namespaces = new XmlSerializerNamespaces();
            _namespaces.Add(Namespaces.SitemapPrefix, Namespaces.Sitemap);
            _namespaceBuilder.Setup(item => item.Create(It.IsAny<IEnumerable<string>>())).Returns(_namespaces);
        }

        [Test]
        public void Serialize_SitemapModel()
        {
            SitemapModel sitemap = new SitemapModel(new List<SitemapNode> { new SitemapNode("abc"), new SitemapNode("def") });

            string result = _serializer.Serialize(sitemap);

            result.Should().BeXmlEquivalent("Samples/sitemap.xml");
        }

        [Test]
        public void Serialize_SitemapIndexModel()
        {
            SitemapIndexModel sitemapIndex = new SitemapIndexModel(new List<SitemapIndexNode>
            {
                new SitemapIndexNode { Url = "abc" },
                new SitemapIndexNode { Url = "def" }
            });

            string result = _serializer.Serialize(sitemapIndex);

            result.Should().BeXmlEquivalent("Samples/sitemap-index.xml");
        }

        [Test]
        public void Serialize_SitemapNode_RequiredTegs()
        {
            SitemapNode sitemapNode = new SitemapNode("abc");

            string result = _serializer.Serialize(sitemapNode);

            result.Should().BeXmlEquivalent("Samples/sitemap-node-required.xml");
        }

        [Test]
        public void Serialize_SitemapNode_AllTags()
        {
            SitemapNode sitemapNode = new SitemapNode("abc")
            {
                LastModificationDate = new DateTime(2013, 12, 11, 16, 05, 00, DateTimeKind.Utc),
                ChangeFrequency = ChangeFrequency.Weekly,
                Priority = 0.8M
            };

            string result = _serializer.Serialize(sitemapNode);

            result.Should().BeXmlEquivalent("Samples/sitemap-node-all.xml");
        }

        [Test]
        public void Serialize_SitemapIndexNode_RequiredTags()
        {
            SitemapIndexNode sitemapIndexNode = new SitemapIndexNode("abc");

            string result = _serializer.Serialize(sitemapIndexNode);

            result.Should().BeXmlEquivalent("Samples/sitemap-index-node-required.xml");
        }

        [Test]
        public void Serialize_SitemapIndexNode_AllTags()
        {
            SitemapIndexNode sitemapIndexNode = new SitemapIndexNode
            {
                Url = "abc",
                LastModificationDate = new DateTime(2013, 12, 11, 16, 05, 00, DateTimeKind.Utc)
            };

            string result = _serializer.Serialize(sitemapIndexNode);

            result.Should().BeXmlEquivalent("Samples/sitemap-index-node-all.xml");
        }

        [Test]
        public void Serialize_SitemapNode_ImageRequiredTags()
        {
            SitemapNode sitemapNode = new SitemapNode("abc")
            {
                Images = new List<SitemapImage> { new SitemapImage("image1"), new SitemapImage("image2") }
            };

            _namespaces.Add(Namespaces.ImagePrefix, Namespaces.Image);

            string result = _serializer.Serialize(sitemapNode);

            result.Should().BeXmlEquivalent("Samples/sitemap-node-image-required.xml");
        }

        [Test]
        public void Serialize_SitemapNode_ImageAllTags()
        {
            SitemapNode sitemapNode = new SitemapNode("abc")
            {
                Images = new List<SitemapImage> 
                { 
                    new SitemapImage("http://example.com/image.jpg")
                    {
                        Caption = "Photo caption",
                        Location = "Limerick, Ireland",
                        License = "http://choosealicense.com/licenses/unlicense/",
                        Title = "Photo Title"
                    }
                }
            };

            _namespaces.Add(Namespaces.ImagePrefix, Namespaces.Image);

            string result = _serializer.Serialize(sitemapNode);

            result.Should().BeXmlEquivalent("Samples/sitemap-node-image-all.xml");
        }

        [Test]
        public void Serialize_SitemapNode_VideoRequiredTags()
        {
            SitemapNode sitemapNode = new SitemapNode("http://www.example.com/videos/some_video_landing_page.html")
            {
                Video = new SitemapVideo
                {
                    ContentUrl = "http://www.example.com/video123.flv",
                    Description = "Alkis shows you how to get perfectly done steaks every time",
                    ThumbnailUrl = "http://www.example.com/thumbs/123.jpg",
                    Title = "Grilling steaks for summer"
                }
            };

            _namespaces.Add(Namespaces.VideoPrefix, Namespaces.Video);

            string result = _serializer.Serialize(sitemapNode);

            result.Should().BeXmlEquivalent("Samples/sitemap-node-video-required.xml");
        }

        [Test]
        public void Serialize_SitemapNode_VideoAllTags()
        {
            SitemapNode sitemapNode = new SitemapNode("http://www.example.com/videos/some_video_landing_page.html")
            {
                Video = new SitemapVideo
                {
                    ContentUrl = "http://www.example.com/video123.flv",
                    Description = "Alkis shows you how to get perfectly done steaks every time",
                    ThumbnailUrl = "http://www.example.com/thumbs/123.jpg",
                    Title = "Grilling steaks for summer",
                    PlayerUrl = new VideoPlayerUrl
                    {
                        Url = "http://www.example.com/videoplayer.swf?video=123",
                        AllowEmbed = YesNo.Yes,
                        Autoplay = "ap=1"
                    },
                    Duration = 600,
                    ExpirationDate = new DateTime(2014, 12, 16, 16, 56, 0, DateTimeKind.Utc),
                    Rating = 4.2M,
                    ViewCount = 12345,
                    PublicationDate = new DateTime(2014, 12, 16, 17, 51, 0, DateTimeKind.Utc),
                    FamilyFriendly = YesNo.No,
                    Tags = new[] { "steak", "summer", "outdoor" },
                    Category = "Grilling",
                    Restriction = new VideoRestriction
                    {
                        Relationship = VideoRestrictionRelationship.Allow,
                        Countries = "IE GB US CA"
                    },
                    Gallery = new VideoGallery
                    {
                        Url = "http://cooking.example.com",
                        Title = "Cooking Videos"
                    },
                    Prices = new List<VideoPrice>
                    {
                        new VideoPrice{Currency = "EUR",Value = 1.99M },
                        new VideoPrice{Currency = "TRY",Value = 5.99M,Type = VideoPurchaseOption.Rent},
                        new VideoPrice{Currency = "USD",Value = 2.99M, Resolution = VideoPurchaseResolution.Hd}
                    },
                    RequiresSubscription = YesNo.No,
                    Uploader = new VideoUploader
                    {
                        Name = "GrillyMcGrillerson",
                        Info = "http://www.example.com/users/grillymcgrillerson"
                    },
                    Platform = "web mobile",
                    Live = YesNo.Yes
                }
            };

            _namespaces.Add(Namespaces.VideoPrefix, Namespaces.Video);

            string result = _serializer.Serialize(sitemapNode);

            result.Should().BeXmlEquivalent("Samples/sitemap-node-video-all.xml");
        }

        [Test]
        public void Serialize_SitemapNode_NewsReqiredTags()
        {
            SitemapNode sitemapNode = new SitemapNode("http://www.example.org/business/article55.html")
            {
                News = new SitemapNews
                {
                    Publication = new NewsPublication { Name = "The Example Times", Language = "en" },
                    Genres = "PressRelease, Blog",
                    PublicationDate = new DateTime(2014, 11, 5, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Companies A, B in Merger Talks"
                }
            };

            _namespaces.Add(Namespaces.NewsPrefix, Namespaces.News);

            string result = _serializer.Serialize(sitemapNode);

            result.Should().BeXmlEquivalent("Samples/sitemap-node-news-required.xml");
        }

        [Test]
        public void Serialize_SitemapNode_NewsAllTags()
        {
            SitemapNode sitemapNode = new SitemapNode("http://www.example.org/business/article55.html")
            {
                News = new SitemapNews
                {
                    Publication = new NewsPublication { Name = "The Example Times", Language = "en" },
                    Access = NewsAccess.Subscription,
                    Genres = "PressRelease, Blog",
                    PublicationDate = new DateTime(2014, 11, 5, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Companies A, B in Merger Talks",
                    Keywords = "business, merger, acquisition, A, B",
                    StockTickers = "NASDAQ:A, NASDAQ:B"
                }
            };

            _namespaces.Add(Namespaces.NewsPrefix, Namespaces.News);

            string result = _serializer.Serialize(sitemapNode);

            result.Should().BeXmlEquivalent("Samples/sitemap-node-news-all.xml");
        }

        [Test]
        public void Serialize_SitemapNode_Mobile()
        {
            SitemapNode sitemapNode = new SitemapNode("http://mobile.example.com/article100.html") { Mobile = new SitemapMobile() };

            _namespaces.Add(Namespaces.MobilePrefix, Namespaces.Mobile);

            string result = _serializer.Serialize(sitemapNode);

            result.Should().BeXmlEquivalent("Samples/sitemap-node-mobile.xml");
        }
    }
}