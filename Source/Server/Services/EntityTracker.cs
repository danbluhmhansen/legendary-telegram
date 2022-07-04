using LegendaryTelegram.Server.Data;
using LegendaryTelegram.Server.Interfaces;

namespace LegendaryTelegram.Server.Services;

public class EntityTracker : IEntityTracker
{
    public EntityTracker(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    private readonly ApplicationDbContext dbContext;

    public int SaveChanges() => this.dbContext.SaveChanges();

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        this.dbContext.SaveChangesAsync(cancellationToken);
}

