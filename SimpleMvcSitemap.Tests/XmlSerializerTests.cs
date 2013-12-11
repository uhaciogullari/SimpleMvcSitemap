using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace SimpleMvcSitemap.Tests
{
    [TestFixture]
    public class XmlSerializerTests
    {
        private IXmlSerializer _serializer;

        [SetUp]
        public void Setup()
        {
            _serializer = new XmlSerializer();
        }

        [Test]
        public void SerializeSitemapModel()
        {
            SitemapModel sitemap = new SitemapModel(new List<SitemapNode>
            {
                new SitemapNode {Url = "abc"},
                new SitemapNode {Url = "def"},
            });

            string result = _serializer.Serialize(sitemap);

            result.Should().Be("<?xml version=\"1.0\" encoding=\"utf-8\"?><urlset xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\"><url><loc>abc</loc></url><url><loc>def</loc></url></urlset>");
        }

        [Test]
        public void SerializeSitemapIndexModelTest()
        {
            SitemapIndexModel sitemapIndex = new SitemapIndexModel(new List<SitemapIndexNode>
            {
                new SitemapIndexNode{Url = "abc"},
                new SitemapIndexNode{Url = "def"},
            });

            string result = _serializer.Serialize(sitemapIndex);

            result.Should().Be("<?xml version=\"1.0\" encoding=\"utf-8\"?><sitemapindex xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\"><sitemap><loc>abc</loc></sitemap><sitemap><loc>def</loc></sitemap></sitemapindex>");
        }
    }
}