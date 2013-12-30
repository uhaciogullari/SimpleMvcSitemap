using System;
using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    [XmlRoot("url", Namespace = Namespaces.Sitemap)]
    public class SitemapNode : IHasUrl
    {
        internal SitemapNode() { }

        public SitemapNode(string url)
        {
            Url = url;
        }

        [XmlElement("image", Order = 2, Namespace = Namespaces.Image)]
        public ImageDefinition ImageDefinition { get; set; }

        [XmlElement("lastmod", Order = 3)]
        public DateTime? LastModificationDate { get; set; }

        [XmlElement("changefreq", Order = 4)]
        public ChangeFrequency? ChangeFrequency { get; set; }

        [XmlElement("priority", Order = 5)]
        public decimal? Priority { get; set; }

        [XmlElement("loc", Order = 1)]
        public string Url { get; set; }

        //http://stackoverflow.com/questions/1296468/suppress-null-value-types-from-being-emitted-by-xmlserializer
        //http://msdn.microsoft.com/en-us/library/53b8022e.aspx

        public bool ShouldSerializeLastModificationDate()
        {
            return LastModificationDate != null;
        }

        public bool ShouldSerializePriority()
        {
            return Priority != null;
        }

        public bool ShouldSerializeUrl()
        {
            return Url != null;
        }

        public bool ShouldSerializeChangeFrequency()
        {
            return ChangeFrequency != null;
        }

        public bool ShouldSerializeImageDefinition()
        {
            return ImageDefinition != null;
        }
    }
}