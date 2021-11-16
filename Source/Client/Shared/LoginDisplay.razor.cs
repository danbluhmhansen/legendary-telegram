namespace BlazorApp1.Client.Shared;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

public partial class LoginDisplay : ComponentBase
{
	[Inject] private NavigationManager? Navigation { get; init; }
	[Inject] private SignOutSessionStateManager? SignOutManager { get; init; }

	private async Task BeginSignOut()
	{
		if (this.SignOutManager is not null)
			await this.SignOutManager.SetSignOutState();
		this.Navigation?.NavigateTo("authentication/logout");
	}
}
