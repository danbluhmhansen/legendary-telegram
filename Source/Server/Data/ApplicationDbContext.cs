namespace BlazorApp1.Server.Data;

using System.Text.Json;

using BlazorApp1.Server.Entities;

using Bogus;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using OpenIddict.EntityFrameworkCore.Models;

using static OpenIddict.Abstractions.OpenIddictConstants;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

	protected ApplicationDbContext() { }

	public DbSet<Character> Characters => Set<Character>();

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		Faker<Character> characterFaker = new Faker<Character>()
			.CustomInstantiator((Faker faker) => new Character(faker.Random.Guid(), faker.Name.FullName()));

		builder.Entity<Character>().HasData(characterFaker.Generate(100));

		builder.Entity<OpenIddictEntityFrameworkCoreApplication>().HasData(
			new OpenIddictEntityFrameworkCoreApplication
			{
				Id = "59d50e89-c579-4cc4-9cd1-a8db1399fe4e",
				ClientId = "blazor-client",
				ConsentType = ConsentTypes.Explicit,
				DisplayName = "Blazor client application",
				Type = ClientTypes.Public,
				PostLogoutRedirectUris = JsonSerializer.Serialize(
				new[] { "https://localhost:7290/authentication/logout-callback" }),
				RedirectUris = JsonSerializer.Serialize(
				new[] { "https://localhost:7290/authentication/login-callback" }),
				Permissions = JsonSerializer.Serialize(
				new[]
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
				}),
				Requirements = JsonSerializer.Serialize(new[] { Requirements.Features.ProofKeyForCodeExchange }),
			},
			new OpenIddictEntityFrameworkCoreApplication
			{
				Id = "5cf57099-7162-425c-b2cd-11d7c02610d5",
				ClientId = "swagger-client",
				ConsentType = ConsentTypes.Explicit,
				DisplayName = "Swagger client application",
				Type = ClientTypes.Public,
				RedirectUris = JsonSerializer.Serialize(
					new[] { "https://localhost:7289/swagger/oauth2-redirect.html" }),
				Permissions = JsonSerializer.Serialize(new[]
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
				}),
				Requirements = JsonSerializer.Serialize(new[] { Requirements.Features.ProofKeyForCodeExchange }),
			},
			new OpenIddictEntityFrameworkCoreApplication
			{
				Id = "d2557a9b-404d-42ab-89ab-7019929e96a4",
				ClientId = "postman-client",
				ConsentType = ConsentTypes.Explicit,
				DisplayName = "Postman client application",
				Type = ClientTypes.Public,
				RedirectUris = JsonSerializer.Serialize(new[] { "https://oauth.pstmn.io/v1/callback" }),
				Permissions = JsonSerializer.Serialize(new[]
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
				}),
				Requirements = JsonSerializer.Serialize(new[] { Requirements.Features.ProofKeyForCodeExchange }),
			});
	}
}
