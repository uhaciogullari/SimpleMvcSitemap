#if Mvc
using System.Web.Mvc;
#endif

#if CoreMvc
using Microsoft.AspNetCore.Mvc;
# endif

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
        private readonly DynamicSitemapIndexProvider dynamicSitemapIndexProvider;
        private readonly Mock<ISitemapProvider> sitemapProvider;
        private readonly Mock<ISitemapIndexConfiguration<SampleData>> sitemapIndexConfiguration;
        private ActionResult expectedResult;

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

            act.ShouldThrow<ArgumentNullException>();
        }


        [Fact]
        public void CreateSitemapIndex_SitemapIndexConfigurationIsNull_ThrowsException()
        {
            Action act = () => dynamicSitemapIndexProvider.CreateSitemapIndex<SampleData>(sitemapProvider.Object, null);

            act.ShouldThrow<ArgumentNullException>();
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
            sitemapIndexConfiguration.Setup(item => item.CurrentPage).Returns(currentPage);
            sitemapIndexConfiguration.Setup(item => item.Size).Returns(2);
            sitemapIndexConfiguration.Setup(item => item.CreateSitemapIndexNode(It.Is<int>(i => i <= 3))).Returns(new SitemapIndexNode());
            FakeDataSource sampleData = new FakeDataSource().WithCount(5).WithEnumerationDisabled();
            SetStyleSheets(StyleSheetType.SitemapIndex);


            CreateSitemapIndex().Should().Be(expectedResult);
            sampleData.SkippedItemCount.Should().NotHaveValue();
            sampleData.TakenItemCount.Should().NotHaveValue();
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