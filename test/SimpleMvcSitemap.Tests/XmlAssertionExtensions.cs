using FluentAssertions;
using FluentAssertions.Primitives;
using System.Xml.Linq;
using System.IO;
using Microsoft.Extensions.PlatformAbstractions;

namespace SimpleMvcSitemap.Tests
{
    public static class XmlAssertionExtensions
    {
        public static void BeXmlEquivalent(this StringAssertions assertions, string filename)
        {
            var fullPath = Path.Combine(new ApplicationEnvironment().ApplicationBasePath, "Samples", filename);
            XDocument doc1 = XDocument.Parse(File.ReadAllText(fullPath));
            XDocument doc2 = XDocument.Parse(assertions.Subject);

            XNode.DeepEquals(doc1, doc2).Should().BeTrue();
        }
    }
}