using System;
using System.Collections.Generic;

namespace SimpleMvcSitemap.Sample.SampleBusiness
{
    public class SampleSitemapNodeBuilder : ISampleSitemapNodeBuilder
    {
        public IEnumerable<SitemapIndexNode> BuildSitemapIndex()
        {
            var nodes = new List<SitemapIndexNode>();
            nodes.Add(new SitemapIndexNode
            {
                LastModificationDate = DateTime.Now,
                Url = "/sitemapcategories"
            });

            nodes.Add(new SitemapIndexNode
            {
                LastModificationDate = DateTime.Now,
                Url = "/sitemapbrands"
            });

            return nodes;
        }

        public IEnumerable<SitemapNode> BuildSitemapNodes()
        {
            var nodes = new List<SitemapNode>();
            nodes.Add(new SitemapNode("http://msdn.microsoft.com/en-us/library/ms752244(v=vs.110).aspx")
            {
                LastModificationDate = DateTime.Now,
                ChangeFrequency = ChangeFrequency.Daily,
                Priority = 0.5M,
                ImageDefinition = new ImageDefinition
                    {
                        Caption = "caption",
                        Title = "title"
                    }
            });

            nodes.Add(new SitemapNode("http://joelabrahamsson.com/xml-sitemap-with-aspnet-mvc/")
            {
                LastModificationDate = DateTime.Now,
                ChangeFrequency = ChangeFrequency.Weekly,
                Priority = 0.5M,
                ImageDefinition = new ImageDefinition
                    {
                        Caption = "caption",
                        Title = "title",
                        Url = "test.img"
                    }
            });

            return nodes;
        }
    }
}