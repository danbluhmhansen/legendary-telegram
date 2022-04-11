namespace LegendaryTelegram.Infrastructure.Mapster.Services;

using global::MapsterMapper;

public class MapsterMapper : Application.Common.Interfaces.IMapper
{
    public MapsterMapper(IMapper mapper)
    {
        this.mapper = mapper;
    }

    private readonly IMapper mapper;

    public TDestination Map<TDestination>(object source) => this.mapper.Map<TDestination>(source);
}
