namespace LegendaryTelegram.Server.Interfaces;

public interface IEntityTracker
{
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

