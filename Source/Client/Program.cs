using LegendaryTelegram.Client;
using LegendaryTelegram.Client.Data;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("OData", (serviceProvider, httpClient) =>
  httpClient.BaseAddress = serviceProvider.GetRequiredService<IConfiguration>().GetValue<Uri>("ODataServiceUri"));
builder.Services.AddHttpClient("Local", httpClient => httpClient.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

builder.Services.AddScoped<ODataServiceContext>();

await builder.Build().RunAsync();

