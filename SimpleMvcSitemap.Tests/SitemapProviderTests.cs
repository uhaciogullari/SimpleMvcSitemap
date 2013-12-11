using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace SimpleMvcSitemap.Tests
{
    [TestFixture]
    public class SitemapProviderTests
    {
        private ISitemapProvider _sitemapProvider;

        private Mock<IActionResultFactory> _actionResultFactory;
        private Mock<IBaseUrlProvider> _baseUrlProvider;
        private Mock<HttpContextBase> _httpContext;

        private EmptyResult _expectedResult;
        private string _baseUrl;

        [SetUp]
        public void Setup()
        {
            _actionResultFactory = new Mock<IActionResultFactory>(MockBehavior.Strict);
            _httpContext = new Mock<HttpContextBase>(MockBehavior.Strict);
            _baseUrlProvider = new Mock<IBaseUrlProvider>(MockBehavior.Strict);
            _sitemapProvider = new SitemapProvider(_actionResultFactory.Object,_baseUrlProvider.Object);

            _expectedResult = new EmptyResult();
            
            _baseUrl = "http://example.org";
            _baseUrlProvider.Setup(item => item.GetBaseUrl(_httpContext.Object)).Returns(_baseUrl);
        }

        [Test]
        public void CreateSitemap_SingleSitemapWithAbsoluteUrls()
        {
            string url = "http://notexample.org/abc";
            List<SitemapNode> sitemapNodes = new List<SitemapNode> { new SitemapNode(url) };

            _actionResultFactory.Setup(
                item => item.CreateXmlResult(It.Is<SitemapModel>(model => model.First().Url == url)))
                .Returns(_expectedResult);

            ActionResult result = _sitemapProvider.CreateSiteMap(_httpContext.Object, sitemapNodes);

            result.Should().Be(_expectedResult);
        }

        [Test]
        public void CreateSitemap_SingleSitemapWithRelativeUrls()
        {
            string url = "/relative";
            List<SitemapNode> sitemapNodes = new List<SitemapNode> { new SitemapNode(url) };

            Expression<Func<SitemapModel, bool>> validateNode =
                model => model.First().Url == "http://example.org/relative";

            _actionResultFactory.Setup(item => item.CreateXmlResult(It.Is(validateNode)))
                                .Returns(_expectedResult);

            ActionResult result = _sitemapProvider.CreateSiteMap(_httpContext.Object, sitemapNodes);

            result.Should().Be(_expectedResult);
        }

        [TearDown]
        public void Teardown()
        {
            _actionResultFactory.VerifyAll();
            _baseUrlProvider.VerifyAll();
            _httpContext.VerifyAll();
        }
    }
}