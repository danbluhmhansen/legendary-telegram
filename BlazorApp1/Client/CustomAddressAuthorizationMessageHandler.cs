namespace BlazorApp1.Client;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

public class CustomAddressAuthorizationMessageHandler : AuthorizationMessageHandler
{
	public CustomAddressAuthorizationMessageHandler(
		IAccessTokenProvider provider,
		NavigationManager navigation,
		IConfiguration configuration) : base(provider, navigation)
	{
		ConfigureHandler(new[] { navigation.BaseUri, configuration.GetValue<string>("ServerUrl") });
	}
}
