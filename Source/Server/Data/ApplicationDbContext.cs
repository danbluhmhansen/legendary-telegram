namespace BlazorApp1.Server.Data;

using BlazorApp1.Server.Entities;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

	protected ApplicationDbContext() { }

	public DbSet<Character> Characters => Set<Character>();
	public DbSet<Feature> Features => Set<Feature>();
	public DbSet<CoreEffect> CoreEffects => Set<CoreEffect>();
	public DbSet<Effect> Effects => Set<Effect>();
}
