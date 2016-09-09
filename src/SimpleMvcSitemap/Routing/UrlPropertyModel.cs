using System.Collections.Generic;
using System.Reflection;

namespace SimpleMvcSitemap.Routing
{
    class UrlPropertyModel
    {
        public UrlPropertyModel()
        {
            UrlProperties = new List<PropertyInfo>();
            EnumerableProperties = new List<PropertyInfo>();
            ClassProperties = new List<PropertyInfo>();
        }
        
        public List<PropertyInfo> UrlProperties { get; set; }
        public List<PropertyInfo> EnumerableProperties { get; set; }
        public List<PropertyInfo> ClassProperties { get; set; }
    }
}