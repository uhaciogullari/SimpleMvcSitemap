SimpleMvcSitemap
=============
A simple library for creating sitemap files inside ASP.NET MVC applications.

SimpleMvcSitemap lets you create [sitemap files](http://www.sitemaps.org/protocol.html) inside action methods without any configuration. It also supports generating [sitemap index files](http://www.sitemaps.org/protocol.html#index). Since you are using regular action methods you can take advantage of ASP.NET MVC caching and routing.

## Installation

Install the [NuGet package](https://www.nuget.org/packages/SimpleMvcSitemap/) on your ASP.NET MVC project

    Install-Package SimpleMvcSitemap

SimpleMvcSitemap supports ASP.NET MVC 3/4/5 and .NET 4.0/4.5/4.5.1 versions.

## Examples

You can use SitemapProvider class to create sitemap files inside any action method. Here's an example:
```csharp
public class SitemapController : Controller
{
    public ActionResult Index()
    {
        List<SitemapNode> nodes = new List<SitemapNode>
        {
            new SitemapNode(Url.Action("Index","Home")),
            new SitemapNode(Url.Action("About","Home")),
            //other nodes
        };

        return new SitemapProvider().CreateSitemap(HttpContext, nodes);
    }
}
```

SitemapNode class also lets you specify the [optional attributes](http://www.sitemaps.org/protocol.html#xmlTagDefinitions):
```csharp
new SitemapNode(Url.Action("Index", "Home"))
{
    ChangeFrequency = ChangeFrequency.Weekly,
    LastModificationDate = DateTime.UtcNow,
    Priority = 0.8M
}
```	
Sitemap files must have no more than 50,000 URLs and must be no larger then 10MB [as stated in the protocol](http://www.sitemaps.org/protocol.html#index). If you think your sitemap file can exceed these limits you should create a sitemap index file. A regular sitemap will be created if you don't have more nodes than sitemap size.
```csharp
public class SitemapController : Controller
{
    class SiteMapConfiguration : SitemapConfigurationBase
    {
        private readonly UrlHelper _urlHelper;

        public SiteMapConfiguration(UrlHelper urlHelper, int? currentPage) : base(currentPage)
        {
            _urlHelper = urlHelper;
	//Size = 40000; //You can set URL count for each sitemap file. Default size is 50000
        }

        public override string CreateSitemapUrl(int currentPage)
        {
            return _urlHelper.Action("LargeSitemap", "Sitemap", new { id = currentPage });
        }
    }

    public ActionResult LargeSitemap(int? id)
    {
        //should be instantiated on each method call
        SitemapConfiguration configuration = new SiteMapConfiguration(Url, id);

        return new SitemapProvider().CreateSitemap(HttpContext, GetNodes(), configuration);
    }
}
```

You can also create index files by providing sitemap file URLs manually.

```csharp
List<SitemapIndexNode> sitemapIndexNodes = new List<SitemapIndexNode>
{
    new SitemapIndexNode(Url.Action("Categories","Sitemap")),
    new SitemapIndexNode(Url.Action("Products","Sitemap"))
};

return new SitemapProvider().CreateSitemap(HttpContext, sitemapIndexNodes);
```

## Unit Testing and Dependency Injection

SitemapProvider class implements the ISitemapProvider interface which can be injected to your controllers and be replaced with test doubles. All methods are thread safe so they can be used with singleton life cycle.
```csharp
public class SitemapController : Controller
{
    private readonly ISitemapProvider _sitemapProvider;

    public SitemapController(ISitemapProvider sitemapProvider)
    {
        _sitemapProvider = sitemapProvider;
    }
	
	//action methods
}
```


## License

SimpleMvcSitemap is licensed under [MIT License](http://opensource.org/licenses/MIT "Read more about the MIT license form"). Refer to license file for more information.


[![Bitdeli Badge](https://d2weczhvl823v0.cloudfront.net/uhaciogullari/simplemvcsitemap/trend.png)](https://bitdeli.com/free "Bitdeli Badge")

