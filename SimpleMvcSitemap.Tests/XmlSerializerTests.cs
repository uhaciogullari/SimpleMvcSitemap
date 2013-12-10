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
            SitemapModel sitemapModel = new SitemapModel(new List<SitemapNode>
            {
                new SitemapNode {Url = "abc"},
                new SitemapNode {Url = "def"},
            });

            string result = _serializer.Serialize(sitemapModel);

            result.Should().Be("<?xml version=\"1.0\" encoding=\"utf-8\"?><urlset xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\"><url><loc>abc</loc></url><url><loc>def</loc></url></urlset>");
        }
    }
}