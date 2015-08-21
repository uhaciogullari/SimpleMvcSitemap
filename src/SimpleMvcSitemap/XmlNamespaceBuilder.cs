using System.Collections.Generic;
using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    class XmlNamespaceBuilder : IXmlNamespaceBuilder
    {
        private readonly IDictionary<string, string> _prefixList;

        public XmlNamespaceBuilder()
        {
            _prefixList = new Dictionary<string, string>
                                       {
                                           { Namespaces.Sitemap, Namespaces.SitemapPrefix },
                                           { Namespaces.Image, Namespaces.ImagePrefix },
                                           { Namespaces.News, Namespaces.NewsPrefix},
                                           { Namespaces.Video, Namespaces.VideoPrefix},
                                           { Namespaces.Mobile, Namespaces.MobilePrefix}
                                       };
        }

        public XmlSerializerNamespaces Create(IEnumerable<string> namespaces)
        {
            XmlSerializerNamespaces result = new XmlSerializerNamespaces();
            result.Add(Namespaces.SitemapPrefix, Namespaces.Sitemap);

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