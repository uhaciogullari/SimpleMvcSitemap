using Microsoft.AspNetCore.Hosting;

namespace SimpleMvcSitemap.Website
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder().UseKestrel()
                                           .UseIISIntegration()
                                           .UseStartup<Startup>()
                                           .UseUrls("http://localhost:14956/abc")
                                           .Build();

            host.Run();
        }
    }
}
