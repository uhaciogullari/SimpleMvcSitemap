using System;
using System.Collections.Generic;

namespace SimpleMvcSitemap.Sample.SampleBusiness
{
    public class SampleSitemapNodeBuilder : ISampleSitemapNodeBuilder
    {
        public IEnumerable<SitemapIndexNode> BuildSitemapIndex()
        {
            var nodes = new List<SitemapIndexNode>();
            nodes.Add(new SitemapIndexNode("/sitemapcategories")
            {
                LastModificationDate = DateTime.Now
            });

            nodes.Add(new SitemapIndexNode("/sitemapbrands")
            {
                LastModificationDate = DateTime.Now
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
                ImageDefinition = new ImageDefinition("/image1")
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
                ImageDefinition = new ImageDefinition("test.img")
                    {
                        Caption = "caption",
                        Title = "title"
                    }
            });

            return nodes;
        }
    }
}