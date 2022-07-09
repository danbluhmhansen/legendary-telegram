using System.Net.Http.Json;

using LegendaryTelegram.Client.Components;
using LegendaryTelegram.Client.Models;

using Microsoft.AspNetCore.Components;

namespace LegendaryTelegram.Client.Pages;

public partial class FetchData : ComponentBase
{
    [CascadingParameter(Name = "ErrorComponent")]
    private IErrorComponent? ErrorComponent { get; init; }

    [Inject] private IHttpClientFactory? HttpClientFactory { get; init; }

    private IQueryable<WeatherForecast>? forecasts;

    protected override async Task OnInitializedAsync()
    {
        if (HttpClientFactory is null) throw new ArgumentNullException(nameof(HttpClientFactory));

        try
        {
            using var httpClient = HttpClientFactory.CreateClient("Local");
            forecasts = (await httpClient!.GetFromJsonAsync<WeatherForecast[]>("sample-data/weather.json"))?.AsQueryable();
        }
        catch (Exception)
        {
            ErrorComponent!.ShowError(new Error("Error", "Error while getting data, try reloading."));
        }
    }

    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string? Summary { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}

