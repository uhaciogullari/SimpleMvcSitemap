using System;
using FluentAssertions;
using Moq;
using SimpleMvcSitemap.Routing;
using Xunit;

namespace SimpleMvcSitemap.Tests
{
    public class UrlValidatorTests : TestBase
    {
        private readonly IUrlValidator urlValidator;
        private readonly Mock<IBaseUrlProvider> baseUrlProvider;


        public UrlValidatorTests()
        {
            IReflectionHelper reflectionHelper = new FakeReflectionHelper();
            urlValidator = new UrlValidator(reflectionHelper);

            baseUrlProvider = MockFor<IBaseUrlProvider>();
        }

        private class SampleType1
        {
            [Url]
            public string Url { get; set; }
        }

        [Fact]
        public void ValidateUrls_ItemIsNull_ThrowsException()
        {
            Action act = () => urlValidator.ValidateUrls(null, baseUrlProvider.Object);
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ValidateUrls_BaseUrlProviderIsNull_ThrowsException()
        {
            Action act = () => urlValidator.ValidateUrls(new SampleType1(), null);
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ValidateUrls_UrlIsRelativeUrl_ConvertsToAbsoluteUrl()
        {
            SampleType1 item = new SampleType1 { Url = "/sitemap" };
            SetBaseUrl();

            urlValidator.ValidateUrls(item, baseUrlProvider.Object);

            item.Url.Should().Be("http://example.org/sitemap");
        }


        [Fact]
        public void ValidateUrl_RelativeUrlDeosNotStartWithSlash_BaseUrlEndsWithSlash()
        {
            SampleType1 item = new SampleType1 { Url = "sitemap" };
            SetBaseUrl();

            urlValidator.ValidateUrls(item, baseUrlProvider.Object);

            item.Url.Should().Be("http://example.org/sitemap");
        }

        [Theory]
        [InlineData("sitemap")]
        [InlineData("/sitemap")]
        public void ValidateUrl_BaseUrlInNotDomainRoot(string relativeUrl)
        {
            SampleType1 item = new SampleType1 { Url = relativeUrl };
            SetBaseUrl("http://example.org/app/");

            urlValidator.ValidateUrls(item, baseUrlProvider.Object);

            item.Url.Should().Be("http://example.org/app/sitemap");
        }


        [Fact]
        public void ValidateUrls_AbsoluteUrl_DoesntChangeUrl()
        {
            SampleType1 item = new SampleType1 { Url = "http://example.org/sitemap" };

            urlValidator.ValidateUrls(item, baseUrlProvider.Object);

            item.Url.Should().Be("http://example.org/sitemap");
        }

        [Fact]
        public void ValidateUrls_MalformedUrl_DoesntThrowException()
        {
            string malformedUrl = ":abc";
            SampleType1 item = new SampleType1 { Url = malformedUrl };

            urlValidator.ValidateUrls(item, baseUrlProvider.Object);

            item.Url.Should().Be(malformedUrl);
        }

        private class SampleType2
        {
            public SampleType1 SampleType1 { get; set; }
        }

        [Fact]
        public void ValidateUrls_RelativeUrlInNestedObject_ConvertsToAbsoluteUrl()
        {
            SampleType2 item = new SampleType2 { SampleType1 = new SampleType1 { Url = "/sitemap2" } };
            SetBaseUrl();

            urlValidator.ValidateUrls(item, baseUrlProvider.Object);

            item.SampleType1.Url.Should().Be("http://example.org/sitemap2");
        }

        [Fact]
        public void ValidateUrls_NestedObjectIsNull_DoesNotThrowException()
        {
            SampleType2 item = new SampleType2();

            Action action = () => { urlValidator.ValidateUrls(item, baseUrlProvider.Object); };

            action.Should().NotThrow();
        }


        private class SampleType3
        {
            public SampleType1[] Items { get; set; }
        }

        [Fact]
        public void ValidateUrls_RelativeUrlInList_ConvertsToAbsoluteUrl()
        {
            var relativeUrl1 = "/sitemap/1";
            var relativeUrl2 = "/sitemap/2";
            SampleType3 item = new SampleType3 { Items = new[] { new SampleType1 { Url = relativeUrl1 }, new SampleType1 { Url = relativeUrl2 } } };
            SetBaseUrl();

            urlValidator.ValidateUrls(item, baseUrlProvider.Object);

            item.Items[0].Url.Should().Be("http://example.org/sitemap/1");
            item.Items[1].Url.Should().Be("http://example.org/sitemap/2");
        }

        [Fact]
        public void ValidateUrls_EnumerablePropertyIsNull_DoesNotThrowException()
        {
            SampleType3 item = new SampleType3();

            Action action = () => { urlValidator.ValidateUrls(item, baseUrlProvider.Object); };

            action.Should().NotThrow();
        }

        [Fact]
        public void ValidateUrls_CallingConsecutivelyWithTheSameType_GetsPropertyModelOnce()
        {
            SampleType1 item = new SampleType1 { Url = "/sitemap" };
            SetBaseUrl();

            Action action = () => { urlValidator.ValidateUrls(item, baseUrlProvider.Object); };

            action.Should().NotThrow();
        }

        private void SetBaseUrl(string baseUrl = "http://example.org/")
        {
            baseUrlProvider.Setup(provider => provider.BaseUrl).Returns(new Uri(baseUrl));
        }
    }

}