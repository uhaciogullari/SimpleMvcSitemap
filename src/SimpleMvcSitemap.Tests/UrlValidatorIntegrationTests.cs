using System.Collections.Generic;
using FluentAssertions;
using SimpleMvcSitemap.Routing;
using SimpleMvcSitemap.Videos;
using Xunit;

namespace SimpleMvcSitemap.Tests
{
    public class UrlValidatorIntegrationTests : TestBase
    {
        private readonly IUrlValidator _urlValidator;


        public UrlValidatorIntegrationTests()
        {
            //Mock<IBaseUrlProvider> baseUrlProvider = MockFor<IBaseUrlProvider>();
            _urlValidator = new UrlValidator(new ReflectionHelper(), null, null);
        }

        [Fact]
        public void ValidateUrls_SitemapNode()
        {
            SitemapNode siteMapNode = new SitemapNode("/categories");

            _urlValidator.ValidateUrls(siteMapNode);

            siteMapNode.Url.Should().Be("http://example.org/categories");
        }

        [Fact]
        public void ValidateUrls_SitemapIndexNode()
        {
            SitemapIndexNode sitemapIndexNode = new SitemapIndexNode("/product-sitemap");

            _urlValidator.ValidateUrls(sitemapIndexNode);

            sitemapIndexNode.Url.Should().Be("http://example.org/product-sitemap");
        }

        [Fact]
        public void ValidateUrls_SitemapNodeWithImages()
        {
            SitemapNode sitemapNode = new SitemapNode("abc")
            {
                Images = new List<SitemapImage>
                {
                    new SitemapImage("/image.jpg")
                    {
                        License = "/licenses/unlicense/",
                    }
                }
            };

            _urlValidator.ValidateUrls(sitemapNode);

            var sitemapImage = sitemapNode.Images[0];

            sitemapImage.Url.Should().Be("http://example.org/image.jpg");
            sitemapImage.License.Should().Be("http://example.org/licenses/unlicense/");
        }

        [Fact]
        public void ValidateUrls_SitemapNodeWithVideo()
        {
            SitemapNode sitemapNode = new SitemapNode("/some_video_landing_page.html")
            {
                Video = new SitemapVideo
                {
                    ContentUrl = "/video123.flv",
                    ThumbnailUrl = "/thumbs/123.jpg",
                    PlayerUrl = new VideoPlayerUrl
                    {
                        Url = "/videoplayer.swf?video=123",
                    },
                    Gallery = new VideoGallery
                    {
                        Url = "/gallery-1",
                    },
                    Uploader = new VideoUploader
                    {
                        Info = "/users/grillymcgrillerson"
                    }
                }
            };

            _urlValidator.ValidateUrls(sitemapNode);

            sitemapNode.Video.ContentUrl.Should().Be("http://example.org/video123.flv");
            sitemapNode.Video.ThumbnailUrl.Should().Be("http://example.org/thumbs/123.jpg");
            sitemapNode.Video.PlayerUrl.Url.Should().Be("http://example.org/videoplayer.swf?video=123");
            sitemapNode.Video.Gallery.Url.Should().Be("http://example.org/gallery-1");
            sitemapNode.Video.Uploader.Info.Should().Be("http://example.org/users/grillymcgrillerson");
        }

    }
}