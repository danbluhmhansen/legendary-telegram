namespace LegendaryTelegram.Infrastructure.EntityFramework.Common.Services;

using LegendaryTelegram.Domain;

using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected ApplicationDbContext() { }

    public DbSet<WeatherForecast> WeatherForecasts => Set<WeatherForecast>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<WeatherForecast>();
    }
}
