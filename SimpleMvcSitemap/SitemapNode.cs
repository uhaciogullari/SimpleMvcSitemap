using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    [XmlRoot("url", Namespace = Namespaces.Sitemap)]
    public class SitemapNode : IHasUrl , IXmlNamespaceProvider
    {
        internal SitemapNode() { }

        public SitemapNode(string url)
        {
            Url = url;
        }

        [XmlElement("loc", Order = 1)]
        public string Url { get; set; }

        [XmlElement("image", Order = 2, Namespace = Namespaces.Image)]
        public List<SitemapImage> Images { get; set; }

        [XmlElement("news", Order = 3, Namespace = Namespaces.News)]
        public SitemapNews News { get; set; }


        [XmlElement("video", Order = 4, Namespace = Namespaces.Video)]
        public SitemapVideo Video { get; set; }

        [XmlElement("lastmod", Order = 5)]
        public DateTime? LastModificationDate { get; set; }

        [XmlElement("changefreq", Order = 6)]
        public ChangeFrequency? ChangeFrequency { get; set; }

        [XmlElement("priority", Order = 7)]
        public decimal? Priority { get; set; }

        //http://stackoverflow.com/questions/1296468/suppress-null-value-types-from-being-emitted-by-xmlserializer
        //http://msdn.microsoft.com/en-us/library/53b8022e.aspx

        public bool ShouldSerializeUrl()
        {
            return Url != null;
        }

        public bool ShouldSerializeLastModificationDate()
        {
            return LastModificationDate != null;
        }

        public bool ShouldSerializeChangeFrequency()
        {
            return ChangeFrequency != null;
        }

        public bool ShouldSerializePriority()
        {
            return Priority != null;
        }

        IEnumerable<string> IXmlNamespaceProvider.GetNamespaces()
        {
            return new List<string> { Namespaces.Sitemap };
        }
    }
}