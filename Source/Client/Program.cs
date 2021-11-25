using System.Text.Json;
using System.Text.Json.Serialization;

using BlazorApp1.Client;

using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
	.AddBlazorise(options => options.ChangeTextOnKeyPress = true)
	.AddBootstrap5Providers()
	.AddFontAwesomeIcons();

builder.Services.AddScoped<CustomAddressAuthorizationMessageHandler>();

builder.Services
	.AddHttpClient("BlazorApp1.ServerAPI",
		client => client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ServerUrl")))
	.AddHttpMessageHandler<CustomAddressAuthorizationMessageHandler>();

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BlazorApp1.ServerAPI"));

builder.Services.AddOidcAuthentication(options =>
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

builder.Services.Configure((JsonSerializerOptions options) =>
	options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);

builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

await builder.Build().RunAsync().ConfigureAwait(false);
