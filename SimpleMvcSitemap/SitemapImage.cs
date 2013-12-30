using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    public class SitemapImage : IHasUrl
    {
        internal SitemapImage() { }

        public SitemapImage(string url)
        {
            Url = url;
        }

        [XmlElement("loc", Order = 1)]
        public string Url { get; set; }

        [XmlElement("caption", Order = 2)]
        public string Caption { get; set; }

        [XmlElement("geo_location", Order = 3)]
        public string Location { get; set; }

        [XmlElement("title", Order = 4)]
        public string Title { get; set; }

        [XmlElement("license", Order = 5)]
        public string License { get; set; }

        public bool ShouldSerializeUrl()
        {
            return Url != null;
        }

        public bool ShouldSerializeCaption()
        {
            return Caption != null;
        }

        public bool ShouldSerializeLocation()
        {
            return Location != null;
        }

        public bool ShouldSerializeTitle()
        {
            return Title != null;
        }

        public bool ShouldSerializeLicense()
        {
            return License != null;
        }

    }
}
