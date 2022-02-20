namespace BlazorApp1.Client;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.JSInterop;

// RemoteAuthenticatorView only uses the path and query part of a URI when redirecting,
// so it cannot redirect to an app hosted on another base URI/port.
// https://github.com/dotnet/aspnetcore/issues/25153
public class CustomRemoteAuthenticatorView : RemoteAuthenticatorView
{
    [Inject] internal IJSRuntime Js { get; set; } = null!;
    [Inject] internal NavigationManager Navigation { get; set; } = null!;

    protected override async Task OnParametersSetAsync()
    {
        switch (this.Action)
        {
            case RemoteAuthenticationActions.Profile when this.ApplicationPaths.RemoteProfilePath != null:
                this.UserProfile ??= this.LoggingIn;
                await RedirectToProfile();
                break;
            case RemoteAuthenticationActions.Register when this.ApplicationPaths.RemoteRegisterPath != null:
                this.Registering ??= this.LoggingIn;
                await RedirectToRegister();
                break;
            default:
                await base.OnParametersSetAsync();
                break;
        }
    }

    private ValueTask RedirectToProfile() =>
        this.Js.InvokeVoidAsync("location.replace", this.Navigation.ToAbsoluteUri(this.ApplicationPaths.RemoteProfilePath));

    private ValueTask RedirectToRegister()
    {
        Uri loginUrl = this.Navigation.ToAbsoluteUri(this.ApplicationPaths.LogInPath);
        Uri registerUrl = this.Navigation.ToAbsoluteUri($"{this.ApplicationPaths.RemoteRegisterPath}?returnUrl={loginUrl}");

        return this.Js.InvokeVoidAsync("location.replace", registerUrl);
    }
}
