namespace LegendaryTelegram.Application.Common.Interfaces;

public interface IRepository
{
    IQueryable<TEntity> Query<TEntity>() where TEntity : class;

    TEntity? Find<TEntity>(params object?[]? keyValues) where TEntity : class;
    ValueTask<TEntity?> FindAsync<TEntity>(object?[]? keyValues, CancellationToken cancellationToken)
        where TEntity : class;
    ValueTask<TEntity?> FindAsync<TEntity>(params object?[]? keyValues) where TEntity : class;

    TEntity Add<TEntity>(TEntity entity) where TEntity : class;
    ValueTask<TEntity> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        where TEntity : class;
    void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
    void AddRange<TEntity>(params TEntity[] entities) where TEntity : class;
    Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        where TEntity : class;
    Task AddRangeAsync<TEntity>(params TEntity[] entities) where TEntity : class;

    TEntity Update<TEntity>(TEntity entity) where TEntity : class;
    ValueTask<TEntity> UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        where TEntity : class;
    void UpdateRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
    void UpdateRange<TEntity>(params TEntity[] entities) where TEntity : class;
    Task UpdateRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        where TEntity : class;
    Task UpdateRangeAsync<TEntity>(params TEntity[] entities) where TEntity : class;

    TEntity Remove<TEntity>(TEntity entity) where TEntity : class;
    ValueTask<TEntity> RemoveAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        where TEntity : class;
    void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
    void RemoveRange<TEntity>(params TEntity[] entities) where TEntity : class;
    Task RemoveRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        where TEntity : class;
    Task RemoveRangeAsync<TEntity>(params TEntity[] entities) where TEntity : class;
}
