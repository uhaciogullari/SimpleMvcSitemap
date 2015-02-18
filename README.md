SimpleMvcSitemap
=============
A simple library for creating sitemap files inside ASP.NET MVC applications.

[![Build status](https://ci.appveyor.com/api/projects/status/0ix6isof9dmu7rm2?svg=true)](https://ci.appveyor.com/project/uhaciogullari/simplemvcsitemap)

SimpleMvcSitemap lets you create [sitemap files](http://www.sitemaps.org/protocol.html) inside action methods without any configuration. It also supports generating [sitemap index files](http://www.sitemaps.org/protocol.html#index). Since you are using regular action methods you can take advantage of ASP.NET MVC caching and routing.

##Table of contents
 - [Installation](#installation)
 - [Examples](#examples)
 - [Sitemap Index Files](#sitemap-index-files)
 - [Google Sitemap Extensions](#google-sitemap-extensions)
   - [Images](#images)
   - [Videos](#videos)
   - [News](#news)
   - [Mobile](#mobile)
 - [Unit Testing and Dependency Injection](#di)
 - [License](#license)


## <a id="installation">Installation</a>

Install the [NuGet package](https://www.nuget.org/packages/SimpleMvcSitemap/) on your ASP.NET MVC project. It supports ASP.NET MVC 3/4/5 and .NET 4.0/4.5/4.5.1 versions.

    Install-Package SimpleMvcSitemap

SimpleMvcSitemap references the ASP.NET MVC assembly in the [earliest package](https://www.nuget.org/packages/Microsoft.AspNet.Mvc/3.0.20105.1). Since it's a strongly-named assembly, you will have to keep assembly binding redirection in Web.config if you are working with ASP.NET MVC 4/5. These sections are created for you in project templates.

```xml
<runtime>
  <assemblyBinding xmlns="urn:schemas-microsoft-com:asm.v1">
    <dependentAssembly>
      <assemblyIdentity name="System.Web.Mvc" publicKeyToken="31bf3856ad364e35" />
      <bindingRedirect oldVersion="0.0.0.0-4.0.0.0" newVersion="4.0.0.0" />
    </dependentAssembly>
  </assemblyBinding>
</runtime>
```



## <a id="examples">Examples</a>

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

## <a id="sitemap-index-files">Sitemap Index Files</a>

Sitemap files must have no more than 50,000 URLs and must be no larger then 10MB [as stated in the protocol](http://www.sitemaps.org/protocol.html#index). If you think your sitemap file can exceed these limits you should create a sitemap index file. A regular sitemap will be created if you don't have more nodes than sitemap size.

SimpleMvcSitemap assumes you will get this amount of data from a data source. If you are using a LINQ provider, SimpleMvcSitemap can handle the paging. 

![Generating sitemap index files](http://i.imgur.com/ZJ7UNkM.png)

This requires a little configuration:

```csharp
public class ProductSitemapConfiguration : ISitemapConfiguration<Product>
{
    private readonly UrlHelper _urlHelper;

    public ProductSitemapConfiguration(UrlHelper urlHelper, int? currentPage)
    {
        _urlHelper = urlHelper;
        Size = 50000;
        CurrentPage = currentPage;
    }

    public int? CurrentPage { get; private set; }

    public int Size { get; private set; }

    public string CreateSitemapUrl(int currentPage)
    {
        return _urlHelper.RouteUrl("ProductSitemap", new { currentPage });
    }

    public SitemapNode CreateNode(Product source)
    {
        return new SitemapNode(_urlHelper.RouteUrl("Product", new { id = source.Id }));
    }
}
```
Then you can create the sitemap file or the index file within a single action method.

```csharp
public ActionResult Products(int? currentPage)
{
    IQueryable<Product> dataSource = _products.Where(item => item.Status == ProductStatus.Active);
    ProductSitemapConfiguration configuration = new ProductSitemapConfiguration(Url, currentPage);

    return new SitemapProvider().CreateSitemap(HttpContext, dataSource, configuration);
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

## <a id="google-sitemap-extensions">Google Sitemap Extensions</a>

You can use [Google's sitemap extensions](https://support.google.com/webmasters/answer/183668?hl=en#2) to provide detailed information about specific content types like [images](https://support.google.com/webmasters/answer/178636), [videos](https://support.google.com/webmasters/answer/80472), [mobile](https://support.google.com/webmasters/answer/34648?rd=1) or [news](https://support.google.com/news/publisher/answer/75717?hl=en&ref_topic=2527688).

### <a id="images">Images</a>

```csharp
new SitemapNode(Url.Action("Display", "Product"))
{
    Images = new List<SitemapImage>
    {
        new SitemapImage(Url.Action("Image","Product", new {id = 1})),
        new SitemapImage(Url.Action("Image","Product", new {id = 2}))
    }
};
```

### <a id="videos">Videos</a>

```csharp
SitemapNode sitemapNode = new SitemapNode("http://www.example.com/videos/some_video_landing_page.html")
{
    Video = new SitemapVideo(title: "Grilling steaks for summer",
                             description: "Alkis shows you how to get perfectly done steaks every time",
                             thumbnailUrl: "http://www.example.com/thumbs/123.jpg", 
                             contentUrl: "http://www.example.com/video123.flv")
};
```

### <a id="news">News</a>

```csharp
SitemapNode sitemapNode = new SitemapNode("http://www.example.org/business/article55.html")
{
    News = new SitemapNews(newsPublication: new NewsPublication(name: "The Example Times", language: "en"),
                           publicationDate: new DateTime(2014, 11, 5, 0, 0, 0, DateTimeKind.Utc),
                           title: "Companies A, B in Merger Talks")
};
```

### <a id="mobile">Mobile</a>

```csharp
SitemapNode sitemapNode = new SitemapNode("http://mobile.example.com/article100.html")
{
    Mobile = new SitemapMobile()
};
```

## <a id="di">Unit Testing and Dependency Injection</a>

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


## <a id="license">License</a>

SimpleMvcSitemap is licensed under [MIT License](http://opensource.org/licenses/MIT "Read more about the MIT license form"). Refer to license file for more information.
