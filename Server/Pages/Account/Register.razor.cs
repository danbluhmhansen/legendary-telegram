namespace LegendaryTelegram.Server.Pages.Account;

using Blazorise;

using LegendaryTelegram.Server.Models;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;

public partial class Register : ComponentBase
{
    [Parameter] public string? ReturnUrl { get; set; }

    [Inject] private SignInManager<ApplicationUser> SignInManager { get; set; } = default!;
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    [Inject] private ILogger<Register> Logger { get; set; } = default!;

    private string? username;
    private string? password;
    private string? confirmPassword;

    private async Task RegisterAsync()
    {
        // TODO: Proper validation
        if (string.IsNullOrWhiteSpace(this.username)
            || string.IsNullOrWhiteSpace(this.password)
            || string.IsNullOrWhiteSpace(this.confirmPassword)
            || this.password != this.confirmPassword)
            return;

        this.Logger?.LogInformation($"Attempting to register with username: {this.username}");

        var user = new ApplicationUser { UserName = this.username };
        var result = await this.SignInManager.UserManager.CreateAsync(user, this.password);

        if (!result.Succeeded)
            // TODO: Return error messages.
            return;

        await this.SignInManager.SignInAsync(user, false);

        if (string.IsNullOrWhiteSpace(this.ReturnUrl))
            this.NavigationManager.NavigateTo("/");
        else
            this.NavigationManager.NavigateTo(this.ReturnUrl);
    }

    private void ConfirmPasswordValidator(ValidatorEventArgs args) =>
        args.Status = this.password == this.confirmPassword ? ValidationStatus.Success : ValidationStatus.Error;
}
