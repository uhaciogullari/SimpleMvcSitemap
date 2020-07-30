namespace SimpleMvcSitemap.StyleSheets
{
    /// <summary>
    /// Represents the stylesheets that will be attached to created XML files.
    /// </summary>
    public class XmlStyleSheet
    {
        /// <summary>
        /// Creates an XML stylesheet.
        /// </summary>
        /// <param name="url">URL of the style sheet</param>
        public XmlStyleSheet(string url)
        {
            Url = url;
            Type = "text/xsl";
        }

        /// <summary>
        /// URL of the style sheet
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Media type of the style sheet.
        /// Default value is "text/xsl"
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Title of the style sheet.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The media which the stylesheet applies.
        /// </summary>
        public string Media { get; set; }

        /// <summary>
        /// Character encoding of the style sheet.
        /// </summary>
        public string Charset { get; set; }

        /// <summary>
        /// Specifies if the style sheet is an alternative style sheet.
        /// </summary>
        public YesNo? Alternate { get; set; }
    }
}