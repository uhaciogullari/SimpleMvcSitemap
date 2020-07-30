using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using SimpleMvcSitemap.StyleSheets;
using Xunit;

namespace SimpleMvcSitemap.Tests
{
    public class DynamicSitemapIndexProviderTests : TestBase
    {
        private readonly IDynamicSitemapIndexProvider dynamicSitemapIndexProvider;
        private readonly Mock<ISitemapProvider> sitemapProvider;
        private readonly Mock<ISitemapIndexConfiguration<SampleData>> sitemapIndexConfiguration;
        private readonly ActionResult expectedResult;

        public DynamicSitemapIndexProviderTests()
        {
            dynamicSitemapIndexProvider = new DynamicSitemapIndexProvider();
            sitemapProvider = MockFor<ISitemapProvider>();
            sitemapIndexConfiguration = MockFor<ISitemapIndexConfiguration<SampleData>>();
            expectedResult = new EmptyResult();
        }

        [Fact]
        public void CreateSitemapIndex_SitemapProviderIsNull_ThrowsException()
        {
            Action act = () => dynamicSitemapIndexProvider.CreateSitemapIndex(null, sitemapIndexConfiguration.Object);

            act.Should().Throw<ArgumentNullException>();
        }


        [Fact]
        public void CreateSitemapIndex_SitemapIndexConfigurationIsNull_ThrowsException()
        {
            Action act = () => dynamicSitemapIndexProvider.CreateSitemapIndex<SampleData>(sitemapProvider.Object, null);

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void CreateSitemapIndex_PageSizeIsBiggerThanTheNodeCount_CreatesSitemap()
        {
            sitemapIndexConfiguration.Setup(configuration => configuration.Size).Returns(10);

            int itemCount = 5;
            var sampleData = CreateSampleData(itemCount);
            CreateFakeDataSource().WithCount(itemCount).WithItemsToBeEnumerated(sampleData);

            sitemapIndexConfiguration.Setup(configuration => configuration.CreateNode(It.IsAny<SampleData>()))
                                     .Returns(new SitemapNode("abc"));

            SetStyleSheets(StyleSheetType.Sitemap);

            sitemapProvider.Setup(provider => provider.CreateSitemap(It.Is<SitemapModel>(model => model.Nodes.Count == itemCount)))
                           .Returns(expectedResult);

            CreateSitemapIndex().Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(0)]
        public void CreateSitemapIndex_NodeCountIsGreaterThanPageSize_CreatesIndex(int? currentPage)
        {
            var sampleData = CreateFakeDataSource().WithCount(5).WithEnumerationDisabled();

            sitemapIndexConfiguration.Setup(item => item.Size).Returns(2);
            sitemapIndexConfiguration.Setup(item => item.CurrentPage).Returns(currentPage);
            UseReverseOrderingForSitemapIndexNodes(false);

            SetExpectedSitemapIndexNodeParameters(1, 2, 3);


            SetStyleSheets(StyleSheetType.SitemapIndex);

            sitemapProvider.Setup(provider => provider.CreateSitemapIndex(It.Is<SitemapIndexModel>(model => model.Nodes.Count == 3)))
                .Returns(expectedResult);


            CreateSitemapIndex().Should().Be(expectedResult);
            sampleData.SkippedItemCount.Should().NotHaveValue();
            sampleData.TakenItemCount.Should().NotHaveValue();
        }

        [Fact]
        public void CreateSitemapIndex_NodeCountIsGreaterThanPageSize_ReverseOrderingEnabled_CreatesIndex()
        {
            CreateFakeDataSource().WithCount(5).WithEnumerationDisabled();
            sitemapIndexConfiguration.Setup(item => item.Size).Returns(2);
            sitemapIndexConfiguration.Setup(item => item.CurrentPage).Returns((int?)null);
            UseReverseOrderingForSitemapIndexNodes();
            SetExpectedSitemapIndexNodeParameters(3, 2, 1);
            SetStyleSheets(StyleSheetType.SitemapIndex);

            sitemapProvider.Setup(provider => provider.CreateSitemapIndex(It.IsAny<SitemapIndexModel>())).Returns(expectedResult);

            CreateSitemapIndex().Should().Be(expectedResult);
        }

        [Fact]
        public void CreateSitemapIndex_AsksForSpecificPage_CreatesSitemap()
        {
            var fakeDataSource = CreateFakeDataSource().WithCount(5).WithItemsToBeEnumerated(CreateSampleData());

            sitemapIndexConfiguration.Setup(item => item.Size).Returns(2);
            sitemapIndexConfiguration.Setup(item => item.CurrentPage).Returns(2);
            sitemapIndexConfiguration.Setup(item => item.CreateNode(It.IsAny<SampleData>())).Returns(new SitemapNode());
            sitemapProvider.Setup(provider => provider.CreateSitemap(It.Is<SitemapModel>(model => model.Nodes.Count == 3)))
                            .Returns(expectedResult);
            SetStyleSheets(StyleSheetType.Sitemap);

            CreateSitemapIndex().Should().Be(expectedResult);

            fakeDataSource.TakenItemCount.Should().Be(2);
            fakeDataSource.SkippedItemCount.Should().Be(2);
        }

        private ActionResult CreateSitemapIndex()
        {
            return dynamicSitemapIndexProvider.CreateSitemapIndex(sitemapProvider.Object, sitemapIndexConfiguration.Object);
        }

        private FakeDataSource CreateFakeDataSource()
        {
            FakeDataSource fakeDataSource = new FakeDataSource();
            sitemapIndexConfiguration.Setup(configuration => configuration.DataSource).Returns(fakeDataSource);
            return fakeDataSource;
        }

        private List<SampleData> CreateSampleData(int count = 3)
        {
            return Enumerable.Range(1, count).Select(i => new SampleData { Title = i.ToString() }).ToList();
        }

        private void SetExpectedSitemapIndexNodeParameters(params int[] expectedParameters)
        {
            var queue = new Queue<int>(expectedParameters);
            sitemapIndexConfiguration.Setup(item => item.CreateSitemapIndexNode(It.IsAny<int>()))
                                     .Returns(new SitemapIndexNode())
                                     .Callback((int i) => i.Should().Be(queue.Dequeue()));
        }

        private void UseReverseOrderingForSitemapIndexNodes(bool value = true)
        {
            sitemapIndexConfiguration.Setup(configuration => configuration.UseReverseOrderingForSitemapIndexNodes).Returns(value);
        }

        private void SetStyleSheets(StyleSheetType styleSheetType, List<XmlStyleSheet> styleSheets = null)
        {
            var setup = styleSheetType == StyleSheetType.Sitemap
                    ? sitemapIndexConfiguration.Setup(configuration => configuration.SitemapStyleSheets)
                    : sitemapIndexConfiguration.Setup(configuration => configuration.SitemapIndexStyleSheets);

            setup.Returns(styleSheets);
        }

        private enum StyleSheetType
        {
            Sitemap, SitemapIndex
        }

    }
}