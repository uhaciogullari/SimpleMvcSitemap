using System.Text;
using System.Xml;
using SimpleMvcSitemap.StyleSheets;

namespace SimpleMvcSitemap.Serialization
{
    class XmlProcessingInstructionHandler : IXmlProcessingInstructionHandler
    {
        public void AddStyleSheets(XmlWriter xmlWriter, IHasStyleSheets model)
        {
            if (model.StyleSheets == null)
            {
                return;
            }

            foreach (var styleSheet in model.StyleSheets)
            {
                StringBuilder stringBuilder = new StringBuilder($@"type=""{styleSheet.Type}"" href=""{styleSheet.Url}""");

                WriteAttribute(stringBuilder, "title", styleSheet.Title);
                WriteAttribute(stringBuilder, "media", styleSheet.Media);
                WriteAttribute(stringBuilder, "charset", styleSheet.Charset);

                if (styleSheet.Alternate.HasValue && styleSheet.Alternate.Value != YesNo.None)
                {
                    WriteAttribute(stringBuilder, "alternate", styleSheet.Alternate.Value.ToString().ToLowerInvariant());
                }

                xmlWriter.WriteProcessingInstruction("xml-stylesheet", stringBuilder.ToString());
            }
        }

        private void WriteAttribute(StringBuilder stringBuilder, string attributeName, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                stringBuilder.Append($@" {attributeName}=""{value}""");
            }
        }
    }
}