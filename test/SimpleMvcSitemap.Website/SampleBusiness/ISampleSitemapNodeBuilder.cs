using System.Collections.Generic;

namespace SimpleMvcSitemap.Website.SampleBusiness
{
    public interface ISampleSitemapNodeBuilder
    {
        IEnumerable<SitemapIndexNode> BuildSitemapIndex();
        SitemapModel BuildSitemapModel();
    }
}