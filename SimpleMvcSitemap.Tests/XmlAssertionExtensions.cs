using System.Xml;
using FluentAssertions;
using FluentAssertions.Primitives;

namespace SimpleMvcSitemap.Tests
{
    public static class XmlAssertionExtensions
    {
        public static void BeXmlEquivalent(this StringAssertions assertions, string filename)
        {
            XmlDocument doc = new XmlDocument { PreserveWhitespace = false };
            doc.Load(filename);

            assertions.Subject.Should().Be(doc.InnerXml);
        }
    }
}