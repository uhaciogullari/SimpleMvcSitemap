using System.Xml;
using FluentAssertions;
using FluentAssertions.Primitives;
using System.Xml.Linq;
using System.IO;
using Microsoft.AspNetCore.Razor.Text;

namespace SimpleMvcSitemap.Tests
{
    public static class XmlAssertionExtensions
    {
        public static void BeXmlEquivalent(this StringAssertions assertions, string filename)
        {
            XmlDocument doc = new XmlDocument { PreserveWhitespace = false };
            doc.Load(File.OpenRead(filename));

            XDocument doc1 = XDocument.Parse(File.ReadAllText(filename));
            XDocument doc2 = XDocument.Parse(assertions.Subject);

            XNode.DeepEquals(doc1, doc2).Should().BeTrue();
        }
    }
}