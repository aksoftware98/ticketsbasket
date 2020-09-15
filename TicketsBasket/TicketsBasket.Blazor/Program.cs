using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace TicketsBasket
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddHttpClient("TicketsBasket.Api", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["ApiUrl"]);
            }).AddHttpMessageHandler(sp => sp.GetRequiredService<AuthorizationMessageHandler>()
                                                .ConfigureHandler(new[] { builder.Configuration["ApiUrl"] },
                                                                  new[] { builder.Configuration["AzureAdB2C:Scope"] }));

            builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("TicketsBasket.Api"));

            builder.Services.AddMsalAuthentication(options =>
            {
                builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
                options.ProviderOptions.DefaultAccessTokenScopes.Add(builder.Configuration["AzureAdB2C:Scope"]);
            });

            await builder.Build().RunAsync();
        }
    }
}
