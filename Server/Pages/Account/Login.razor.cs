namespace LegendaryTelegram.Server.Pages.Account;

using LegendaryTelegram.Server.Models;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;

public partial class Login : ComponentBase
{
    [Parameter] public string? ReturnUrl { get; set; }

    [Inject] private SignInManager<ApplicationUser> SignInManager { get; set; } = default!;
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    [Inject] private ILogger<Login> Logger { get; set; } = default!;

    private string? username;
    private string? password;
    private bool rememberMe;

    private async Task SignInAsync()
    {
        // TODO: Proper validation
        if (string.IsNullOrWhiteSpace(this.username) || string.IsNullOrWhiteSpace(this.password))
            return;

        this.Logger?.LogInformation($"Attempting to sign in with username: {this.username}");

        var result = await this.SignInManager.PasswordSignInAsync(this.username, this.password, this.rememberMe, false);

        if (!result.Succeeded)
            // TODO: Return error messages.
            return;

        if (result.RequiresTwoFactor)
        {
            // TODO: Redirect to two factor page.
        }

        if (result.IsLockedOut)
        {
            // TODO: Redirect to locked out page.
        }

        if (!string.IsNullOrWhiteSpace(this.ReturnUrl))
            this.NavigationManager.NavigateTo(this.ReturnUrl);
        else
            this.NavigationManager.NavigateTo("/");
    }
}
