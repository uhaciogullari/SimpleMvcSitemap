using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    public class ImageDefinition
    {
        [XmlElement("caption", Order = 1)]
        public string Caption { get; set; }

        [XmlElement("title", Order = 2)]
        public string Title { get; set; }

        [XmlElement("loc", Order = 3)]
        public string Url { get; set; }

        //http://stackoverflow.com/questions/1296468/suppress-null-value-types-from-being-emitted-by-xmlserializer
        //http://msdn.microsoft.com/en-us/library/53b8022e.aspx

        public bool ShouldSerializeCaption()
        {
            return Caption != null;
        }

        public bool ShouldSerializeTitle()
        {
            return Title != null;
        }

        public bool ShouldSerializeUrl()
        {
            return Url != null;
        }
    }
}
