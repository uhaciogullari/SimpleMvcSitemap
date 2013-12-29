using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace SimpleMvcSitemap.Tests
{
    public class SitemapProviderTests : TestBase
    {
        private ISitemapProvider _sitemapProvider;

        private Mock<IActionResultFactory> _actionResultFactory;
        private Mock<IBaseUrlProvider> _baseUrlProvider;

        private Mock<HttpContextBase> _httpContext;
        private Mock<ISitemapConfiguration> _config;
        private Mock<IXmlNamespaceResolver> _namespaceProviderMock;

        private EmptyResult _expectedResult;
        private string _baseUrl;


        protected override void FinalizeSetUp()
        {
            _actionResultFactory = MockFor<IActionResultFactory>();
            _baseUrlProvider = MockFor<IBaseUrlProvider>();
            _namespaceProviderMock = MockFor<IXmlNamespaceResolver>();
            _sitemapProvider = new SitemapProvider(_actionResultFactory.Object, _baseUrlProvider.Object, _namespaceProviderMock.Object);

            _httpContext = MockFor<HttpContextBase>();
            _config = MockFor<ISitemapConfiguration>();
            _baseUrl = "http://example.org";
            _expectedResult = new EmptyResult();
            _baseUrl = "http://example.org";
            _expectedResult = new EmptyResult();
        }

        private void GetBaseUrl()
        {
            _baseUrlProvider.Setup(item => item.GetBaseUrl(_httpContext.Object)).Returns(_baseUrl);
        }

        private void GetNamespaces()
        {
            var xmlSerializerNamespaces = FakeDataRepository.CreateMany<XmlSerializerNamespace>(1);
            _namespaceProviderMock.Setup(item => item.GetNamespaces(It.IsAny<IEnumerable<SitemapNode>>())).Returns(xmlSerializerNamespaces);
        }

        [Test]
        public void CreateSitemap_HttpContextIsNull_ThrowsException()
        {
            List<SitemapNode> sitemapNodes = new List<SitemapNode>();

            Assert.Throws<ArgumentNullException>(() => _sitemapProvider.CreateSitemap(null, sitemapNodes));
        }

        [Test]
        public void CreateSitemap_NodeListIsNull_DoesNotThrowException()
        {
            GetBaseUrl();
            GetNamespaces();
            _actionResultFactory.Setup(
                item => item.CreateXmlResult(It.Is<SitemapModel>(model => !model.Nodes.Any()), It.IsAny<IEnumerable<XmlSerializerNamespace>>()))
                                .Returns(_expectedResult);

            ActionResult result = _sitemapProvider.CreateSitemap(_httpContext.Object, (IEnumerable<SitemapNode>)null);

            result.Should().Be(_expectedResult);
        }

        [Test]
        public void CreateSitemap_SingleSitemapWithAbsoluteUrls()
        {
            GetBaseUrl();
            GetNamespaces();

            string url = "http://notexample.org/abc";
            List<SitemapNode> sitemapNodes = new List<SitemapNode> { new SitemapNode(url) };

            _actionResultFactory.Setup(
                item => item.CreateXmlResult(It.Is<SitemapModel>(model => model.Nodes.First().Url == url), It.IsAny<IEnumerable<XmlSerializerNamespace>>()))
                .Returns(_expectedResult);

            ActionResult result = _sitemapProvider.CreateSitemap(_httpContext.Object, sitemapNodes);

            result.Should().Be(_expectedResult);
        }

        [Test]
        public void CreateSitemap_SingleSitemapWithRelativeUrls()
        {
            GetBaseUrl();
            GetNamespaces();

            string url = "/relative";
            List<SitemapNode> sitemapNodes = new List<SitemapNode> { new SitemapNode(url) };

            Expression<Func<SitemapModel, bool>> validateNode =
                model => model.Nodes.First().Url == "http://example.org/relative";

            _actionResultFactory.Setup(item => item.CreateXmlResult(It.Is(validateNode), It.IsAny<IEnumerable<XmlSerializerNamespace>>()))
                                .Returns(_expectedResult);

            ActionResult result = _sitemapProvider.CreateSitemap(_httpContext.Object, sitemapNodes);

            result.Should().Be(_expectedResult);
        }



        [Test]
        public void CreateSitemapWithConfiguration_HttpContextIsNull_ThrowsException()
        {
            List<SitemapNode> sitemapNodes = new List<SitemapNode>();

            TestDelegate act = () => _sitemapProvider.CreateSitemap(null, sitemapNodes, _config.Object);
            Assert.Throws<ArgumentNullException>(act);
        }

        [Test]
        public void CreateSitemapWithConfiguration_ConfigurationIsNull_ThrowsException()
        {
            List<SitemapNode> sitemapNodes = new List<SitemapNode>();

            TestDelegate act = () => _sitemapProvider.CreateSitemap(_httpContext.Object, sitemapNodes, null);
            Assert.Throws<ArgumentNullException>(act);
        }

        [Test]
        public void CreateSitemapWithConfiguration_PageSizeIsBiggerThanNodeCount_CreatesSitemap()
        {
            GetBaseUrl();
            GetNamespaces();

            List<SitemapNode> sitemapNodes = new List<SitemapNode> { new SitemapNode("/relative") };
            _config.Setup(item => item.Size).Returns(5);

            _actionResultFactory.Setup(item => item.CreateXmlResult(It.IsAny<SitemapModel>(), It.IsAny<IEnumerable<XmlSerializerNamespace>>()))
                                .Returns(_expectedResult);

            ActionResult result = _sitemapProvider.CreateSitemap(_httpContext.Object, sitemapNodes, _config.Object);

            result.Should().Be(_expectedResult);
        }

        [TestCase(null)]
        [TestCase(0)]
        public void CreateSitemapWithConfiguration_NodeCountIsGreaterThanPageSize_CreatesIndex(int? currentPage)
        {
            GetBaseUrl();

            GetNamespaces();

            List<SitemapNode> sitemapNodes = FakeDataRepository.CreateMany<SitemapNode>(5).ToList();
            _config.Setup(item => item.Size).Returns(2);
            _config.Setup(item => item.CurrentPage).Returns(currentPage);
            _config.Setup(item => item.CreateSitemapUrl(It.Is<int>(i => i <= 3))).Returns(string.Empty);

            Expression<Func<SitemapIndexModel, bool>> validateIndex = index => index.Nodes.Count == 3;
            _actionResultFactory.Setup(item => item.CreateXmlResult(It.Is(validateIndex), It.IsAny<IEnumerable<XmlSerializerNamespace>>()))
                                .Returns(_expectedResult);


            //act
            ActionResult result = _sitemapProvider.CreateSitemap(_httpContext.Object, sitemapNodes, _config.Object);

            result.Should().Be(_expectedResult);
        }

        [Test]
        public void CreateSitemapWithConfiguration_AsksForSpecificPage_CreatesSitemap()
        {
            GetBaseUrl();
            GetNamespaces();

            List<SitemapNode> sitemapNodes = FakeDataRepository.CreateMany<SitemapNode>(5).ToList();
            _config.Setup(item => item.Size).Returns(2);
            _config.Setup(item => item.CurrentPage).Returns(3);

            Expression<Func<SitemapModel, bool>> validateSitemap = model => model.Nodes.Count == 1;
            _actionResultFactory.Setup(item => item.CreateXmlResult(It.Is(validateSitemap), It.IsAny<IEnumerable<XmlSerializerNamespace>>()))
                                .Returns(_expectedResult);


            //act
            ActionResult result = _sitemapProvider.CreateSitemap(_httpContext.Object, sitemapNodes, _config.Object);

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