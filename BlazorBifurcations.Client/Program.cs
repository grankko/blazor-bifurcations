using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using BlazorBifurcations.Calculations;
using BlazorBifurcations.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorBifurcations.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            builder.Services.AddSingleton<CalculationService>();
            builder.Services.AddSingleton<JavaScriptService>();
            builder.Services.AddSingleton<DiagramService>();
            await builder.Build().RunAsync();
        }
    }
}
