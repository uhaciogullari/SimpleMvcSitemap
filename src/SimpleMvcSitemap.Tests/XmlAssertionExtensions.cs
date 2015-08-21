using System.Xml;
using FluentAssertions;
using FluentAssertions.Primitives;
using JetBrains.Annotations;

namespace SimpleMvcSitemap.Tests
{
    public static class XmlAssertionExtensions
    {
        public static void BeXmlEquivalent(this StringAssertions assertions, [PathReference]string filename)
        {
            XmlDocument doc = new XmlDocument { PreserveWhitespace = false };
            doc.Load(filename);

            assertions.Subject.Should().Be(doc.InnerXml);
        }
    }
}