namespace LegendaryTelegram.Server.Data;

using LegendaryTelegram.Server.Models;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected ApplicationDbContext() { }

    public DbSet<Character> Characters => Set<Character>();
}
