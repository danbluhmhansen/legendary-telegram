namespace BlazorApp1.Server;

using AutoMapper;

using BlazorApp1.Server.Controllers.v1;
using BlazorApp1.Server.Data;

using Microsoft.EntityFrameworkCore;

using StrongInject;

[Register<ApplicationDbContext>]
[Register<CharacterFeaturesController>]
public partial class ServiceContainer : IContainer<ApplicationDbContext>, IContainer<CharacterFeaturesController>
{
    public ServiceContainer(
        IServiceProvider serviceProvider,
        IConfiguration configuration)
    {
        this.serviceProvider = serviceProvider;
        this.configuration = configuration;

        this.mapper = new Mapper(new MapperConfiguration((IMapperConfigurationExpression expression) =>
        {
            expression.CreateMap<Entities.Character, Shared.Models.v1.Character>().ReverseMap();
            expression.CreateMap<Entities.Feature, Shared.Models.v1.Feature>().ReverseMap();
            expression.CreateMap<Entities.CoreEffect, Shared.Models.v1.CoreEffect>().ReverseMap();
            expression.CreateMap<Entities.Effect, Shared.Models.v1.Effect>().ReverseMap();
            expression.CreateMap<Entities.CharacterFeature, Shared.Models.v1.CharacterFeature>().ReverseMap();
        }));
        this.dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
            .Options;
    }

    private readonly IServiceProvider serviceProvider;
    [Instance] private readonly IConfiguration configuration;

    [Instance] private readonly IMapper mapper;
    [Instance] private readonly DbContextOptions<ApplicationDbContext> dbContextOptions;

    [Factory] private ILogger<T> GetLogger<T>() => this.serviceProvider.GetRequiredService<ILogger<T>>();
}
