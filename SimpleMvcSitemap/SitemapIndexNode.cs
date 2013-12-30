using System;
using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    [XmlRoot("sitemap", Namespace = Namespaces.Sitemap)]
    public class SitemapIndexNode : IHasUrl
    {
        [XmlElement("loc", Order = 1)]
        public string Url { get; set; }

        [XmlElement("lastmod", Order = 2)]
        public DateTime? LastModificationDate { get; set; }

        //http://stackoverflow.com/questions/1296468/suppress-null-value-types-from-being-emitted-by-xmlserializer
        //http://msdn.microsoft.com/en-us/library/53b8022e.aspx
        public bool ShouldSerializeLastModificationDate()
        {
            return LastModificationDate != null;
        }

        public bool ShouldSerializeUrl()
        {
            return Url != null;
        }
    }
}