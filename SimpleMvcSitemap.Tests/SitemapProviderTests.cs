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
    public class SitemapProviderTests : TestBase
    {
        private ISitemapProvider _sitemapProvider;

        private Mock<IActionResultFactory> _actionResultFactory;
        private Mock<IBaseUrlProvider> _baseUrlProvider;

        private Mock<HttpContextBase> _httpContext;
        private Mock<ISitemapConfiguration<SampleData>> _config;

        private EmptyResult _expectedResult;
        private string _baseUrl;


        protected override void FinalizeSetUp()
        {
            _actionResultFactory = MockFor<IActionResultFactory>();
            _baseUrlProvider = MockFor<IBaseUrlProvider>();
            _sitemapProvider = new SitemapProvider(_actionResultFactory.Object, _baseUrlProvider.Object);

            _httpContext = MockFor<HttpContextBase>();
            _config = MockFor<ISitemapConfiguration<SampleData>>();
            _baseUrl = "http://example.org";
            _expectedResult = new EmptyResult();
        }

        private void GetBaseUrl()
        {
            _baseUrlProvider.Setup(item => item.GetBaseUrl(_httpContext.Object)).Returns(_baseUrl);
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
            _actionResultFactory.Setup(
                item => item.CreateXmlResult(It.Is<SitemapModel>(model => !model.Nodes.Any())))
                                .Returns(_expectedResult);

            ActionResult result = _sitemapProvider.CreateSitemap(_httpContext.Object, (IEnumerable<SitemapNode>)null);

            result.Should().Be(_expectedResult);
        }

        [Test]
        public void CreateSitemap_SingleSitemapWithAbsoluteUrls()
        {
            GetBaseUrl();

            string url = "http://notexample.org/abc";
            List<SitemapNode> sitemapNodes = new List<SitemapNode> { new SitemapNode(url) };

            _actionResultFactory.Setup(
                item => item.CreateXmlResult(It.Is<SitemapModel>(model => model.Nodes.First().Url == url)))
                .Returns(_expectedResult);

            ActionResult result = _sitemapProvider.CreateSitemap(_httpContext.Object, sitemapNodes);

            result.Should().Be(_expectedResult);
        }

        [Test]
        public void CreateSitemap_SingleSitemapWithRelativeUrls()
        {
            GetBaseUrl();

            string url = "/relative";
            List<SitemapNode> sitemapNodes = new List<SitemapNode> { new SitemapNode(url) };

            Expression<Func<SitemapModel, bool>> validateNode =
                model => model.Nodes.First().Url == "http://example.org/relative";

            _actionResultFactory.Setup(item => item.CreateXmlResult(It.Is(validateNode)))
                                .Returns(_expectedResult);

            ActionResult result = _sitemapProvider.CreateSitemap(_httpContext.Object, sitemapNodes);

            result.Should().Be(_expectedResult);
        }

        [Test]
        public void CreateSitemap_SingleSitemapWithAbsoluteUrls_ImageTagWithRelativeUrl()
        {
            GetBaseUrl();

            List<SitemapNode> sitemapNodes = new List<SitemapNode>
            {
                new SitemapNode("http://example.org/sitemap")
                {
                    Images = new List<SitemapImage> {new SitemapImage("/image.png")}
                }
            };

            Expression<Func<SitemapModel, bool>> validateNode =
                model => model.Nodes.First().Images.First().Url == "http://example.org/image.png";

            _actionResultFactory.Setup(item => item.CreateXmlResult(It.Is(validateNode)))
                                .Returns(_expectedResult);

            ActionResult result = _sitemapProvider.CreateSitemap(_httpContext.Object, sitemapNodes);

            result.Should().Be(_expectedResult);
        }


        
        [Test]
        public void CreateSitemapWithConfiguration_HttpContextIsNull_ThrowsException()
        {
            FakeDataSource dataSource = new FakeDataSource();

            TestDelegate act = () => _sitemapProvider.CreateSitemap(null, dataSource, _config.Object);

            Assert.Throws<ArgumentNullException>(act);
        }

        [Test]
        public void CreateSitemapWithConfiguration_ConfigurationIsNull_ThrowsException()
        {
            IQueryable<SitemapNode> sitemapNodes = new List<SitemapNode>().AsQueryable();

            TestDelegate act = () => _sitemapProvider.CreateSitemap(_httpContext.Object, sitemapNodes, null);

            Assert.Throws<ArgumentNullException>(act);
        }

        [Test]
        public void CreateSitemapWithConfiguration_PageSizeIsBiggerThanNodeCount_CreatesSitemap()
        {
            GetBaseUrl();

            var sitemapNodes = new FakeDataSource().WithCount(1);
            _config.Setup(item => item.Size).Returns(5);

            _actionResultFactory.Setup(item => item.CreateXmlResult(It.IsAny<SitemapModel>()))
                                .Returns(_expectedResult);

            ActionResult result = _sitemapProvider.CreateSitemap(_httpContext.Object, sitemapNodes, _config.Object);

            result.Should().Be(_expectedResult);
            sitemapNodes.TakenItemCount.Should().NotHaveValue();
            sitemapNodes.SkippedItemCount.Should().NotHaveValue();
        }

        [TestCase(null)]
        [TestCase(0)]
        public void CreateSitemapWithConfiguration_NodeCountIsGreaterThanPageSize_CreatesIndex(int? currentPage)
        {
            GetBaseUrl();

            FakeDataSource datas = new FakeDataSource().WithCount(5).WithEnumerationDisabled();
            _config.Setup(item => item.Size).Returns(2);
            _config.Setup(item => item.CurrentPage).Returns(currentPage);
            _config.Setup(item => item.CreateSitemapUrl(It.Is<int>(i => i <= 3))).Returns(string.Empty);

            Expression<Func<SitemapIndexModel, bool>> validateIndex = index => index.Nodes.Count == 3;
            _actionResultFactory.Setup(item => item.CreateXmlResult(It.Is(validateIndex))).Returns(_expectedResult);


            ActionResult result = _sitemapProvider.CreateSitemap(_httpContext.Object, datas, _config.Object);

            result.Should().Be(_expectedResult);
            datas.SkippedItemCount.Should().NotHaveValue();
            datas.TakenItemCount.Should().NotHaveValue();
        }

        [Test]
        public void CreateSitemapWithConfiguration_AsksForSpecificPage_CreatesSitemap()
        {
            GetBaseUrl();

            FakeDataSource datas = new FakeDataSource().WithCount(5);

            _config.Setup(item => item.Size).Returns(2);
            _config.Setup(item => item.CurrentPage).Returns(2);

            _actionResultFactory.Setup(item => item.CreateXmlResult(It.IsAny<SitemapModel>())).Returns(_expectedResult);


            ActionResult result = _sitemapProvider.CreateSitemap(_httpContext.Object, datas, _config.Object);

            result.Should().Be(_expectedResult);
            datas.TakenItemCount.Should().Be(2);
            datas.SkippedItemCount.Should().Be(2);
        }



        [Test]
        public void CreateSitemapWithIndexNodes_HttpContextIsNull_ThrowsException()
        {
            List<SitemapIndexNode> sitemapIndexNodes = new List<SitemapIndexNode>();

            TestDelegate act = () => _sitemapProvider.CreateSitemap(null, sitemapIndexNodes);

            Assert.Throws<ArgumentNullException>(act);
        }

        [Test]
        public void CreateSitemapWithIndexNodes_IndexWithRelativeUrls()
        {
            GetBaseUrl();
            List<SitemapIndexNode> sitemapIndexNodes = new List<SitemapIndexNode>
            {
                new SitemapIndexNode("/relative")
            };
            _actionResultFactory.Setup(
                item => item.CreateXmlResult(It.Is<SitemapIndexModel>(model =>
                    model.Nodes.First().Url == "http://example.org/relative"))).Returns(_expectedResult);

            ActionResult result = _sitemapProvider.CreateSitemap(_httpContext.Object, sitemapIndexNodes);

            result.Should().Be(_expectedResult);
        }


    }
}