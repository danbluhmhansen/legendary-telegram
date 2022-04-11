namespace LegendaryTelegram.Presentation.Server.Controllers;

using LegendaryTelegram.Application.Common.Services;
using LegendaryTelegram.Application.WeatherForecasts.Models;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    public WeatherForecastController(QueryEntities queryEntities, ILogger<WeatherForecastController> logger)
    {
        this.queryEntities = queryEntities;
        this.logger = logger;
    }

    private readonly QueryEntities queryEntities;
    private readonly ILogger<WeatherForecastController> logger;

    [HttpGet]
    public IQueryable<WeatherForecast> Get() => this.queryEntities.Execute<WeatherForecast, Domain.WeatherForecast>();
}
