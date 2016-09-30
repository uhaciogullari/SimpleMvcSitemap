using System.Xml;
using SimpleMvcSitemap.StyleSheets;

namespace SimpleMvcSitemap.Serialization
{
    interface IXmlProcessingInstructionHandler
    {
        void AddStyleSheets(XmlWriter xmlWriter, IHasStyleSheets model);
    }
}