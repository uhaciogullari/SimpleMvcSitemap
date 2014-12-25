using FluentAssertions;
using NUnit.Framework;

namespace SimpleMvcSitemap.Tests
{
    public class UrlValidatorTests : TestBase
    {
        private IUrlValidator _urlValidator;
        private string _baseUrl;

        protected override void FinalizeSetUp()
        {
            _baseUrl = "http://example.org";
            _urlValidator = new UrlValidator();
        }

        [Test]
        public void ValidateUrl_UrlIsRelativeUrl_ConvertsToAbsoluteUrl()
        {
            SampleClass1 item = new SampleClass1 { Url = "/sitemap" };

            _urlValidator.ValidateUrls(item, _baseUrl);

            item.Url.Should().Be("http://example.org/sitemap");
        }

    }

    class SampleClass1
    {
        [Url]
        public string Url { get; set; }
    }
}