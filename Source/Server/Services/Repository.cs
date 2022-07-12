using System.Collections;
using System.Linq.Expressions;

using LegendaryTelegram.Server.Data;
using LegendaryTelegram.Server.Interfaces;

namespace LegendaryTelegram.Server.Services;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    public Repository(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    Type IQueryable.ElementType => this.dbContext.Set<TEntity>().AsQueryable().ElementType;
    Expression IQueryable.Expression => this.dbContext.Set<TEntity>().AsQueryable().Expression;
    IQueryProvider IQueryable.Provider => this.dbContext.Set<TEntity>().AsQueryable().Provider;

    private readonly ApplicationDbContext dbContext;

    public TEntity? Find(params object?[]? keyValues) => this.dbContext.Set<TEntity>().Find(keyValues);

    public ValueTask<TEntity?> FindAsync(params object?[]? keyValues) => this.dbContext.Set<TEntity>().FindAsync(keyValues);

    public ValueTask<TEntity?> FindAsync(object?[]? keyValues, CancellationToken cancellationToken) =>
        this.dbContext.Set<TEntity>().FindAsync(keyValues, cancellationToken);

    public TEntity Add(TEntity entity) => this.dbContext.Add(entity).Entity;

    public ValueTask<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default) =>
        ValueTask.FromResult(Add(entity));

    public TEntity Update(TEntity entity) => this.dbContext.Update(entity).Entity;

    public ValueTask<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default) =>
        ValueTask.FromResult(Update(entity));

    public TEntity Remove(TEntity entity) => this.dbContext.Remove(entity).Entity;

    public ValueTask<TEntity> RemoveAsync(TEntity entity, CancellationToken cancellationToken = default) =>
        ValueTask.FromResult(Remove(entity));

    public IEnumerator<TEntity> GetEnumerator() => this.dbContext.Set<TEntity>().AsQueryable().GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => this.dbContext.Set<TEntity>().AsQueryable().GetEnumerator();
}

