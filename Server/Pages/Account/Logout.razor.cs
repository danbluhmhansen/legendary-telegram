namespace LegendaryTelegram.Server.Pages.Account;

using LegendaryTelegram.Server.Models;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;

public partial class Logout : ComponentBase
{
    [Inject] private SignInManager<ApplicationUser> SignInManager { get; set; } = default!;
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await this.SignInManager.SignOutAsync();
        this.NavigationManager.NavigateTo("/");
    }
}
