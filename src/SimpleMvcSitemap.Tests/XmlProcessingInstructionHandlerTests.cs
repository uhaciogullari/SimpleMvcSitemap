using System.Collections.Generic;
using System.Xml;
using Moq;
using SimpleMvcSitemap.Serialization;
using SimpleMvcSitemap.StyleSheets;
using Xunit;

namespace SimpleMvcSitemap.Tests
{
    public class XmlProcessingInstructionHandlerTests : TestBase
    {
        private readonly IXmlProcessingInstructionHandler xmlProcessingInstructionHandler;
        private readonly Mock<XmlWriter> xmlWriter;

        public XmlProcessingInstructionHandlerTests()
        {
            xmlProcessingInstructionHandler = new XmlProcessingInstructionHandler();
            xmlWriter = MockFor<XmlWriter>();
        }


        [Fact]
        public void AddStyleSheets_SyleSheetListIsNullOrEmpty_DoesNotWriteAnything()
        {
            xmlProcessingInstructionHandler.AddStyleSheets(xmlWriter.Object, new SitemapModel());

            xmlProcessingInstructionHandler.AddStyleSheets(xmlWriter.Object, new SitemapModel { StyleSheets = new List<XmlStyleSheet>() });
        }

        [Fact]
        public void AddStyleSheets_ModelContainsSingleStyleSheet_WriteInstruction()
        {
            var sitemapModel = new SitemapModel
            {
                StyleSheets = new List<XmlStyleSheet>
                {
                    new XmlStyleSheet("http://www.icrossing.com/sitemap.xsl")
                }
            };

            xmlWriter.Setup(writer => writer.WriteProcessingInstruction("xml-stylesheet", @"type=""text/xsl"" href=""http://www.icrossing.com/sitemap.xsl"""))
                     .Verifiable();

            xmlProcessingInstructionHandler.AddStyleSheets(xmlWriter.Object, sitemapModel);
        }


        [Fact]
        public void AddStyleSheets_ModelContainsMultipleStyleSheets_WriteMultipleInstructions()
        {
            var sitemapModel = new SitemapModel
            {
                StyleSheets = new List<XmlStyleSheet>
                {
                    new XmlStyleSheet("/regular.css") {Type = "text/css",Title = "Regular fonts",Media = "screen"},
                    new XmlStyleSheet("/bigfonts.css") {Type = "text/css",Title = "Extra large fonts",Media = "projection",Alternate = YesNo.Yes},
                    new XmlStyleSheet("/smallfonts.css") {Type = "text/css",Title = "Smaller fonts",Media = "print",Alternate = YesNo.Yes,Charset = "UTF-8"}
                }

            };

            xmlWriter.Setup(writer => writer.WriteProcessingInstruction("xml-stylesheet", @"type=""text/css"" href=""/regular.css"" title=""Regular fonts"" media=""screen"""))
                     .Verifiable();

            xmlWriter.Setup(writer => writer.WriteProcessingInstruction("xml-stylesheet", @"type=""text/css"" href=""/bigfonts.css"" title=""Extra large fonts"" media=""projection"" alternate=""yes"""))
                     .Verifiable();

            xmlWriter.Setup(writer => writer.WriteProcessingInstruction("xml-stylesheet", @"type=""text/css"" href=""/smallfonts.css"" title=""Smaller fonts"" media=""print"" charset=""UTF-8"" alternate=""yes"""))
                     .Verifiable();

            xmlProcessingInstructionHandler.AddStyleSheets(xmlWriter.Object, sitemapModel);
        }
    }
}