namespace LegendaryTelegram.Server.Interfaces;

public interface IRepository<TEntity> : IQueryable<TEntity>
{
    TEntity? Find(params object?[]? keyValues);
    ValueTask<TEntity?> FindAsync(params object?[]? keyValues);
    ValueTask<TEntity?> FindAsync(object?[]? keyValues, CancellationToken cancellationToken);

    TEntity Add(TEntity entity);
    ValueTask<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    TEntity Update(TEntity entity);
    ValueTask<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    
    TEntity Remove(TEntity entity);
    ValueTask<TEntity> RemoveAsync(TEntity entity, CancellationToken cancellationToken = default);
}

