namespace LegendaryTelegram.Server.Pages.Account;

using LegendaryTelegram.Server.Models;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;

public partial class Login : ComponentBase
{
    [Parameter] public string? ReturnUrl { get; set; }
    [Inject] private SignInManager<ApplicationUser>? SignInManager { get; set; }

    private string? UserName { get; set; }
    private string? Password { get; set; }

    private async Task SignInAsync()
    {
        if (this.SignInManager is null
            || string.IsNullOrWhiteSpace(this.UserName)
            || string.IsNullOrWhiteSpace(this.Password))
            return;

        var result = await this.SignInManager.PasswordSignInAsync(this.UserName, this.Password, false, false);
    }
}
