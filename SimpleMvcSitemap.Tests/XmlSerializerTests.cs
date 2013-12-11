using System;
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
        public void Serialize_SitemapModel()
        {
            SitemapModel sitemap = new SitemapModel(new List<SitemapNode>
            {
                new SitemapNode {Url = "abc"},
                new SitemapNode {Url = "def"},
            });

            string result = _serializer.Serialize(sitemap);

            result.Should().Be(CreateXml("urlset",
                               "<url><loc>abc</loc></url><url><loc>def</loc></url>"));
        }

        [Test]
        public void Serialize_SitemapIndexModel()
        {
            SitemapIndexModel sitemapIndex = new SitemapIndexModel(new List<SitemapIndexNode>
            {
                new SitemapIndexNode{Url = "abc"},
                new SitemapIndexNode{Url = "def"},
            });

            string result = _serializer.Serialize(sitemapIndex);

            result.Should().Be(CreateXml("sitemapindex",
                               "<sitemap><loc>abc</loc></sitemap><sitemap><loc>def</loc></sitemap>"));
        }

        [Test]
        public void Serialize_SitemapNode()
        {
            SitemapNode sitemapNode = new SitemapNode("abc");

            string result = _serializer.Serialize(sitemapNode);

            result.Should().Be(CreateXml("url", "<loc>abc</loc>"));
        }

        [Test]
        public void Serialize_SitemapNodeWithLastModificationDate()
        {
            SitemapNode sitemapNode = new SitemapNode("abc")
            {
                LastModificationDate = new DateTime(2013, 12, 11, 16, 05, 00, DateTimeKind.Utc)
            };

            string result = _serializer.Serialize(sitemapNode);

            result.Should().Be(CreateXml("url", "<loc>abc</loc><lastmod>2013-12-11T16:05:00Z</lastmod>"));
        }

        [Test]
        public void Serialize_SitemapNodeWithChangeFrequency()
        {
            SitemapNode sitemapNode = new SitemapNode("abc")
            {
                ChangeFrequency = ChangeFrequency.Weekly
            };

            string result = _serializer.Serialize(sitemapNode);

            result.Should().Be(CreateXml("url", "<loc>abc</loc><changefreq>weekly</changefreq>"));
        }

        [Test]
        public void Serialize_SitemapNodeWithPriority()
        {
            SitemapNode sitemapNode = new SitemapNode("abc")
            {
                Priority = 0.8M
            };

            string result = _serializer.Serialize(sitemapNode);

            result.Should().Be(CreateXml("url", "<loc>abc</loc><priority>0.8</priority>"));
        }

        [Test]
        public void Serialize_SitemapIndexNode()
        {
            SitemapIndexNode sitemapIndexNode = new SitemapIndexNode { Url = "abc" };

            string result = _serializer.Serialize(sitemapIndexNode);

            result.Should().Be(CreateXml("sitemap", "<loc>abc</loc>"));
        }

        [Test]
        public void Serialize_SitemapIndexNodeWithLastModificationDate()
        {
            SitemapIndexNode sitemapIndexNode = new SitemapIndexNode
            {
                Url = "abc",
                LastModificationDate = new DateTime(2013, 12, 11, 16, 05, 00, DateTimeKind.Utc)
            };

            string result = _serializer.Serialize(sitemapIndexNode);

            result.Should().Be(CreateXml("sitemap", "<loc>abc</loc><lastmod>2013-12-11T16:05:00Z</lastmod>"));
        }


        private string CreateXml(string rootTagName, string content)
        {
            return string.Format(
                    "<?xml version=\"1.0\" encoding=\"utf-8\"?><{0} xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">{1}</{0}>", rootTagName, content);
        }


    }
}