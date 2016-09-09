using System.Collections;
using System.Collections.Generic;

namespace SimpleMvcSitemap.Routing
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<object> SelectMany(this IEnumerable<IEnumerable> source)
        {
            foreach (var outer in source)
            {
                foreach (object inner in outer)
                {
                    yield return inner;
                }
            }
        }
    }
}