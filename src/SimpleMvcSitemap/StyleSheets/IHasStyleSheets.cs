using System.Collections.Generic;

namespace SimpleMvcSitemap.StyleSheets
{
    /// <summary>
    /// Specifies that a class has XML style sheets that
    /// will be used during the serialization.
    /// </summary>
    public interface IHasStyleSheets
    {
        /// <summary>
        /// XML style sheets
        /// </summary>
        List<XmlStyleSheet> StyleSheets { get; set; }
    }
}
