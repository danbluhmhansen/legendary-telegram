namespace LegendaryTelegram.Infrastructure.EntityFramework.Common.Services;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using LegendaryTelegram.Application.Common.Interfaces;

using Microsoft.EntityFrameworkCore;

public class DbContextRepository<TDbContext> : IRepository
    where TDbContext : DbContext
{
    public DbContextRepository(TDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    private readonly TDbContext dbContext;

    public IQueryable<TEntity> Query<TEntity>() where TEntity : class => this.dbContext.Set<TEntity>();

    public TEntity? Find<TEntity>(params object?[]? keyValues) where TEntity : class =>
        this.dbContext.Set<TEntity>().Find(keyValues);
    public ValueTask<TEntity?> FindAsync<TEntity>(object?[]? keyValues, CancellationToken cancellationToken)
        where TEntity : class => this.dbContext.Set<TEntity>().FindAsync(keyValues, cancellationToken);
    public ValueTask<TEntity?> FindAsync<TEntity>(params object?[]? keyValues) where TEntity : class =>
        this.dbContext.Set<TEntity>().FindAsync(keyValues);

    public TEntity Add<TEntity>(TEntity entity) where TEntity : class => this.dbContext.Add(entity).Entity;
    public ValueTask<TEntity> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        where TEntity : class => ValueTask.FromResult(Add(entity));
    public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class =>
        this.dbContext.AddRange(entities);
    public void AddRange<TEntity>(params TEntity[] entities) where TEntity : class => this.dbContext.AddRange(entities);
    public Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        AddRange(entities);
        return Task.CompletedTask;
    }

    public Task AddRangeAsync<TEntity>(params TEntity[] entities) where TEntity : class
    {
        AddRange(entities);
        return Task.CompletedTask;
    }

    public TEntity Update<TEntity>(TEntity entity) where TEntity : class => this.dbContext.Update(entity).Entity;
    public ValueTask<TEntity> UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
    where TEntity : class => ValueTask.FromResult(Update(entity));
    public void UpdateRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class =>
        this.dbContext.UpdateRange(entities);
    public void UpdateRange<TEntity>(params TEntity[] entities) where TEntity : class =>
        this.dbContext.UpdateRange(entities);
    public Task UpdateRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        UpdateRange(entities);
        return Task.CompletedTask;
    }
    public Task UpdateRangeAsync<TEntity>(params TEntity[] entities) where TEntity : class
    {
        UpdateRange(entities);
        return Task.CompletedTask;
    }

    public TEntity Remove<TEntity>(TEntity entity) where TEntity : class => this.dbContext.Remove(entity).Entity;
    public ValueTask<TEntity> RemoveAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        where TEntity : class => ValueTask.FromResult(Remove(entity));
    public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class =>
        this.dbContext.RemoveRange(entities);
    public void RemoveRange<TEntity>(params TEntity[] entities) where TEntity : class =>
        this.dbContext.RemoveRange(entities);
    public Task RemoveRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        RemoveRange(entities);
        return Task.CompletedTask;
    }
    public Task RemoveRangeAsync<TEntity>(params TEntity[] entities) where TEntity : class
    {
        RemoveRange(entities);
        return Task.CompletedTask;
    }
}
