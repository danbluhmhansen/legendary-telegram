namespace LegendaryTelegram.Application.Common.Services;

using LegendaryTelegram.Application.Common.Interfaces;

public class QueryEntities
{
    public QueryEntities(IRepository repository, IProjector projector)
    {
        this.repository = repository;
        this.projector = projector;
    }

    private readonly IRepository repository;
    private readonly IProjector projector;

    public IQueryable<TModel> Execute<TModel, TEntity>() where TEntity : class =>
        this.projector.Project<TModel>(this.repository.Query<TEntity>());
}
