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
        private readonly TestDataBuilder testDataBuilder;

        public XmlProcessingInstructionHandlerTests()
        {
            xmlProcessingInstructionHandler = new XmlProcessingInstructionHandler();
            xmlWriter = MockFor<XmlWriter>();
            testDataBuilder = new TestDataBuilder();
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
            var sitemapModel = testDataBuilder.CreateSitemapWithSingleStyleSheet();

            xmlWriter.Setup(writer => writer.WriteProcessingInstruction("xml-stylesheet", @"type=""text/xsl"" href=""http://www.icrossing.com/sitemap.xsl"""))
                     .Verifiable();

            xmlProcessingInstructionHandler.AddStyleSheets(xmlWriter.Object, sitemapModel);
        }


        [Fact]
        public void AddStyleSheets_ModelContainsMultipleStyleSheets_WriteMultipleInstructions()
        {
            var sitemapModel = testDataBuilder.CreateSitemapWithMultipleStyleSheets();

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