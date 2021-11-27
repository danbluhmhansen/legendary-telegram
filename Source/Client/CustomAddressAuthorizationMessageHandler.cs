namespace BlazorApp1.Client;

using BlazorApp1.Client.Configuration;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Options;

public class CustomAddressAuthorizationMessageHandler : AuthorizationMessageHandler
{
	public CustomAddressAuthorizationMessageHandler(IAccessTokenProvider provider, NavigationManager navigation,
		IOptions<ServerOptions> serverOptions) : base(provider, navigation)
	{
		ConfigureHandler(new[] { navigation.BaseUri, serverOptions.Value.Route!.ToString() });
	}
}
