using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace SimpleMvcSitemap.Tests
{
    public class SitemapProviderTests : TestBase
    {
        private readonly ISitemapProvider sitemapProvider;


        public SitemapProviderTests()
        {
            sitemapProvider = new SitemapProvider();
        }


        [Fact]
        public void CreateSitemap_SitemapModelIsNull_ThrowsException()
        {
            Action act = () => sitemapProvider.CreateSitemap(null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void CreateSitemap_CreatesSitemapXmlResult()
        {
            List<SitemapNode> sitemapNodes = new List<SitemapNode> { new SitemapNode("/relative") };
            SitemapModel sitemapModel = new SitemapModel(sitemapNodes);

            var result = sitemapProvider.CreateSitemap(sitemapModel);

            result.Should().BeOfType<XmlResult<SitemapModel>>();
        }

        [Fact]
        public void CreateSitemapIndex_SitemapIndexModelIsNull_ThrowsException()
        {
            Action act = () => sitemapProvider.CreateSitemapIndex(null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void CreateSitemapIndex_CreatesSitemapIndexXmlResult()
        {
            List<SitemapIndexNode> indexNodes = new List<SitemapIndexNode> { new SitemapIndexNode("/relative") };
            SitemapIndexModel sitemapIndexModel = new SitemapIndexModel(indexNodes);

            var result = sitemapProvider.CreateSitemapIndex(sitemapIndexModel);

            result.Should().BeOfType<XmlResult<SitemapIndexModel>>();
        }
    }
}