using System;
using FluentAssertions;
using Moq;
using SimpleMvcSitemap.Routing;
using Xunit;

namespace SimpleMvcSitemap.Tests
{
    public class UrlValidatorTests : TestBase
    {
        private readonly IUrlValidator _urlValidator;

        private readonly IReflectionHelper _reflectionHelper;
        private readonly Mock<IAbsoluteUrlConverter> _absoluteUrlConverter;

        public UrlValidatorTests()
        {
            _reflectionHelper = new FakeReflectionHelper();
            _urlValidator = new UrlValidator(_reflectionHelper);
            _absoluteUrlConverter = MockFor<IAbsoluteUrlConverter>();
        }

        private class SampleType1
        {
            [Url]
            public string Url { get; set; }
        }

        [Fact]
        public void ValidateUrl_ItemIsNull_ThrowsException()
        {
            Action act = () => _urlValidator.ValidateUrls(null, _absoluteUrlConverter.Object);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void ValidateUrl_AbsoluteUrlConverterIsNull_ThrowsException()
        {
            Action act = () => _urlValidator.ValidateUrls(new SampleType1(), null);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void ValidateUrl_UrlIsRelativeUrl_ConvertsToAbsoluteUrl()
        {
            SampleType1 item = new SampleType1 { Url = "/sitemap" };
            var expected = MockAbsoluteUrl(item.Url);

            _urlValidator.ValidateUrls(item, _absoluteUrlConverter.Object);

            item.Url.Should().Be(expected);
        }

        [Fact]
        public void ValidateUrl_AbsoluteUrl_DoesntChangeUrl()
        {
            SampleType1 item = new SampleType1 { Url = "http://example.org/sitemap" };

            _urlValidator.ValidateUrls(item, _absoluteUrlConverter.Object);

            item.Url.Should().Be("http://example.org/sitemap");
        }

        private class SampleType2
        {
            public SampleType1 SampleType1 { get; set; }
        }

        [Fact]
        public void ValidateUrl_RelativeUrlInNestedObject_ConvertsToAbsoluteUrl()
        {
            SampleType2 item = new SampleType2 { SampleType1 = new SampleType1 { Url = "/sitemap" } };
            var expected = MockAbsoluteUrl(item.SampleType1.Url);

            _urlValidator.ValidateUrls(item, _absoluteUrlConverter.Object);

            item.SampleType1.Url.Should().Be(expected);
        }

        [Fact]
        public void ValidateUrl_NestedObjectIsNull_DoesNotThrowException()
        {
            SampleType2 item = new SampleType2();

            Action action = () => { _urlValidator.ValidateUrls(item, _absoluteUrlConverter.Object); };

            action.ShouldNotThrow();
        }


        private class SampleType3
        {
            public SampleType1[] Items { get; set; }
        }

        [Fact]
        public void ValidateUrl_RelativeUrlInList_ConvertsToAbsoluteUrl()
        {
            var relativeUrl1 = "/sitemap/1";
            var relativeUrl2 = "/sitemap/2";
            SampleType3 item = new SampleType3 { Items = new[] { new SampleType1 { Url = relativeUrl1 }, new SampleType1 { Url = relativeUrl2 } } };

            var absoluteUrl1 = MockAbsoluteUrl(relativeUrl1);
            var absoluteUrl2 = MockAbsoluteUrl(relativeUrl2);

            _urlValidator.ValidateUrls(item, _absoluteUrlConverter.Object);

            item.Items[0].Url.Should().Be(absoluteUrl1);
            item.Items[1].Url.Should().Be(absoluteUrl2);
        }

        [Fact]
        public void ValidateUrl_EnumerablePropertyIsNull_DoesNotThrowException()
        {
            SampleType3 item = new SampleType3();

            Action action = () => { _urlValidator.ValidateUrls(item, _absoluteUrlConverter.Object); };

            action.ShouldNotThrow();
        }

        [Fact]
        public void ValidateUrl_CallingConsecutivelyWithTheSameType_GetsPropertyModelOnce()
        {
            SampleType1 item = new SampleType1 { Url = "/sitemap" };
            MockAbsoluteUrl(item.Url);

            Action action = () => { _urlValidator.ValidateUrls(item, _absoluteUrlConverter.Object); };

            action.ShouldNotThrow();
        }

        private string MockAbsoluteUrl(string relativeUrl)
        {
            string absoluteUrl = Guid.NewGuid().ToString();
            _absoluteUrlConverter.Setup(converter => converter.ConvertToAbsoluteUrl(relativeUrl)).Returns(absoluteUrl);
            return absoluteUrl;
        }
    }

}