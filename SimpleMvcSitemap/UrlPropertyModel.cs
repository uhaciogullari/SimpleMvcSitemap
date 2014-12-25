using System.Collections.Generic;
using System.Reflection;

namespace SimpleMvcSitemap
{
    class UrlPropertyModel
    {
        public UrlPropertyModel()
        {
            UrlProperties = new List<PropertyInfo>();
            EnumerableProperties = new List<PropertyInfo>();
            ClassPropeties = new List<PropertyInfo>();
        }
        
        public List<PropertyInfo> UrlProperties { get; set; }
        public List<PropertyInfo> EnumerableProperties { get; set; }
        public List<PropertyInfo> ClassPropeties { get; set; }
    }
}