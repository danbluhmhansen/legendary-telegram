namespace LegendaryTelegram.Server.Services;

using System.Threading;
using System.Threading.Tasks;

using LegendaryTelegram.Server.Data;

using OpenIddict.Abstractions;

using static OpenIddict.Abstractions.OpenIddictConstants;

public class SeedWorker : IHostedService
{
    public SeedWorker(IServiceProvider services)
    {
        this.services = services;
    }

    private readonly IServiceProvider services;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = this.services.CreateAsyncScope();

        var applicationManager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

        if (await applicationManager.FindByClientIdAsync("sample-client", cancellationToken) is null)
        {
            await applicationManager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "sample-client",
                ConsentType = ConsentTypes.Explicit,
                DisplayName = "Sample Client Application",
                Permissions =
                {
                    Permissions.Endpoints.Authorization,
                    Permissions.Endpoints.Logout,
                    Permissions.Endpoints.Token,
                    Permissions.GrantTypes.AuthorizationCode,
                    Permissions.GrantTypes.RefreshToken,
                    Permissions.ResponseTypes.Code,
                    Permissions.Scopes.Email,
                    Permissions.Scopes.Profile,
                    Permissions.Scopes.Roles
                },
                PostLogoutRedirectUris = { new Uri("https://localhost:44300/signout-oidc") },
                RedirectUris = { new Uri("https://localhost:44300/signin-oidc") },
                Requirements = { Requirements.Features.ProofKeyForCodeExchange },
                Type = ClientTypes.Public,
            }, cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
