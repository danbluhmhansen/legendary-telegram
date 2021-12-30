namespace BlazorApp1.Client.Shared;

using Microsoft.AspNetCore.Components;

public partial class RedirectToLogin : ComponentBase
{
	[Inject] private NavigationManager? Navigation { get; init; }

	protected override void OnInitialized() =>
		this.Navigation?.NavigateTo($"authentication/login?returnUrl={Uri.EscapeDataString(this.Navigation.Uri)}");
}
