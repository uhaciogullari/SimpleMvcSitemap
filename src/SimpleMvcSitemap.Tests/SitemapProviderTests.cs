using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace SimpleMvcSitemap.Tests
{
    public class SitemapProviderTests : TestBase
    {
        private readonly ISitemapProvider _sitemapProvider;

        private readonly Mock<ISitemapConfiguration<SampleData>> _config;

        private readonly EmptyResult _expectedResult;

        public SitemapProviderTests()
        {
            _sitemapProvider = new SitemapProvider();

            _config = MockFor<ISitemapConfiguration<SampleData>>();
            _expectedResult = new EmptyResult();
        }


        [Fact]
        public void CreateSitemap_SitemapModelIsNull_ThrowsException()
        {
            Action act = () => _sitemapProvider.CreateSitemap((SitemapModel) null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void CreateSitemap_SingleSitemap()
        {
            List<SitemapNode> sitemapNodes = new List<SitemapNode> { new SitemapNode("/relative") };
            SitemapModel sitemapModel = new SitemapModel(sitemapNodes);

            ActionResult result = _sitemapProvider.CreateSitemap(sitemapModel);

            result.Should().BeOfType<XmlResult<SitemapModel>>();
        }


        [Fact]
        public void CreateSitemapWithConfiguration_ConfigurationIsNull_ThrowsException()
        {
            IQueryable<SitemapNode> sitemapNodes = new List<SitemapNode>().AsQueryable();

            Action act = () => _sitemapProvider.CreateSitemap(sitemapNodes, null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void CreateSitemapWithConfiguration_PageSizeIsBiggerThanNodeCount_CreatesSitemap()
        {
            var sitemapNodes = new FakeDataSource(CreateSampleData()).WithCount(1);
            _config.Setup(item => item.Size).Returns(5);

            ActionResult result = _sitemapProvider.CreateSitemap(sitemapNodes, _config.Object);

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


            ActionResult result = _sitemapProvider.CreateSitemap(datas, _config.Object);

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

            ActionResult result = _sitemapProvider.CreateSitemap(datas, _config.Object);

            result.Should().Be(_expectedResult);
            datas.TakenItemCount.Should().Be(2);
            datas.SkippedItemCount.Should().Be(2);
        }


        [Fact]
        public void CreateSitemapWithIndexNodes()
        {
            List<SitemapIndexNode> sitemapIndexNodes = new List<SitemapIndexNode> { new SitemapIndexNode("/relative") };

            ActionResult result = _sitemapProvider.CreateSitemap(sitemapIndexNodes);

            result.Should().Be(_expectedResult);
        }

        private IEnumerable<SampleData> CreateSampleData(int count = 3)
        {
            return Enumerable.Range(1, count).Select(i => new SampleData { Title = i.ToString() });
        }

    }
}