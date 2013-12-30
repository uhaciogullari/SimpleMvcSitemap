using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace SimpleMvcSitemap.Tests
{
    public class XmlSerializerTests : TestBase
    {
        private IXmlSerializer _serializer;

        List<XmlSerializerNamespace> _namespaces;


        protected override void FinalizeSetUp()
        {
            _serializer = new XmlSerializer();
            _namespaces = new List<XmlSerializerNamespace>
                                       {
                                           new XmlSerializerNamespace
                                           {
                                               Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9",
                                               Prefix = "", 
                                           }
                                       };
        }

        [Test]
        public void Serialize_SitemapModel()
        {
            SitemapModel sitemap = new SitemapModel(new List<SitemapNode>
            {
                new SitemapNode { Url = "abc" },
                new SitemapNode { Url = "def" }
            });


            string result = _serializer.Serialize(sitemap, _namespaces);

            string expected = CreateXml("urlset", "<url><loc>abc</loc></url><url><loc>def</loc></url>");
            result.Should().Be(expected);
        }

        [Test]
        public void Serialize_SitemapIndexModel()
        {
            SitemapIndexModel sitemapIndex = new SitemapIndexModel(new List<SitemapIndexNode>
            {
                new SitemapIndexNode { Url = "abc" },
                new SitemapIndexNode { Url = "def" }
            });

            string result = _serializer.Serialize(sitemapIndex, _namespaces);

            string expected = CreateXml("sitemapindex", "<sitemap><loc>abc</loc></sitemap><sitemap><loc>def</loc></sitemap>");
            result.Should().Be(expected);
        }

        [Test]
        public void Serialize_SitemapNode()
        {
            SitemapNode sitemapNode = new SitemapNode("abc");

            string result = _serializer.Serialize(sitemapNode, _namespaces);

            result.Should().Be(CreateXml("url", "<loc>abc</loc>"));
        }

        [Test]
        public void Serialize_SitemapNodeWithLastModificationDate()
        {
            SitemapNode sitemapNode = new SitemapNode("abc")
            {
                LastModificationDate = new DateTime(2013, 12, 11, 16, 05, 00, DateTimeKind.Utc)
            };

            string result = _serializer.Serialize(sitemapNode, _namespaces);

            result.Should().Be(CreateXml("url", "<loc>abc</loc><lastmod>2013-12-11T16:05:00Z</lastmod>"));
        }

        [Test]
        public void Serialize_SitemapNodeWithChangeFrequency()
        {
            SitemapNode sitemapNode = new SitemapNode("abc")
            {
                ChangeFrequency = ChangeFrequency.Weekly
            };

            string result = _serializer.Serialize(sitemapNode, _namespaces);

            string expected = CreateXml("url", "<loc>abc</loc><changefreq>weekly</changefreq>");

            result.Should().Be(expected);
        }

        [Test]
        public void Serialize_SitemapNodeWithPriority()
        {
            SitemapNode sitemapNode = new SitemapNode("abc")
            {
                Priority = 0.8M
            };

            string result = _serializer.Serialize(sitemapNode, _namespaces);

            string expected = CreateXml("url", "<loc>abc</loc><priority>0.8</priority>");

            result.Should().Be(expected);
        }

        [Test]
        public void Serialize_SitemapNodeWithImageDefinition()
        {
            SitemapNode sitemapNode = new SitemapNode("abc")
            {
                Images = new List<SitemapImage> { new SitemapImage { Title = "title", Url = "url", Caption = "caption" },
                                                  new SitemapImage { Title = "title2", Url = "url2", Caption = "caption2" } }
            };

            _namespaces.Add(new XmlSerializerNamespace
            {
                Namespace = Namespaces.Image,
                Prefix = Namespaces.ImagePrefix
            });

            string result = _serializer.Serialize(sitemapNode, _namespaces);

            string expected = CreateXml("url",
                "<loc>abc</loc>" +
                "<image:image><image:caption>caption</image:caption><image:title>title</image:title><image:loc>url</image:loc></image:image>" +
                "<image:image><image:caption>caption2</image:caption><image:title>title2</image:title><image:loc>url2</image:loc></image:image>",
                "xmlns:image=\"http://www.google.com/schemas/sitemap-image/1.1\"");

            result.Should().Be(expected);
        }


        [Test]
        public void Serialize_SitemapIndexNode()
        {
            SitemapIndexNode sitemapIndexNode = new SitemapIndexNode { Url = "abc" };

            string result = _serializer.Serialize(sitemapIndexNode, _namespaces);

            string expected = CreateXml("sitemap", "<loc>abc</loc>");

            result.Should().Be(expected);
        }

        [Test]
        public void Serialize_SitemapIndexNodeWithLastModificationDate()
        {
            SitemapIndexNode sitemapIndexNode = new SitemapIndexNode
            {
                Url = "abc",
                LastModificationDate = new DateTime(2013, 12, 11, 16, 05, 00, DateTimeKind.Utc)
            };

            string result = _serializer.Serialize(sitemapIndexNode, _namespaces);

            string expected = CreateXml("sitemap", "<loc>abc</loc><lastmod>2013-12-11T16:05:00Z</lastmod>");

            result.Should().Be(expected);
        }


        private string CreateXml(string rootTagName, string content)
        {
            return string.Format(
                    "<?xml version=\"1.0\" encoding=\"utf-8\"?><{0} xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">{1}</{0}>", rootTagName, content);
        }

        private string CreateXml(string rootTagName, string content, string expectedNamespace)
        {
            return string.Format(
                    "<?xml version=\"1.0\" encoding=\"utf-8\"?><{1} {0} xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">{2}</{1}>", expectedNamespace, rootTagName, content);
        }
    }
}