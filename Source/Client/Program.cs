using System.Text.Json;

using BlazorApp1.Client;
using BlazorApp1.Client.Commands;
using BlazorApp1.Client.Configuration;
using BlazorApp1.Client.Data;

using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OData.Client;
using Microsoft.OData.Extensions.Client;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
	.AddBlazorise()
	.AddBootstrap5Providers()
	.AddFontAwesomeIcons();

builder.Services.AddScoped<CustomAddressAuthorizationMessageHandler>();

builder.Services
	.AddHttpClient("BlazorApp1.ServerAPI",
		(IServiceProvider serviceProvider, HttpClient client) =>
			client.BaseAddress = serviceProvider.GetRequiredService<IOptions<ServerOptions>>().Value.Route)
	.AddHttpMessageHandler<CustomAddressAuthorizationMessageHandler>();

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped((IServiceProvider serviceProvider) =>
	serviceProvider.GetRequiredService<IHttpClientFactory>().CreateClient("BlazorApp1.ServerAPI"));

builder.Services.AddOidcAuthentication((RemoteAuthenticationOptions<OidcProviderOptions> options) =>
{
	builder.Configuration
		.GetSection("RemoteAuthentication")
		.GetSection("AuthenticationPaths")
		.Bind(options.AuthenticationPaths);

	builder.Configuration
		.GetSection("RemoteAuthentication")
		.GetSection("ProviderOptions")
		.Bind(options.ProviderOptions);
});

builder.Services.AddLogging();

builder.Services.AddODataClient()
	.ConfigureODataClient((DataServiceContext context) =>
	{
		context.BaseUri = new Uri(builder.Configuration.GetSection("ServerOptions").GetValue<string>("Route") + "v1/");
		context.HttpRequestTransportMode = HttpRequestTransportMode.HttpClient;
		context.Format.UseJson();
	})
	.AddHttpClient();

builder.Services.AddScoped((IServiceProvider serviceProvider) =>
	serviceProvider.GetRequiredService<IODataClientFactory>().CreateClient<ODataServiceContext>(
		serviceProvider.GetRequiredService<IOptions<ServerOptions>>().Value.Route));

builder.Services.AddScoped<ReadDataCommand>();
builder.Services.AddScoped<ComputeCharacterCommand>();

builder.Services.Configure<BlazoriseOptions>(builder.Configuration.GetSection(nameof(BlazoriseOptions)));
builder.Services.Configure<JsonSerializerOptions>(builder.Configuration.GetSection(nameof(JsonSerializerOptions)));
builder.Services.Configure<ServerOptions>(builder.Configuration.GetSection(nameof(ServerOptions)));

builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

WebAssemblyHost host = builder.Build();

await host.RunAsync().ConfigureAwait(false);
