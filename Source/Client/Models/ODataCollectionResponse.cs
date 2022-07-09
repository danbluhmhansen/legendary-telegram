namespace LegendaryTelegram.Client.Models;

using System.Text.Json.Serialization;

public record ODataCollectionResponse<T>(
    IEnumerable<T> Value,
    [property: JsonPropertyName("@odata.context")] Uri Context,
    [property: JsonPropertyName("@odata.count")] int? Count);

