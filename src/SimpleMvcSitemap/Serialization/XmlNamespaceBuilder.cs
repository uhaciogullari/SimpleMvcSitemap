using System.Collections.Generic;
using System.Xml.Serialization;

namespace SimpleMvcSitemap.Serialization
{
    class XmlNamespaceBuilder : IXmlNamespaceBuilder
    {
        private readonly IDictionary<string, string> _prefixList;

        public XmlNamespaceBuilder()
        {
            _prefixList = new Dictionary<string, string>
            {
                { XmlNamespaces.Sitemap, XmlNamespaces.SitemapPrefix },
                { XmlNamespaces.Image, XmlNamespaces.ImagePrefix },
                { XmlNamespaces.News, XmlNamespaces.NewsPrefix},
                { XmlNamespaces.Video, XmlNamespaces.VideoPrefix},
                { XmlNamespaces.Mobile, XmlNamespaces.MobilePrefix},
                { XmlNamespaces.Xhtml, XmlNamespaces.XhtmlPrefix}
            };
        }

        public XmlSerializerNamespaces Create(IEnumerable<string> namespaces)
        {
            XmlSerializerNamespaces result = new XmlSerializerNamespaces();
            result.Add(XmlNamespaces.SitemapPrefix, XmlNamespaces.Sitemap);

            foreach (var ns in namespaces)
            {
                string prefix;
                if (_prefixList.TryGetValue(ns, out prefix))
                {
                    result.Add(prefix, ns);
                }
            }

            return result;
        }
    }
}