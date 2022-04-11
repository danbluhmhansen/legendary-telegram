namespace LegendaryTelegram.Application.WeatherForecasts.Models;

public class WeatherForecast
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public int TemperatureC { get; set; }
    public string? Summary { get; set; }
    public int TemperatureF => 32 + (int)(this.TemperatureC / 0.5556);
}
