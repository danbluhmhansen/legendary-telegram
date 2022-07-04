using LegendaryTelegram.Server.Interfaces;

namespace LegendaryTelegram.Server.Commands;

public class RemoveEntity<TEntity>
{
    public RemoveEntity(IRepository<TEntity> repository)
    {
        this.repository = repository;
    }

    private readonly IRepository<TEntity> repository;

    public ValueTask<TEntity> ExecuteAsync(TEntity entity, CancellationToken cancellationToken = default) =>
        this.repository.RemoveAsync(entity, cancellationToken);
}

