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

            result.Should().BeXmlEquivalent("Samples/sitemap-model-1.xml");
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

            result.Should().BeXmlEquivalent("Samples/sitemap-index-model-1.xml");
        }

        [Test]
        public void Serialize_SitemapNode()
        {
            SitemapNode sitemapNode = new SitemapNode("abc");

            string result = _serializer.Serialize(sitemapNode);

            result.Should().BeXmlEquivalent("Samples/sitemap-node-1.xml");
        }

        [Test]
        public void Serialize_SitemapNodeWithLastModificationDate()
        {
            SitemapNode sitemapNode = new SitemapNode("abc")
            {
                LastModificationDate = new DateTime(2013, 12, 11, 16, 05, 00, DateTimeKind.Utc)
            };

            string result = _serializer.Serialize(sitemapNode);

            result.Should().BeXmlEquivalent("Samples/sitemap-node-2.xml");
        }

        [Test]
        public void Serialize_SitemapNodeWithChangeFrequency()
        {
            SitemapNode sitemapNode = new SitemapNode("abc")
            {
                ChangeFrequency = ChangeFrequency.Weekly
            };

            string result = _serializer.Serialize(sitemapNode);

            result.Should().BeXmlEquivalent("Samples/sitemap-node-3.xml");
        }

        [Test]
        public void Serialize_SitemapNodeWithPriority()
        {
            SitemapNode sitemapNode = new SitemapNode("abc")
            {
                Priority = 0.8M
            };

            string result = _serializer.Serialize(sitemapNode);

            result.Should().BeXmlEquivalent("Samples/sitemap-node-4.xml");
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

            result.Should().BeXmlEquivalent("Samples/sitemap-node-5.xml");
        }


        [Test]
        public void Serialize_SitemapIndexNode()
        {
            SitemapIndexNode sitemapIndexNode = new SitemapIndexNode { Url = "abc" };

            string result = _serializer.Serialize(sitemapIndexNode);

            result.Should().BeXmlEquivalent("Samples/sitemap-index-node-1.xml");
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

            result.Should().BeXmlEquivalent("Samples/sitemap-index-node-2.xml");
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

            result.Should().BeXmlEquivalent("Samples/sitemap-node-6.xml");
        }



        [Test]
        public void Serialize_SitemapVideoNode()
        {
            SitemapNode sitemapNode = new SitemapNode("abc")
            {
                Video = new SitemapVideo
                {
                    ContentLoc = "http://www.example.com/video123.flv",
                    FamilyFriendly = "yes",
                    Description = "Alkis shows you how to get perfectly done steaks every time",
                    ThumbnailLoc = "http://www.example.com/thumbs/123.jpg",
                    PublicationDate = new DateTime(2014, 11, 5, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Grilling steaks for summer"

                }
            };

            _namespaces.Add(Namespaces.VideoPrefix, Namespaces.Video);

            string result = _serializer.Serialize(sitemapNode);

            result.Should().BeXmlEquivalent("Samples/sitemap-node-7.xml");
        }
    }
}