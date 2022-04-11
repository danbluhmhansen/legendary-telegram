namespace LegendaryTelegram.Application.Common.Interfaces;

public interface IProjector
{
    IQueryable<TDestination> Project<TDestination>(IQueryable source);
}
