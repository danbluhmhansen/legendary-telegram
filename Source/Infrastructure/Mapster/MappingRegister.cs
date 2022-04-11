namespace LegendaryTelegram.Infrastructure.Mapster;

using global::Mapster;

using MapsterMapper;

public class MappingRegister : IRegister
{
    public void Register(TypeAdapterConfig config) =>
        config.NewConfig<Domain.WeatherForecast, Application.WeatherForecasts.Models.WeatherForecast>().TwoWays();
}
