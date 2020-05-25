using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace SimpleMvcSitemap.Website
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseIISIntegration()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureLogging(builder => builder.AddConsole())
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}