namespace LegendaryTelegram.Presentation.Server;

using LegendaryTelegram.Application.Common.Interfaces;
using LegendaryTelegram.Application.Common.Services;
using LegendaryTelegram.Infrastructure.EntityFramework.Common.Services;
using LegendaryTelegram.Infrastructure.Mapster.Services;
using LegendaryTelegram.Presentation.Server.Controllers;

using Microsoft.EntityFrameworkCore;

using StrongInject;

[Register(typeof(MapsterMapper), Scope.SingleInstance, typeof(IMapper))]
[Register(typeof(MapsterProjector), Scope.SingleInstance, typeof(IProjector))]
[Register(typeof(DbContextRepository<ApplicationDbContext>), Scope.InstancePerResolution, typeof(IRepository))]
[Register(typeof(QueryEntities), Scope.InstancePerResolution)]
[Register(typeof(WeatherForecastController), Scope.InstancePerResolution)]
public partial class Container : IContainer<WeatherForecastController>
{
    public Container(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
        this.configuration = serviceProvider.GetRequiredService<IConfiguration>();
    }

    private readonly IServiceProvider serviceProvider;
    private readonly IConfiguration configuration;

    [Factory] private ApplicationDbContext GetApplicationDbContext() => new ApplicationDbContext(
        new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(this.configuration.GetConnectionString("DefaultConnection"))
            .Options);

    [Factory] private ILogger<T> GetLogger<T>() => this.serviceProvider.GetRequiredService<ILogger<T>>();
}
