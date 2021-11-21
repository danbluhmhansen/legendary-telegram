﻿namespace BlazorApp1.Server.Data;

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

	public DbSet<CharacterFeature> CharacterFeatures => Set<CharacterFeature>();

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

		builder.Entity<Character>()
			.HasMany((Character character) => character.Features)
			.WithMany((Feature feature) => feature.Characters)
			.UsingEntity<CharacterFeature>();

		builder.Entity<Feature>()
			.HasMany((Feature feature) => feature.Characters)
			.WithMany((Character character) => character.Features)
			.UsingEntity<CharacterFeature>();

		builder.Entity<CoreEffect>().Property((CoreEffect effect) => effect.Rule).HasColumnType("jsonb");
		builder.Entity<Effect>().Property((Effect effect) => effect.Rule).HasColumnType("jsonb");
	}
}
