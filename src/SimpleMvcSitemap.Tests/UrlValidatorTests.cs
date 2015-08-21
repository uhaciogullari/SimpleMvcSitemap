using System;
using System.Web;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace SimpleMvcSitemap.Tests
{
    public class UrlValidatorTests : TestBase
    {
        private IUrlValidator _urlValidator;
        private string _baseUrl;
        private IReflectionHelper _reflectionHelper;
        private Mock<IBaseUrlProvider> _baseUrlProvider;

        protected override void FinalizeSetUp()
        {
            _baseUrl = "http://example.org";
            _reflectionHelper = new FakeReflectionHelper();
            _baseUrlProvider = MockFor<IBaseUrlProvider>();
            _urlValidator = new UrlValidator(_reflectionHelper, _baseUrlProvider.Object);
        }

        private class SampleType1
        {
            [Url]
            public string Url { get; set; }
        }

        [Test]
        public void ValidateUrl_UrlIsRelativeUrl_ConvertsToAbsoluteUrl()
        {
            SampleType1 item = new SampleType1 { Url = "/sitemap" };
            MockBaseUrl();

            _urlValidator.ValidateUrls(null, item);

            item.Url.Should().Be("http://example.org/sitemap");
        }

        [Test]
        public void ValidateUrl_AbsoluteUrl_DoesntChangeUrl()
        {
            SampleType1 item = new SampleType1 { Url = "http://example.org/sitemap" };

            _urlValidator.ValidateUrls(null, item);

            item.Url.Should().Be("http://example.org/sitemap");
        }

        [Test]
        public void ValidateUrl_ItemIsNull_ThrowsException()
        {
            Action act = () => _urlValidator.ValidateUrls(null, null);
            act.ShouldThrow<ArgumentNullException>();
        }

        private class SampleType2
        {
            public SampleType1 SampleType1 { get; set; }
        }

        [Test]
        public void ValidateUrl_RelativeUrlInNestedObject_ConvertsToAbsoluteUrl()
        {
            SampleType2 item = new SampleType2 { SampleType1 = new SampleType1 { Url = "/sitemap" } };
            MockBaseUrl();

            _urlValidator.ValidateUrls(null, item);

            item.SampleType1.Url.Should().Be("http://example.org/sitemap");
        }

        [Test]
        public void ValidateUrl_NestedObjectIsNull_DoesNotThrowException()
        {
            SampleType2 item = new SampleType2();

            Action action = () => { _urlValidator.ValidateUrls(null, item); };

            action.ShouldNotThrow();
        }


        private class SampleType3
        {
            public SampleType1[] Items { get; set; }
        }

        [Test]
        public void ValidateUrl_RelativeUrlInList_ConvertsToAbsoluteUrl()
        {
            SampleType3 item = new SampleType3 { Items = new[] { new SampleType1 { Url = "/sitemap/1" }, new SampleType1 { Url = "/sitemap/2" } } };
            MockBaseUrl();

            _urlValidator.ValidateUrls(null, item);

            item.Items[0].Url.Should().Be("http://example.org/sitemap/1");
            item.Items[1].Url.Should().Be("http://example.org/sitemap/2");
        }

        [Test]
        public void ValidateUrl_EnumerablePropertyIsNull_DoesNotThrowException()
        {
            SampleType3 item = new SampleType3();

            Action action = () => { _urlValidator.ValidateUrls(null, item); };

            action.ShouldNotThrow();
        }

        [Test]
        public void ValidateUrl_CallingConsecutivelyWithTheSameType_GetsPropertyModelOnce()
        {
            SampleType1 item = new SampleType1 { Url = "/sitemap" };
            MockBaseUrl();

            _urlValidator.ValidateUrls(null, item);

            Action action = () => { _urlValidator.ValidateUrls(null, item); };

            action.ShouldNotThrow();
        }

        private void MockBaseUrl()
        {
            _baseUrlProvider.Setup(item => item.GetBaseUrl(It.IsAny<HttpContextBase>())).Returns(_baseUrl);
        }

    }

}