namespace LegendaryTelegram.Server.Services;

using System.Threading;
using System.Threading.Tasks;
using Bogus;
using LegendaryTelegram.Server.Commands;
using LegendaryTelegram.Server.Interfaces;
using LegendaryTelegram.Server.Models;

using Microsoft.EntityFrameworkCore;

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
                ClientSecret = "3e52ce50-5ac2-4885-94d8-2c8a6c1c902a",
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
                PostLogoutRedirectUris = { new Uri("https://localhost:7169/") },
                RedirectUris = { new Uri("https://localhost:7169/"), new Uri("https://localhost:7169/swagger/oauth2-redirect.html") },
                Requirements = { Requirements.Features.ProofKeyForCodeExchange },
                Type = ClientTypes.Confidential,
            }, cancellationToken);
        }

        var queryCharacters = scope.ServiceProvider.GetRequiredService<QueryEntities<Character>>();
        if (!await queryCharacters.AnyAsync())
        {
            var addCharacter = scope.ServiceProvider.GetRequiredService<AddEntity<Character>>();
            var entityTracker = scope.ServiceProvider.GetService<IEntityTracker>();

           foreach (var character in new Faker<Character>()
                .RuleFor(character => character.Name, f => f.Name.FullName())
                .Generate(100))
           {
              await addCharacter.ExecuteAsync(character);
           } 

            if (entityTracker is not null)
                await entityTracker.SaveChangesAsync();
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
