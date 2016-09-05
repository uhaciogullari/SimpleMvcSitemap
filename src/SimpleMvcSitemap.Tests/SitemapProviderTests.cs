using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Xunit.Extensions;

namespace SimpleMvcSitemap.Tests
{
    public class SitemapProviderTests : TestBase
    {
        private ISitemapProvider _sitemapProvider;

        private Mock<ISitemapActionResultFactory> _actionResultFactory;

        private Mock<HttpContext> _httpContext;
        private Mock<ISitemapConfiguration<SampleData>> _config;

        private EmptyResult _expectedResult;

        public SitemapProviderTests()
        {
            _actionResultFactory = MockFor<ISitemapActionResultFactory>();
            _sitemapProvider = new SitemapProvider(_actionResultFactory.Object);

            _httpContext = MockFor<HttpContext>();
            _config = MockFor<ISitemapConfiguration<SampleData>>();
            _expectedResult = new EmptyResult();
        }


        [Fact]
        public void CreateSitemap_HttpContextIsNull_ThrowsException()
        {
            Action act = () => _sitemapProvider.CreateSitemap(null, new List<SitemapNode>());

            act.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void CreateSitemap_NodeListIsNull_DoesNotThrowException()
        {
            _actionResultFactory.Setup(item => item.CreateSitemapResult(_httpContext.Object, It.Is<SitemapModel>(model => !model.Nodes.Any()))).Returns(_expectedResult);

            ActionResult result = _sitemapProvider.CreateSitemap(_httpContext.Object, (IEnumerable<SitemapNode>)null);

            result.Should().Be(_expectedResult);
        }

        [Fact]
        public void CreateSitemap_SingleSitemap()
        {
            List<SitemapNode> sitemapNodes = new List<SitemapNode> { new SitemapNode("/relative") };

            Expression<Func<SitemapModel, bool>> validateSitemap = model => model.Nodes.SequenceEqual(sitemapNodes);
            _actionResultFactory.Setup(item => item.CreateSitemapResult(_httpContext.Object, It.Is(validateSitemap))).Returns(_expectedResult);

            ActionResult result = _sitemapProvider.CreateSitemap(_httpContext.Object, sitemapNodes);

            result.Should().Be(_expectedResult);
        }


        [Fact]
        public void CreateSitemapWithConfiguration_HttpContextIsNull_ThrowsException()
        {
            FakeDataSource dataSource = new FakeDataSource();

            Action act = () => _sitemapProvider.CreateSitemap(null, dataSource, _config.Object);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void CreateSitemapWithConfiguration_ConfigurationIsNull_ThrowsException()
        {
            IQueryable<SitemapNode> sitemapNodes = new List<SitemapNode>().AsQueryable();

            Action act = () => _sitemapProvider.CreateSitemap(_httpContext.Object, sitemapNodes, null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void CreateSitemapWithConfiguration_PageSizeIsBiggerThanNodeCount_CreatesSitemap()
        {
            var sitemapNodes = new FakeDataSource(CreateSampleData()).WithCount(1);
            _config.Setup(item => item.Size).Returns(5);

            _config.Setup(item => item.CreateNode(It.IsAny<SampleData>())).Returns(new SitemapNode());
            _actionResultFactory.Setup(item => item.CreateSitemapResult(_httpContext.Object, It.IsAny<SitemapModel>())).Returns(_expectedResult);

            ActionResult result = _sitemapProvider.CreateSitemap(_httpContext.Object, sitemapNodes, _config.Object);

            result.Should().Be(_expectedResult);
            sitemapNodes.TakenItemCount.Should().NotHaveValue();
            sitemapNodes.SkippedItemCount.Should().NotHaveValue();
        }

        [Theory]
        [InlineData(null)]
        [InlineData(0)]
        public void CreateSitemapWithConfiguration_NodeCountIsGreaterThanPageSize_CreatesIndex(int? currentPage)
        {
            FakeDataSource datas = new FakeDataSource().WithCount(5).WithEnumerationDisabled();
            _config.Setup(item => item.Size).Returns(2);
            _config.Setup(item => item.CurrentPage).Returns(currentPage);
            _config.Setup(item => item.CreateSitemapUrl(It.Is<int>(i => i <= 3))).Returns(string.Empty);

            Expression<Func<SitemapIndexModel, bool>> validateIndex = index => index.Nodes.Count == 3;
            _actionResultFactory.Setup(item => item.CreateSitemapResult(_httpContext.Object, It.Is(validateIndex))).Returns(_expectedResult);


            ActionResult result = _sitemapProvider.CreateSitemap(_httpContext.Object, datas, _config.Object);

            result.Should().Be(_expectedResult);
            datas.SkippedItemCount.Should().NotHaveValue();
            datas.TakenItemCount.Should().NotHaveValue();
        }

        [Fact]
        public void CreateSitemapWithConfiguration_AsksForSpecificPage_CreatesSitemap()
        {
            FakeDataSource datas = new FakeDataSource(CreateSampleData()).WithCount(5);

            _config.Setup(item => item.Size).Returns(2);
            _config.Setup(item => item.CurrentPage).Returns(2);
            _config.Setup(item => item.CreateNode(It.IsAny<SampleData>())).Returns(new SitemapNode());
            _actionResultFactory.Setup(item => item.CreateSitemapResult(_httpContext.Object, It.IsAny<SitemapModel>())).Returns(_expectedResult);

            ActionResult result = _sitemapProvider.CreateSitemap(_httpContext.Object, datas, _config.Object);

            result.Should().Be(_expectedResult);
            datas.TakenItemCount.Should().Be(2);
            datas.SkippedItemCount.Should().Be(2);
        }



        [Fact]
        public void CreateSitemapWithIndexNodes_HttpContextIsNull_ThrowsException()
        {
            List<SitemapIndexNode> sitemapIndexNodes = new List<SitemapIndexNode>();

            Action act = () => _sitemapProvider.CreateSitemap(null, sitemapIndexNodes);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void CreateSitemapWithIndexNodes()
        {
            List<SitemapIndexNode> sitemapIndexNodes = new List<SitemapIndexNode> { new SitemapIndexNode("/relative") };
            _actionResultFactory.Setup(item => item.CreateSitemapResult(_httpContext.Object, It.Is<SitemapIndexModel>(model => model.Nodes.SequenceEqual(sitemapIndexNodes))))
                                .Returns(_expectedResult);

            ActionResult result = _sitemapProvider.CreateSitemap(_httpContext.Object, sitemapIndexNodes);

            result.Should().Be(_expectedResult);
        }

        private IEnumerable<SampleData> CreateSampleData(int count = 3)
        {
            return Enumerable.Range(1, count).Select(i => new SampleData { Title = i.ToString() });
        }

    }
}