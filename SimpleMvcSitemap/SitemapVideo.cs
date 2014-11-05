using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SimpleMvcSitemap
{
    public class SitemapVideo 
    {

        [XmlElement("thumbnail_loc", Order = 1)]
        public string ThumbnailLoc { get; set; }

        [XmlElement("title", Order = 2)]
        public string Title { get; set; }

        [XmlElement("description", Order = 3)]
        public string Description { get; set; }

        [XmlElement("content_loc", Order = 4)]
        public string ContentLoc { get; set; }

        [XmlElement("publication_date", Order = 5)]
        public DateTime? PublicationDate { get; set; }

        [XmlElement("family_friendly", Order = 6)]
        public string FamilyFriendly { get; set; }



        public bool ShouldSerializePublicationDate()
        {
            return PublicationDate.HasValue;
        }
    
        public bool ShouldSerializeFamilyFriendly()
        {
            return FamilyFriendly != null;
        }
    }
}