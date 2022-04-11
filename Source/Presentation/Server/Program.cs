using LegendaryTelegram.Presentation.Server;
using LegendaryTelegram.Presentation.Server.Controllers;

using Microsoft.EntityFrameworkCore;

using StrongInject.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddControllersAsServices();
builder.Services.ReplaceWithTransientServiceUsingContainer<Container, WeatherForecastController>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
