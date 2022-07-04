using LegendaryTelegram.Server.Interfaces;

namespace LegendaryTelegram.Server.Commands;

public class FindEntity<TEntity>
{
    public FindEntity(IRepository<TEntity> repository)
    {
        this.repository = repository;
    }

    private readonly IRepository<TEntity> repository;

    public ValueTask<TEntity?> ExecuteAsync(params object?[]? keyValues) => this.repository.FindAsync(keyValues);

    public ValueTask<TEntity?> ExecuteAsync(object?[]? keyValues, CancellationToken cancellationToken = default) =>
        this.repository.FindAsync(keyValues, cancellationToken);
}

