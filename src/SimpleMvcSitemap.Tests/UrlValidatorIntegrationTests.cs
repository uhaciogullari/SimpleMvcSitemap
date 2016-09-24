using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using SimpleMvcSitemap.Images;
using SimpleMvcSitemap.Routing;
using SimpleMvcSitemap.Videos;
using Xunit;

namespace SimpleMvcSitemap.Tests
{
    public class UrlValidatorIntegrationTests : TestBase
    {
        private readonly IUrlValidator _urlValidator;
        private readonly Mock<IAbsoluteUrlConverter> _absoluteUrlConverter;


        public UrlValidatorIntegrationTests()
        {
            _urlValidator = new UrlValidator(new ReflectionHelper());
            _absoluteUrlConverter = MockFor<IAbsoluteUrlConverter>();
        }

        [Fact]
        public void ValidateUrls_SitemapNode()
        {
            SitemapNode siteMapNode = new SitemapNode("/categories");
            var absoluteUrl = MockAbsoluteUrl(siteMapNode.Url);

            _urlValidator.ValidateUrls(siteMapNode, _absoluteUrlConverter.Object);

            siteMapNode.Url.Should().Be(absoluteUrl);
        }

        [Fact]
        public void ValidateUrls_SitemapIndexNode()
        {
            SitemapIndexNode sitemapIndexNode = new SitemapIndexNode("/product-sitemap");
            var absoluteUrl = MockAbsoluteUrl(sitemapIndexNode.Url);

            _urlValidator.ValidateUrls(sitemapIndexNode, _absoluteUrlConverter.Object);

            sitemapIndexNode.Url.Should().Be(absoluteUrl);
        }

        [Fact]
        public void ValidateUrls_SitemapNodeWithImages()
        {
            var imageUrl = "/image.jpg";
            var licenseUrl = "/licenses/unlicense/";

            SitemapNode sitemapNode = new SitemapNode("abc")
            {
                Images = new List<SitemapImage>
                {
                    new SitemapImage(imageUrl)
                    {
                        License = licenseUrl
                    }
                }
            };

            var absoluteNodeUrl = MockAbsoluteUrl(sitemapNode.Url);
            var absoluteImageUrl = MockAbsoluteUrl(imageUrl);
            var absoluteLicenseUrl = MockAbsoluteUrl(licenseUrl);

            _urlValidator.ValidateUrls(sitemapNode, _absoluteUrlConverter.Object);


            sitemapNode.Url.Should().Be(absoluteNodeUrl);
            var sitemapImage = sitemapNode.Images[0];
            sitemapImage.Url.Should().Be(absoluteImageUrl);
            sitemapImage.License.Should().Be(absoluteLicenseUrl);
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
                    Player = new VideoPlayer("/videoplayer.swf?video=123"),
                    Gallery = new VideoGallery("/gallery-1"),
                    Uploader = new VideoUploader("grillymcgrillerson") { Info = "/users/grillymcgrillerson" }
                }
            };

            var absoluteNodeUrl = MockAbsoluteUrl(sitemapNode.Url);
            var absoluteContentUrl = MockAbsoluteUrl(sitemapNode.Video.ContentUrl);
            var absoluteThumbnailUrl = MockAbsoluteUrl(sitemapNode.Video.ThumbnailUrl);
            var absolutePlayerUrl = MockAbsoluteUrl(sitemapNode.Video.Player.Url);
            var absoluteGalleryUrl = MockAbsoluteUrl(sitemapNode.Video.Gallery.Url);
            var absoluteUploaderUrl = MockAbsoluteUrl(sitemapNode.Video.Uploader.Info);

            _urlValidator.ValidateUrls(sitemapNode, _absoluteUrlConverter.Object);

            sitemapNode.Url.Should().Be(absoluteNodeUrl);
            sitemapNode.Video.ContentUrl.Should().Be(absoluteContentUrl);
            sitemapNode.Video.ThumbnailUrl.Should().Be(absoluteThumbnailUrl);
            sitemapNode.Video.Player.Url.Should().Be(absolutePlayerUrl);
            sitemapNode.Video.Gallery.Url.Should().Be(absoluteGalleryUrl);
            sitemapNode.Video.Uploader.Info.Should().Be(absoluteUploaderUrl);
        }


        private string MockAbsoluteUrl(string relativeUrl)
        {
            string absoluteUrl = Guid.NewGuid().ToString();
            _absoluteUrlConverter.Setup(converter => converter.ConvertToAbsoluteUrl(relativeUrl)).Returns(absoluteUrl);
            return absoluteUrl;
        }
    }
}