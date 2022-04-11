namespace LegendaryTelegram.Application.Common.Interfaces;

public interface IMapper
{
    TDestination Map<TDestination>(object source);
}
