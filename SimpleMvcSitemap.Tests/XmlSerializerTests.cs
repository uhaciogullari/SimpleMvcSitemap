using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace SimpleMvcSitemap.Tests
{
    public class XmlSerializerTests : TestBase
    {
        private IXmlSerializer _serializer;

        private Mock<IXmlNamespaceBuilder> _namespaceBuilder;

        XmlSerializerNamespaces _namespaces;

        protected override void FinalizeSetUp()
        {
            _namespaceBuilder = MockFor<IXmlNamespaceBuilder>();
            _serializer = new XmlSerializer(_namespaceBuilder.Object);

            _namespaces = new XmlSerializerNamespaces();
            _namespaces.Add(Namespaces.SitemapPrefix, Namespaces.Sitemap);
            _namespaceBuilder.Setup(item => item.Create(It.IsAny<IEnumerable<string>>())).Returns(_namespaces);
        }

        [Test]
        public void Serialize_SitemapModel()
        {
            SitemapModel sitemap = new SitemapModel(new List<SitemapNode>
            {
                new SitemapNode { Url = "abc" },
                new SitemapNode { Url = "def" }
            });


            string result = _serializer.Serialize(sitemap);

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

            string result = _serializer.Serialize(sitemapIndex);

            string expected = CreateXml("sitemapindex", "<sitemap><loc>abc</loc></sitemap><sitemap><loc>def</loc></sitemap>");
            result.Should().Be(expected);
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

            string result = _serializer.Serialize(sitemapNode);

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

            _namespaces.Add(Namespaces.ImagePrefix, Namespaces.Image);

            string result = _serializer.Serialize(sitemapNode);

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

            string result = _serializer.Serialize(sitemapIndexNode);

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

            string result = _serializer.Serialize(sitemapIndexNode);

            string expected = CreateXml("sitemap", "<loc>abc</loc><lastmod>2013-12-11T16:05:00Z</lastmod>");

            result.Should().Be(expected);
        }


        private string CreateXml(string rootTagName, string content, string additionalNamespace = null)
        {
            additionalNamespace = additionalNamespace != null
                                     ? string.Concat(" ", additionalNamespace)
                                     : string.Empty;

            //namespace ordering is not consistent http://bit.ly/1cPAkid
            return string.Format(
                    "<?xml version=\"1.0\" encoding=\"utf-8\"?><{0}{1} xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">{2}</{0}>",
                    rootTagName, additionalNamespace, content);
        }

    }
}