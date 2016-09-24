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

        private readonly Mock<IAbsoluteUrlConverter> absoluteUrlConverter;

        public UrlValidatorTests()
        {
            IReflectionHelper reflectionHelper = new FakeReflectionHelper();
            urlValidator = new UrlValidator(reflectionHelper);
            absoluteUrlConverter = MockFor<IAbsoluteUrlConverter>();
        }

        private class SampleType1
        {
            [Url]
            public string Url { get; set; }
        }

        [Fact]
        public void ValidateUrl_ItemIsNull_ThrowsException()
        {
            Action act = () => urlValidator.ValidateUrls(null, absoluteUrlConverter.Object);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void ValidateUrl_AbsoluteUrlConverterIsNull_ThrowsException()
        {
            Action act = () => urlValidator.ValidateUrls(new SampleType1(), null);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void ValidateUrl_UrlIsRelativeUrl_ConvertsToAbsoluteUrl()
        {
            SampleType1 item = new SampleType1 { Url = "/sitemap" };
            var expected = MockAbsoluteUrl(item.Url);

            urlValidator.ValidateUrls(item, absoluteUrlConverter.Object);

            item.Url.Should().Be(expected);
        }

        [Fact]
        public void ValidateUrl_AbsoluteUrl_DoesntChangeUrl()
        {
            SampleType1 item = new SampleType1 { Url = "http://example.org/sitemap" };

            urlValidator.ValidateUrls(item, absoluteUrlConverter.Object);

            item.Url.Should().Be("http://example.org/sitemap");
        }

        [Fact]
        public void ValidateUrl_MalformedUrl_DoesntThrowException()
        {
            string malformedUrl = ":abc";
            SampleType1 item = new SampleType1 { Url = malformedUrl };

            urlValidator.ValidateUrls(item, absoluteUrlConverter.Object);

            item.Url.Should().Be(malformedUrl);
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

            urlValidator.ValidateUrls(item, absoluteUrlConverter.Object);

            item.SampleType1.Url.Should().Be(expected);
        }

        [Fact]
        public void ValidateUrl_NestedObjectIsNull_DoesNotThrowException()
        {
            SampleType2 item = new SampleType2();

            Action action = () => { urlValidator.ValidateUrls(item, absoluteUrlConverter.Object); };

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

            urlValidator.ValidateUrls(item, absoluteUrlConverter.Object);

            item.Items[0].Url.Should().Be(absoluteUrl1);
            item.Items[1].Url.Should().Be(absoluteUrl2);
        }

        [Fact]
        public void ValidateUrl_EnumerablePropertyIsNull_DoesNotThrowException()
        {
            SampleType3 item = new SampleType3();

            Action action = () => { urlValidator.ValidateUrls(item, absoluteUrlConverter.Object); };

            action.ShouldNotThrow();
        }

        [Fact]
        public void ValidateUrl_CallingConsecutivelyWithTheSameType_GetsPropertyModelOnce()
        {
            SampleType1 item = new SampleType1 { Url = "/sitemap" };
            MockAbsoluteUrl(item.Url);

            Action action = () => { urlValidator.ValidateUrls(item, absoluteUrlConverter.Object); };

            action.ShouldNotThrow();
        }

        private string MockAbsoluteUrl(string relativeUrl)
        {
            string absoluteUrl = Guid.NewGuid().ToString();
            absoluteUrlConverter.Setup(converter => converter.ConvertToAbsoluteUrl(relativeUrl)).Returns(absoluteUrl);
            return absoluteUrl;
        }
    }

}