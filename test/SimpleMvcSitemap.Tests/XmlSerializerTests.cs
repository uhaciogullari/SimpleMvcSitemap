using System.Collections.Generic;
using FluentAssertions;
using SimpleMvcSitemap.Serialization;
using Xunit;

namespace SimpleMvcSitemap.Tests
{
    public class XmlSerializerTests : TestBase
    {
        private readonly IXmlSerializer serializer;
        private readonly TestDataBuilder testDataBuilder;

        public XmlSerializerTests()
        {
            serializer = new XmlSerializer();
            testDataBuilder = new TestDataBuilder();
        }

        [Fact]
        public void Serialize_SitemapModel()
        {
            SitemapModel sitemap = new SitemapModel(new List<SitemapNode> { new SitemapNode("abc"), new SitemapNode("def") });

            string result = serializer.Serialize(sitemap);

            result.Should().BeXmlEquivalent("sitemap.xml");
        }

        [Fact]
        public void Serialize_SitemapIndexModel()
        {
            SitemapIndexModel sitemapIndex = new SitemapIndexModel(new List<SitemapIndexNode>
            {
                new SitemapIndexNode { Url = "abc" },
                new SitemapIndexNode { Url = "def" }
            });

            string result = serializer.Serialize(sitemapIndex);

            result.Should().BeXmlEquivalent("sitemap-index.xml");
        }

        [Fact]
        public void Serialize_SitemapNode_RequiredProperties()
        {
            string result = SerializeSitemap(testDataBuilder.CreateSitemapNodeWithRequiredProperties());

            result.Should().BeXmlEquivalent("sitemap-node-required.xml");
        }

        [Fact]
        public void Serialize_SitemapNode_AllProperties()
        {
            string result = SerializeSitemap(testDataBuilder.CreateSitemapNodeWithAllProperties());

            result.Should().BeXmlEquivalent("sitemap-node-all.xml");
        }

        [Fact]
        public void Serialize_SitemapIndexNode_RequiredProperties()
        {
            string result = serializer.Serialize(testDataBuilder.CreateSitemapIndexNodeWithRequiredProperties());

            result.Should().BeXmlEquivalent("sitemap-index-node-required.xml");
        }

        [Fact]
        public void Serialize_SitemapIndexNode_AllProperties()
        {
            string result = serializer.Serialize(testDataBuilder.CreateSitemapIndexNodeWithAllProperties());

            result.Should().BeXmlEquivalent("sitemap-index-node-all.xml");
        }

        [Fact]
        public void Serialize_SitemapNode_ImageRequiredProperties()
        {
            string result = SerializeSitemap(testDataBuilder.CreateSitemapNodeWithImageRequiredProperties());

            result.Should().BeXmlEquivalent("sitemap-node-image-required.xml");
        }

        [Fact]
        public void Serialize_SitemapNode_ImageAllProperties()
        {
            string result = SerializeSitemap(testDataBuilder.CreateSitemapNodeWithImageAllProperties());

            result.Should().BeXmlEquivalent("sitemap-node-image-all.xml");
        }

        [Fact]
        public void Serialize_SitemapNode_VideoRequiredProperties()
        {
            string result = SerializeSitemap(testDataBuilder.CreateSitemapNodeWithVideoRequiredProperties());

            result.Should().BeXmlEquivalent("sitemap-node-video-required.xml");
        }

        [Fact]
        public void Serialize_SitemapNode_VideoAllProperties()
        {
            string result = SerializeSitemap(testDataBuilder.CreateSitemapNodeWithVideoAllProperties());

            result.Should().BeXmlEquivalent("sitemap-node-video-all.xml");
        }

        [Fact]
        public void Serialize_SitemapNode_NewsRequiredProperties()
        {
            string result = SerializeSitemap(testDataBuilder.CreateSitemapNodeWithNewsRequiredProperties());

            result.Should().BeXmlEquivalent("sitemap-node-news-required.xml");
        }

        [Fact]
        public void Serialize_SitemapNode_NewsAllProperties()
        {
            string result = SerializeSitemap(testDataBuilder.CreateSitemapNodeWithNewsAllProperties());

            result.Should().BeXmlEquivalent("sitemap-node-news-all.xml");
        }

        [Fact]
        public void Serialize_SitemapModel_AlternateLinks()
        {
            string result = serializer.Serialize(testDataBuilder.CreateSitemapWithTranslations());

            result.Should().BeXmlEquivalent("sitemap-alternate-links.xml");
        }

        [Fact]
        public void Serialize_SitemapModel_HasStyleSheets()
        {
            string result = serializer.Serialize(testDataBuilder.CreateSitemapWithSingleStyleSheet());

            result.Should().BeXmlEquivalent("sitemap-with stylesheets.xml");
        }

        private string SerializeSitemap(SitemapNode sitemapNode)
        {
            return serializer.Serialize(new SitemapModel(new List<SitemapNode> { sitemapNode }));
        }
    }
}