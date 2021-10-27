namespace BlazorApp1.Server.Controllers;

using BlazorApp1.Shared;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

using OpenIddict.Validation.AspNetCore;

[Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
	private readonly static string[] Summaries = new[]
	{
		"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching",
	};

	private readonly ILogger<WeatherForecastController> _logger;

	public WeatherForecastController(ILogger<WeatherForecastController> logger)
	{
		_logger = logger;
	}

	[HttpGet]
	[EnableQuery]
	public IEnumerable<WeatherForecast> Get()
	{
		return Enumerable.Range(1, 5).Select(index => new WeatherForecast {
			Date = DateTime.Now.AddDays(index),
			TemperatureC = Random.Shared.Next(-20, 55),
			Summary = Summaries[Random.Shared.Next(Summaries.Length)]
		})
		.ToArray();
	}
}
