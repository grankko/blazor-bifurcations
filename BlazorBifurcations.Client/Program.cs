using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions.DependencyInjection;
using BlazorBifurcations.Calculations;
using BlazorBifurcations.Client.Services;

namespace BlazorBifurcations.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var bifurcator = new Bifurcator(0.5, 1000, 4);

            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            builder.Services.AddSingleton<Bifurcator>(bifurcator);
            builder.Services.AddSingleton<JavaScriptService>();
            builder.Services.AddSingleton<DiagramService>();
            await builder.Build().RunAsync();
        }
    }
}
