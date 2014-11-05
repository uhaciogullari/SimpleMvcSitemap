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
                Images = new List<SitemapImage> 
                { 
                    new SitemapImage { Url = "u", Caption = "c", Location = "lo", Title = "t", License = "li"},
                    new SitemapImage { Url = "u2", Caption = "c2", Location = "lo2", Title = "t2", License = "li2"} 
                }
            };

            _namespaces.Add(Namespaces.ImagePrefix, Namespaces.Image);

            string result = _serializer.Serialize(sitemapNode);

            string expected = CreateXml("url",
                "<loc>abc</loc>" +
                    "<image:image>" +
                        "<image:loc>u</image:loc>" +
                        "<image:caption>c</image:caption>" +
                        "<image:geo_location>lo</image:geo_location>" +
                        "<image:title>t</image:title>" +
                        "<image:license>li</image:license>" +
                    "</image:image>" +
                    "<image:image>" +
                        "<image:loc>u2</image:loc>" +
                        "<image:caption>c2</image:caption>" +
                        "<image:geo_location>lo2</image:geo_location>" +
                        "<image:title>t2</image:title>" +
                        "<image:license>li2</image:license>" +
                    "</image:image>",
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


        [Test]
        public void Serialize_SitemapNewsNode()
        {
            SitemapNode sitemapNode = new SitemapNode("abc")
            {
                News = new SitemapNews
                {
                    Publication = new SitemapNewsPublication { Name = "The Example Times", Language = "en" },
                    Genres = "PressRelease, Blog",
                    PublicationDate = new DateTime(2014, 11, 5, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Companies A, B in Merger Talks",
                    Keywords = "business, merger, acquisition, A, B"
                }
            };

            _namespaces.Add(Namespaces.NewsPrefix, Namespaces.News);

            string result = _serializer.Serialize(sitemapNode);

            Console.WriteLine(result);

            string expected = CreateXml("url",
                "<loc>abc</loc>" +
                "<news:news>" +
                    "<news:publication>" +
                    "<news:name>The Example Times</news:name>" +
                    "<news:language>en</news:language>" +
                "</news:publication>" +
                "<news:genres>PressRelease, Blog</news:genres>" +
                "<news:publication_date>2014-11-05T00:00:00Z</news:publication_date>" +
                "<news:title>Companies A, B in Merger Talks</news:title>" +
                "<news:keywords>business, merger, acquisition, A, B</news:keywords>" +
                "</news:news>",
                "xmlns:news=\"http://www.google.com/schemas/sitemap-news/0.9\"");

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