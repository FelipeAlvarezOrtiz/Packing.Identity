using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using MudBlazor.Services;

namespace Packing.Mudblazor.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient("Packing.Mudblazor.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            builder.Services.AddSingleton(sp =>
                new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
     
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Packing.Mudblazor.ServerAPI"));
            builder.Services.AddMudServices();
            builder.Services.AddApiAuthorization();
            builder.Services.AddApiAuthorization(options =>
            {
                options.UserOptions.RoleClaim = "role";
            });
            ConfigureServices(builder.Services);
            await builder.Build().RunAsync();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorizationCore();
        }
    }
}
