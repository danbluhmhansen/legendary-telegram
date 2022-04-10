namespace LegendaryTelegram.Presentation.Server;

using LegendaryTelegram.Presentation.Server.Controllers;

using StrongInject;

[Register(typeof(WeatherForecastController), Scope.InstancePerResolution)]
public partial class Container : IContainer<WeatherForecastController>
{
    public Container(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    private readonly IServiceProvider serviceProvider;

    [Factory] private ILogger<T> GetLogger<T>() => this.serviceProvider.GetRequiredService<ILogger<T>>();
}
