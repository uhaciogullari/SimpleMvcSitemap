using System.Collections.Generic;

namespace SimpleMvcSitemap.Sample.SampleBusiness
{
    public interface ISampleSitemapNodeBuilder
    {
        IEnumerable<SitemapIndexNode> BuildSitemapIndex();
        IEnumerable<SitemapNode> BuildSitemapNodes();
    }
}