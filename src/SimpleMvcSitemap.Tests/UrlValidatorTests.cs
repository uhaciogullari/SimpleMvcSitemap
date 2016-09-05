using System;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Moq;
using Xunit;

namespace SimpleMvcSitemap.Tests
{
    public class UrlValidatorTests : TestBase
    {
        private readonly IUrlValidator _urlValidator;

        private readonly IReflectionHelper _reflectionHelper;

        private readonly Mock<IUrlHelperFactory> _urlHelperFactory;
        private readonly Mock<IUrlHelper> _urlHelper;


        public UrlValidatorTests()
        {
            _reflectionHelper = new FakeReflectionHelper();
            _urlHelperFactory = MockFor<IUrlHelperFactory>();
            Mock<IActionContextAccessor> actionContextAccessor = MockFor<IActionContextAccessor>();
            _urlValidator = new UrlValidator(_reflectionHelper, _urlHelperFactory.Object,actionContextAccessor.Object);


            var actionContext = new ActionContext();
            actionContextAccessor.Setup(accessor => accessor.ActionContext).Returns(actionContext);
            _urlHelper = MockFor<IUrlHelper>();
            _urlHelperFactory.Setup(factory => factory.GetUrlHelper(actionContext)).Returns(_urlHelper.Object);
        }

        private class SampleType1
        {
            [Url]
            public string Url { get; set; }
        }

        [Fact]
        public void ValidateUrl_UrlIsRelativeUrl_ConvertsToAbsoluteUrl()
        {
            SampleType1 item = new SampleType1 { Url = "/sitemap" };
            _urlHelper.Setup(helper => helper.IsLocalUrl(item.Url)).Returns(true);
            var expected = "http://example.org/sitemap";
            _urlHelper.Setup(helper => helper.Content(item.Url)).Returns(expected);

            _urlValidator.ValidateUrls(item);

            item.Url.Should().Be(expected);
        }

        [Fact]
        public void ValidateUrl_AbsoluteUrl_DoesntChangeUrl()
        {
            SampleType1 item = new SampleType1 { Url = "http://example.org/sitemap" };
            _urlHelper.Setup(helper => helper.IsLocalUrl(item.Url)).Returns(false);

            _urlValidator.ValidateUrls(item);

            item.Url.Should().Be("http://example.org/sitemap");
        }

        [Fact]
        public void ValidateUrl_ItemIsNull_ThrowsException()
        {
            Action act = () => _urlValidator.ValidateUrls(null);
            act.ShouldThrow<ArgumentNullException>();
        }

        private class SampleType2
        {
            public SampleType1 SampleType1 { get; set; }
        }

        [Fact]
        public void ValidateUrl_RelativeUrlInNestedObject_ConvertsToAbsoluteUrl()
        {
            SampleType2 item = new SampleType2 { SampleType1 = new SampleType1 { Url = "/sitemap" } };

            _urlValidator.ValidateUrls(item);

            item.SampleType1.Url.Should().Be("http://example.org/sitemap");
        }

        [Fact]
        public void ValidateUrl_NestedObjectIsNull_DoesNotThrowException()
        {
            SampleType2 item = new SampleType2();

            Action action = () => { _urlValidator.ValidateUrls(item); };

            action.ShouldNotThrow();
        }


        private class SampleType3
        {
            public SampleType1[] Items { get; set; }
        }

        [Fact]
        public void ValidateUrl_RelativeUrlInList_ConvertsToAbsoluteUrl()
        {
            SampleType3 item = new SampleType3 { Items = new[] { new SampleType1 { Url = "/sitemap/1" }, new SampleType1 { Url = "/sitemap/2" } } };

            _urlValidator.ValidateUrls(item);

            item.Items[0].Url.Should().Be("http://example.org/sitemap/1");
            item.Items[1].Url.Should().Be("http://example.org/sitemap/2");
        }

        [Fact]
        public void ValidateUrl_EnumerablePropertyIsNull_DoesNotThrowException()
        {
            SampleType3 item = new SampleType3();

            Action action = () => { _urlValidator.ValidateUrls(item); };

            action.ShouldNotThrow();
        }

        [Fact]
        public void ValidateUrl_CallingConsecutivelyWithTheSameType_GetsPropertyModelOnce()
        {
            SampleType1 item = new SampleType1 { Url = "/sitemap" };

            _urlValidator.ValidateUrls(item);

            Action action = () => { _urlValidator.ValidateUrls(item); };

            action.ShouldNotThrow();
        }
    }

}