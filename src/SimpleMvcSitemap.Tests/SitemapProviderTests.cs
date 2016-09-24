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


        public SitemapProviderTests()
        {
            _sitemapProvider = new SitemapProvider();
        }


        [Fact]
        public void CreateSitemap_SitemapModelIsNull_ThrowsException()
        {
            Action act = () => _sitemapProvider.CreateSitemap((SitemapModel) null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void CreateSitemap_CreatesSitemapXmlResult()
        {
            List<SitemapNode> sitemapNodes = new List<SitemapNode> { new SitemapNode("/relative") };
            SitemapModel sitemapModel = new SitemapModel(sitemapNodes);

            ActionResult result = _sitemapProvider.CreateSitemap(sitemapModel);

            result.Should().BeOfType<XmlResult<SitemapModel>>();
        }




    }
}