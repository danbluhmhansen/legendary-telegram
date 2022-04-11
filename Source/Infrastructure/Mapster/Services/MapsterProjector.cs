namespace LegendaryTelegram.Infrastructure.Mapster.Services;

using global::Mapster;

using LegendaryTelegram.Application.Common.Interfaces;

public class MapsterProjector : IProjector
{
    public IQueryable<TDestination> Project<TDestination>(IQueryable source) => source.ProjectToType<TDestination>();
}
