using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Packing.Mudblazor.Server.Areas.Identity.IdentityHostingStartup))]
namespace Packing.Mudblazor.Server.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}