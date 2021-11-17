namespace BlazorApp1.Server.Data;

using BlazorApp1.Server.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using OpenIddict.EntityFrameworkCore.Models;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

	protected ApplicationDbContext() { }

	public DbSet<OpenIddictEntityFrameworkCoreApplication> OpenIddictApplications =>
		Set<OpenIddictEntityFrameworkCoreApplication>();
	public DbSet<OpenIddictEntityFrameworkCoreAuthorization> OpenIddictAuthorizations =>
		Set<OpenIddictEntityFrameworkCoreAuthorization>();
	public DbSet<OpenIddictEntityFrameworkCoreScope> OpenIddictScopes =>
		Set<OpenIddictEntityFrameworkCoreScope>();
	public DbSet<OpenIddictEntityFrameworkCoreToken> OpenIddictTokens =>
		Set<OpenIddictEntityFrameworkCoreToken>();

	public DbSet<Character> Characters => Set<Character>();
	public DbSet<Feature> Features => Set<Feature>();
	public DbSet<CoreEffect> CoreEffects => Set<CoreEffect>();
	public DbSet<Effect> Effects => Set<Effect>();

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.UseOpenIddict();

		builder.Entity<ApplicationUser>().ToTable("AspNetUsers", "identity");
		builder.Entity<IdentityUserClaim<string>>().ToTable("AspNetUserClaims", "identity");
		builder.Entity<IdentityUserLogin<string>>().ToTable("AspNetUserLogins", "identity");
		builder.Entity<IdentityUserToken<string>>().ToTable("AspNetUserTokens", "identity");
		builder.Entity<IdentityRole>().ToTable("AspNetRoles", "identity");
		builder.Entity<IdentityRoleClaim<string>>().ToTable("AspNetRoleClaims", "identity");
		builder.Entity<IdentityUserRole<string>>().ToTable("AspNetUserRoles", "identity");

		builder.Entity<OpenIddictEntityFrameworkCoreApplication>().ToTable("OpenIddictApplications", "opid");
		builder.Entity<OpenIddictEntityFrameworkCoreAuthorization>().ToTable("OpenIddictAuthorizations", "opid");
		builder.Entity<OpenIddictEntityFrameworkCoreScope>().ToTable("OpenIddictScopes", "opid");
		builder.Entity<OpenIddictEntityFrameworkCoreToken>().ToTable("OpenIddictTokens", "opid");
	}
}
