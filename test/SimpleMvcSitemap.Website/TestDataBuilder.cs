using System;
using System.Collections.Generic;
using System.Linq;
using SimpleMvcSitemap.Images;
using SimpleMvcSitemap.News;
using SimpleMvcSitemap.StyleSheets;
using SimpleMvcSitemap.Translations;
using SimpleMvcSitemap.Videos;

namespace SimpleMvcSitemap.Tests
{
    public class TestDataBuilder
    {
        public SitemapNode CreateSitemapNodeWithRequiredProperties()
        {
            return new SitemapNode("abc");
        }


        public SitemapNode CreateSitemapNodeWithAllProperties()
        {
            return new SitemapNode("abc")
            {
                LastModificationDate = new DateTime(2013, 12, 11, 16, 05, 00, DateTimeKind.Utc),
                ChangeFrequency = ChangeFrequency.Weekly,
                Priority = 0.8M
            };
        }

        public SitemapIndexNode CreateSitemapIndexNodeWithRequiredProperties()
        {
            return new SitemapIndexNode("abc");
        }

        public SitemapIndexNode CreateSitemapIndexNodeWithAllProperties()
        {
            return new SitemapIndexNode("abc")
            {
                LastModificationDate = new DateTime(2013, 12, 11, 16, 05, 00, DateTimeKind.Utc)
            };
        }

        public SitemapNode CreateSitemapNodeWithImageRequiredProperties()
        {
            return new SitemapNode("abc")
            {
                Images = new List<SitemapImage> { new SitemapImage("image1"), new SitemapImage("image2") }
            };
        }

        public SitemapNode CreateSitemapNodeWithImageAllProperties()
        {
            return new SitemapNode("abc")
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
        }

        public SitemapNode CreateSitemapNodeWithVideoRequiredProperties()
        {
            return new SitemapNode("http://www.example.com/videos/some_video_landing_page.html")
            {
                Videos = new List<SitemapVideo>()
                {
                    new SitemapVideo("Grilling steaks for summer", "Alkis shows you how to get perfectly done steaks every time",
                        "http://www.example.com/thumbs/123.jpg", "http://www.example.com/video123.flv")
                }
            };
        }


        public SitemapNode CreateSitemapNodeWithVideoAllProperties()
        {
            return new SitemapNode("http://www.example.com/videos/some_video_landing_page.html")
            {
                Videos = new List<SitemapVideo>()
                {
                    new SitemapVideo("Grilling steaks for summer", "Alkis shows you how to get perfectly done steaks every time",
                        "http://www.example.com/thumbs/123.jpg", "http://www.example.com/video123.flv")
                    {
                        Player = new VideoPlayer("http://www.example.com/videoplayer.swf?video=123")
                        {
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
                        Restriction = new VideoRestriction("IE GB US CA", VideoRestrictionRelationship.Allow),
                        Gallery = new VideoGallery("http://cooking.example.com")
                        {
                            Title = "Cooking Videos"
                        },
                        Prices = new List<VideoPrice>
                        {
                            new VideoPrice("EUR",1.99M),
                            new VideoPrice("TRY",5.99M){Type = VideoPurchaseOption.Rent},
                            new VideoPrice("USD",2.99M){Resolution = VideoPurchaseResolution.Hd}
                        },
                        RequiresSubscription = YesNo.No,
                        Uploader = new VideoUploader("GrillyMcGrillerson")
                        {
                            Info = "http://www.example.com/users/grillymcgrillerson"
                        },
                        Platform = "web mobile",
                        Live = YesNo.Yes
                    }
                }
            };
        }

        public SitemapNode CreateSitemapNodeWithNewsRequiredProperties()
        {
            return new SitemapNode("http://www.example.org/business/article55.html")
            {
                News = new SitemapNews(new NewsPublication("The Example Times", "en"), new DateTime(2014, 11, 5, 0, 0, 0, DateTimeKind.Utc), "Companies A, B in Merger Talks")
            };
        }


        public SitemapNode CreateSitemapNodeWithNewsAllProperties()
        {
            return new SitemapNode("http://www.example.org/business/article55.html")
            {
                News = new SitemapNews(new NewsPublication("The Example Times", "en"), new DateTime(2014, 11, 5, 0, 0, 0, DateTimeKind.Utc), "Companies A, B in Merger Talks")
                {
                    Access = NewsAccess.Subscription,
                    Genres = "PressRelease, Blog",
                    Keywords = "business, merger, acquisition, A, B",
                    StockTickers = "NASDAQ:A, NASDAQ:B"
                }
            };
        }

        public SitemapModel CreateSitemapWithTranslations()
        {
            var sitemapNodes = new List<SitemapNode>
            {
                new SitemapNode("abc")
                {
                    Translations = new List<SitemapPageTranslation>
                    {
                        new SitemapPageTranslation("cba", "de")
                    }
                },
                new SitemapNode("def")
                {
                    Translations = new List<SitemapPageTranslation>
                    {
                        new SitemapPageTranslation("fed", "de")
                    }
                }
            };

            return new SitemapModel(sitemapNodes);
        }

        public SitemapModel CreateSitemapWithSingleStyleSheet()
        {
            return new SitemapModel(new List<SitemapNode> { new SitemapNode("abc") })
            {
                StyleSheets = new List<XmlStyleSheet>
                {
                    new XmlStyleSheet("/sitemap.xsl")
                }
            };
        }

        public SitemapModel CreateSitemapWithMultipleStyleSheets()
        {
            return new SitemapModel(new List<SitemapNode> { new SitemapNode("abc") })
            {
                StyleSheets = new List<XmlStyleSheet>
                {
                    new XmlStyleSheet("/regular.css") {Type = "text/css",Title = "Regular fonts",Media = "screen"},
                    new XmlStyleSheet("/bigfonts.css") {Type = "text/css",Title = "Extra large fonts",Media = "projection",Alternate = YesNo.Yes},
                    new XmlStyleSheet("/smallfonts.css") {Type = "text/css",Title = "Smaller fonts",Media = "print",Alternate = YesNo.Yes,Charset = "UTF-8"}
                }
            };
        }


        public SitemapModel CreateHugeSitemap(int nodeCount = 50000)
        {
            var nodes = Enumerable.Range(1, nodeCount).Select(i => new SitemapNode($"page{i}")).ToList();
            return new SitemapModel(nodes);
        }
    }
}