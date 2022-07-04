using System.Collections;
using System.Linq.Expressions;

using LegendaryTelegram.Server.Interfaces;

namespace LegendaryTelegram.Server.Commands;

public class QueryEntities<TEntity> : IQueryable<TEntity>
{
    public QueryEntities(IRepository<TEntity> repository)
    {
        this.repository = repository;
    }

    private readonly IRepository<TEntity> repository;

    public Type ElementType => repository.ElementType;

    public Expression Expression => repository.Expression;

    public IQueryProvider Provider => repository.Provider;

    public IEnumerator<TEntity> GetEnumerator() => repository.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => repository.GetEnumerator();
}

