using System.Xml;
using FluentAssertions;
using FluentAssertions.Primitives;
using JetBrains.Annotations;
using System.Xml.Linq;
using System.IO;

namespace SimpleMvcSitemap.Tests
{
    public static class XmlAssertionExtensions
    {
        public static void BeXmlEquivalent(this StringAssertions assertions, [PathReference]string filename)
        {
            XmlDocument doc = new XmlDocument { PreserveWhitespace = false };
            doc.Load(filename);

            XDocument doc1 = XDocument.Parse(File.ReadAllText(filename));
            XDocument doc2 = XDocument.Parse(assertions.Subject);

            XNode.DeepEquals(doc1, doc2).Should().BeTrue();
        }
    }
}