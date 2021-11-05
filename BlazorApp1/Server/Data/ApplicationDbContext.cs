namespace BlazorApp1.Server.Data;

using System.Text.Json;

using BlazorApp1.Server.Models;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using OpenIddict.EntityFrameworkCore.Models;

using static OpenIddict.Abstractions.OpenIddictConstants;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

	protected ApplicationDbContext() { }

	public DbSet<Customer> Customers => Set<Customer>();

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.Entity<Customer>().HasData(
			new Customer
			{
				Id = Guid.Parse("a9744591-32c4-4477-8c76-ad2d2d50d7b5"),
				Name = "John Doe",
				FirstName = "John",
				LastName = "Doe",
				PhoneNumber = "12345678",
				Email = "john@doe.com",
			},
			new Customer
			{
				Id = Guid.Parse("845ceff5-f389-45d9-89eb-53d7cea8fb5e"),
				Name = "Milan Magdalena",
				FirstName = "Milan",
				LastName = "Magdalena",
				PhoneNumber = "75842857",
				Email = "milan@magdalena.com",
			},
			new Customer
			{
				Id = Guid.Parse("45b6fe3a-cf89-4284-9c6f-c407c430e08d"),
				Name = "David Hrodebert",
				FirstName = "David",
				LastName = "Hrodebert",
				PhoneNumber = "13758938",
				Email = "david@hrodebert.com",
			},
			new Customer
			{
				Id = Guid.Parse("da70a3b9-a1fa-4530-a076-b97f88e42fa3"),
				Name = "Odilo Eadgar",
				FirstName = "Odilo",
				LastName = "Eadgar",
				PhoneNumber = "57698910",
				Email = "odilo@eadgar.com",
			});

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
