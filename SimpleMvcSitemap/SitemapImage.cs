using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    public class SitemapImage
    {
        internal SitemapImage() { }

        public SitemapImage(string url)
        {
            Url = url;
        }

        [XmlElement("caption", Order = 1)]
        public string Caption { get; set; }

        [XmlElement("title", Order = 2)]
        public string Title { get; set; }

        [XmlElement("loc", Order = 3)]
        public string Url { get; set; }

        public bool ShouldSerializeCaption()
        {
            return Caption != null;
        }

        public bool ShouldSerializeTitle()
        {
            return Title != null;
        }

        public bool ShouldSerializeUrl()
        {
            return Url != null;
        }
    }
}
